# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      # version = "~> 2.65"
      version = "~> 3.0.2"
    }
  }
  required_version = ">= 0.14.9"
}
provider "azurerm" {
  features {}
}

# Create the resource group
resource "azurerm_resource_group" "rg" {
  name     = "Web-App-Terraform-RG"
  location = "eastus"
}
# Create the win App Service Plan
resource "azurerm_service_plan" "appserviceplan" {
  name                = "webapp-asp-devopscourse"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type = "Windows"
  sku_name            = "P1v2"
}
# Create the web app, pass in the App Service Plan
resource "azurerm_windows_web_app" "webapp" {
  name                = "webapp-app-devopscourse"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id = azurerm_service_plan.appserviceplan.id

    site_config {}

  app_settings = {
    "SOME_KEY" = "some-value"
  }

  connection_string {
    name  = "DefaultConnectionString"
    type  = "SQLServer"
    value = "Server=tcp:devopscourse-qa.database.windows.net,1433;Initial Catalog=devopscourse_qa;Persist Security Info=False;User ID=mradwan;Password=Password@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

resource "azurerm_mssql_server" "sqlserver" {
  name                         = "devopscourse-qa"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "mradwan"
  administrator_login_password = "Password@123"
}
resource "azurerm_mssql_firewall_rule" "firewallrule" {
  name             = "FirewallRule1"
  server_id        = azurerm_mssql_server.sqlserver.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "sqldb" {
  name                = "devopscourse_qa"
  server_id           = azurerm_mssql_server.sqlserver.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  tags = {
    environment = "qa"
  }
}