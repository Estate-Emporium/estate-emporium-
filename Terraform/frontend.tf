resource "aws_s3_bucket" "website_bucket" {
  bucket = "${var.project_name}-website" # Change to your unique bucket name
  tags   = merge(var.mandatory_tags, { Name = "${var.project_name}-website" })
}

resource "aws_s3_bucket_versioning" "source_versioning" {
  bucket = aws_s3_bucket.website_bucket.bucket
  versioning_configuration {
    status = "Enabled"
  }
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
  bucket              = aws_s3_bucket.website_bucket.id
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
