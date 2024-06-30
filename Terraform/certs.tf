resource "aws_acm_certificate" "sever_cert" {
  domain_name       = "sales.projects.bbdgrad.com" # Change to your domain
  validation_method = "DNS"
  provider          = aws.global
}

resource "aws_acm_certificate" "website_cert" {
  domain_name       = "api.sales.projects.bbdgrad.com" # Change to your domain
  validation_method = "DNS"
}
