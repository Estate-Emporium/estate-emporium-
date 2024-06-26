
output "db_instance_address" {
  value = aws_db_instance.db.address
}

output "github_action_role_arn" {
  value = aws_iam_role.github_action_role.arn
}

output "aws_region" {
  value = var.region
}

output "server_release_bucket_name" {
  value = aws_s3_bucket.server_release_bucket.bucket
}

output "website_url" {
  value = aws_s3_bucket.website_bucket.website_endpoint
}

output "db_endpoint" {
  value = aws_db_instance.db.endpoint
}

output "db_port" {
  value = aws_db_instance.db.port
}

output "server_cer" {
  value = aws_acm_certificate.sever_cert.certificate_body
}

output "server_cert" {
  value = aws_acm_certificate.website_cert.certificate_body
}

output "server_app_name" {
  value = aws_elastic_beanstalk_application.server_app.name
}

output "server_env_name" {
  value = aws_elastic_beanstalk_environment.server_env.name
}


