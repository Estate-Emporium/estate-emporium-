provider "aws" {
  region = var.region
}

provider "aws" {
  alias  = "global"
  region = "us-east-1"
}
