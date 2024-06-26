
resource "aws_s3_bucket" "server_release_bucket" {
  bucket = "${var.project_name}-server-bucket"
  tags   = merge(var.mandatory_tags, { Name = "${var.project_name}-server-bucket" })
}

resource "aws_s3_bucket_versioning" "bucket_versioning" {
  bucket = aws_s3_bucket.server_release_bucket.id
  versioning_configuration {
    status = "Enabled"
  }
}

resource "aws_s3_bucket" "website_bucket" {
  bucket = "${var.project_name}-website" # Change to your unique bucket name
  tags   = merge(var.mandatory_tags, { Name = "${var.project_name}-website" })
}

resource "aws_s3_bucket_website_configuration" "web_config" {
  bucket = aws_s3_bucket.website_bucket.id

  index_document {
    suffix = "index.html"
  }
  error_document {
    key = "error.html"
  }
}

resource "aws_s3_bucket_public_access_block" "bucket_access_block" {
  bucket = aws_s3_bucket.website_bucket.id

  block_public_acls   = false # Change to true because oaic
  block_public_policy = false
}

resource "aws_s3_bucket_policy" "website_bucket_policy" {
  bucket = aws_s3_bucket.website_bucket.id

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect    = "Allow"
        Principal = "*"
        Action    = "s3:GetObject"
        Resource  = "${aws_s3_bucket.website_bucket.arn}/*"
      }
      # ,
      # {
      #   Effect = "Allow"
      #   Principal = {
      #     AWS = "arn:aws:iam::cloudfront:user/CloudFront Origin Access Identity ${aws_cloudfront_origin_access_identity.oai.cloudfront_access_identity_path}"
      #   }
      #   Action   = "s3:GetObject"
      #   Resource = "${aws_s3_bucket.website_bucket.arn}/*"
      # }
    ]
  })
}

resource "aws_vpc" "vpc" {
  cidr_block           = var.cidr_block
  enable_dns_hostnames = true
  tags                 = merge(var.mandatory_tags, { Name = "${var.project_name}-vpc" })
}

resource "aws_subnet" "public_subnets" {
  count             = length(var.vpc_public_subnets)
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.vpc_public_subnets[count.index].cidr_block
  tags              = merge(var.mandatory_tags, { Name = "${var.project_name}-public-subnet-${count.index}" })
  availability_zone = var.vpc_public_subnets[count.index].az
}

resource "aws_subnet" "private_subnets" {
  count             = length(var.vpc_private_subnets)
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.vpc_private_subnets[count.index].cidr_block
  availability_zone = var.vpc_private_subnets[count.index].az
  tags              = merge(var.mandatory_tags, { Name = "${var.project_name}-private-subnet-${count.index}" })
}

# Internet Gateway
resource "aws_internet_gateway" "internet_gateway" {
  vpc_id = aws_vpc.vpc.id
  tags   = merge(var.mandatory_tags, { Name = "${var.project_name}-internet-gateway" })
}

# Routing table
resource "aws_route_table" "route_table" {
  vpc_id = aws_vpc.vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.internet_gateway.id
  }
  tags = merge(var.mandatory_tags, { Name = "${var.project_name}-route-table" })
}

# Resource association table
resource "aws_route_table_association" "route_table_association" {
  count          = length(aws_subnet.public_subnets)
  subnet_id      = aws_subnet.public_subnets[count.index].id
  route_table_id = aws_route_table.route_table.id
}

# NAT Gateway Elastic IP
resource "aws_eip" "nat_eip" {
  domain = "vpc"
}

# NAT Gateway
resource "aws_nat_gateway" "nat_gateway" {
  allocation_id = aws_eip.nat_eip.id
  subnet_id     = aws_subnet.public_subnets[0].id
  tags          = merge(var.mandatory_tags, { Name = "${var.project_name}-nat-gateway" })
}

# Private Routing table
resource "aws_route_table" "private_route_table" {
  vpc_id = aws_vpc.vpc.id

  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat_gateway.id
  }
  tags = merge(var.mandatory_tags, { Name = "${var.project_name}-private-route-table" })
}

# Associate private subnets with the private route table
resource "aws_route_table_association" "private_route_table_association" {
  count          = length(aws_subnet.private_subnets)
  subnet_id      = aws_subnet.private_subnets[count.index].id
  route_table_id = aws_route_table.private_route_table.id
}

#Check this
resource "aws_db_subnet_group" "db_subnet_group" {
  name       = "${var.project_name}-subnet-group"
  subnet_ids = aws_subnet.public_subnets[*].id
  tags       = merge(var.mandatory_tags, { Name = "${var.project_name}-public-subnet-group" })
}

resource "aws_db_instance" "db" {
  identifier                  = "${var.project_name}-db"
  allocated_storage           = 20
  engine                      = "sqlserver-ex"
  engine_version              = "16.00.4095.4.v1"
  instance_class              = "db.t3.micro"
  publicly_accessible         = true
  username                    = "admin"
  multi_az                    = false # Free tier supports only single AZ
  manage_master_user_password = true  #Fetch password from console
  apply_immediately           = true
  copy_tags_to_snapshot       = true
  db_subnet_group_name        = aws_db_subnet_group.db_subnet_group.name
  skip_final_snapshot         = true

  vpc_security_group_ids = [
    aws_security_group.db_security_group.id
  ]
  tags = merge(var.mandatory_tags, { Name = "${var.project_name}-db" })
}

resource "aws_acm_certificate" "sever_cert" {
  domain_name       = "estate-emporium-api.bbdsoftware.com" # Change to your domain
  validation_method = "DNS"
}

resource "aws_acm_certificate" "website_cert" {
  domain_name       = "estate-emporium.bbdsoftware.com" # Change to your domain
  validation_method = "DNS"
}

resource "aws_wafv2_web_acl" "waf_acl" {
  name        = "${var.project_name}-waf-acl"
  scope       = "REGIONAL"
  description = "WAF for API Gateway and S3"

  default_action {
    allow {}
  }

  tags = merge(var.mandatory_tags, { Name = "${var.project_name}-waf-acl" })
  visibility_config {
    cloudwatch_metrics_enabled = true
    metric_name                = "wafACL"
    sampled_requests_enabled   = true
  }

  rule {
    name     = "AWSManagedRulesCommonRuleSet"
    priority = 10

    override_action {
      none {}
    }

    statement {
      managed_rule_group_statement {
        name        = "AWSManagedRulesCommonRuleSet"
        vendor_name = "AWS"
      }
    }

    visibility_config {
      cloudwatch_metrics_enabled = true
      metric_name                = "AWSManagedRulesCommonRuleSetMetric"
      sampled_requests_enabled   = true
    }
  }


  rule {
    name     = "AWSManagedRulesKnownBadInputsRuleSet"
    priority = 20

    override_action {
      count {}
    }

    statement {
      managed_rule_group_statement {
        name        = "AWSManagedRulesKnownBadInputsRuleSet"
        vendor_name = "AWS"
      }
    }

    visibility_config {
      cloudwatch_metrics_enabled = true
      metric_name                = "AWSManagedRulesKnownBadInputsRuleSetMetric"
      sampled_requests_enabled   = true
    }
  }
}

resource "aws_cloudfront_origin_access_identity" "oai" {
  comment = "OAI for my S3 static website"
}

# resource "aws_cloudfront_distribution" "s3_distribution" {
#   origin {
#     domain_name = aws_s3_bucket.website_bucket.bucket_regional_domain_name
#     origin_id   = "s3-web-origin"

#     s3_origin_config {
#       origin_access_identity = aws_cloudfront_origin_access_identity.oai.cloudfront_access_identity_path
#     }
#   }

#   enabled             = true
#   is_ipv6_enabled     = true
#   comment             = "S3 Static Estate emporium Website Distribution"
#   default_root_object = "index.html"

#   default_cache_behavior {
#     allowed_methods  = ["GET", "HEAD"]
#     cached_methods   = ["GET", "HEAD"]
#     target_origin_id = "s3-web-origin"

#     forwarded_values {
#       query_string = true

#       cookies {
#         forward = "all"
#       }
#     }

#     viewer_protocol_policy = "redirect-to-https"

#     min_ttl     = 0
#     default_ttl = 0 # 1200 #20 min
#     max_ttl     = 0 # 10800 #3hr
#   }

#   restrictions {
#     geo_restriction {
#       restriction_type = "none"
#     }
#   }

#   viewer_certificate {
#     cloudfront_default_certificate = true
#   }

#   # web_acl_id = aws_wafv2_web_acl.waf_acl.id
# }

resource "aws_elastic_beanstalk_application" "server_app" {
  name        = "${var.project_name}-api"
  description = "Beanstalk application"
}

resource "aws_elastic_beanstalk_environment" "server_env" {
  name                = "${var.project_name}-api"
  application         = aws_elastic_beanstalk_application.server_app.name
  solution_stack_name = "64bit Amazon Linux 2023 v4.3.3 running Docker"
  cname_prefix        = "${var.project_name}-api"

  setting {
    namespace = "aws:ec2:vpc"
    name      = "VPCId"
    value     = aws_vpc.vpc.id
  }
  setting {
    namespace = "aws:ec2:vpc"
    name      = "Subnets"
    value     = join(",", aws_subnet.private_subnets[*].id)
  }
  setting {
    namespace = "aws:ec2:vpc"
    name      = "ELBSubnets"
    value     = join(",", aws_subnet.public_subnets[*].id)
  }
  setting {
    namespace = "aws:ec2:instances"
    name      = "InstanceTypes"
    value     = "t3.micro"
  }
  # setting {
  #   namespace = "aws:ec2:vpc"
  #   name      = "AssociatePublicIpAddress"
  #   value     = true
  # }
  setting {
    namespace = "aws:autoscaling:asg"
    name      = "MaxSize"
    value     = "2"
  }

  setting {
    namespace = "aws:elbv2:loadbalancer"
    name      = "IdleTimeout"
    value     = "60"
  }
  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "IamInstanceProfile"
    value     = aws_iam_instance_profile.ec2_instance_profile.name
  }
  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "SecurityGroups"
    value     = aws_security_group.eb_security_group_web.id
  }
  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "LoadBalancerType"
    value     = "application"
  }

  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "ServiceRole"
    value     = aws_iam_role.eb_service_role.name
  }

  setting {
    namespace = "aws:elasticbeanstalk:healthreporting:system"
    name      = "SystemType"
    value     = "basic"
  }

  # setting {
  #   namespace = "aws:elbv2:listener:443"
  #   name      = "Protocol"
  #   value     = "HTTPS"
  # }

  # setting {
  #   namespace = "aws:elbv2:listener:443"
  #   name      = "ListenerEnabled"
  #   value     = "true"
  # }

  setting {
    namespace = "aws:elbv2:listener:80"
    name      = "DefaultProcess"
    value     = "default"
  }

  setting {
    namespace = "aws:elbv2:listener:80"
    name      = "Protocol"
    value     = "HTTP"
  }

  setting {
    namespace = "aws:elbv2:listener:80"
    name      = "ListenerEnabled"
    value     = "true"
  }

  setting {
    namespace = "aws:elbv2:loadbalancer"
    name      = "SecurityGroups"
    value     = aws_security_group.eb_security_group_lb.id
  }

  # setting {
  #   namespace = "aws:elbv2:listener:443"
  #   name      = "SSLCertificateArns"
  #   value     = "arn:aws:acm:eu-west-1:574836245203:certificate/51456bea-3d96-4f9d-a893-904c29d58afe" # Replace with your SSL certificate ARN
  # }

  # Optional: redirect HTTP to HTTPS
  # setting {
  #   namespace = "aws:elbv2:listener:80"
  #   name      = "Rules"
  #   value     = "path-pattern / -> forward: 443, path-pattern /* -> redirect: https://rudolph-sucks.projects.bbdgrad.com#{path}?#{query}"
  # }

}








