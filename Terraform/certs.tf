resource "aws_acm_certificate" "sever_cert" {
  domain_name       = "estate-emporium-api.bbdsoftware.com" # Change to your domain
  validation_method = "DNS"
}

resource "aws_acm_certificate" "website_cert" {
  domain_name       = "estate-emporium.bbdsoftware.com" # Change to your domain
  validation_method = "DNS"
}