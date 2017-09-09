# ToDoListAzure
This is a simple proof of concept to display an Azure App Service website communciating with an API project, which communicates to an Azure SQL backend.  The app is a to-do application based on Microsoft's ToDo List app, but adapted for Azure deploy and to Visual Studio 2017 Preview.

![alt text](https://docs.microsoft.com/en-us/azure/app-service-api/media/app-service-api-dotnet-get-started/todospa.png)

The code is based on this tutorial here: https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-get-started

Software used in tutorial (other versions should work fine but this is what I used):
1. Visual Studio 2017 Preview
2. Sql Server Management Server 2016
3. Azure subscription
4. Visual Studio Team Services (VSTS) for CI/CD portion

This is an overview of what we will do:
Part 1:
1. Setup an Azure SQL database using the Azure Portal
2. Sign into the database using SQL Server Management Studio
3. Create a table with an ID and Description
4. Create 5 stored procedures
5. Pull down the GitHub project
6. Make sure it builds locally
7. Test the Azure project connects locally to the Azure SQL DB 
8. Deploy the API project to Azure and ensure it still connects to Azure SQL
8. Test the Web App can connect to tne Azure Web API project you just deployed 
9. Deploy the Web App
10. You should have a working app! Test it out by hitting the web front end website and checking your SQL DB!

Part 2:
1. Create a Visual Studio Team Services (VSTS) account. 

# Setup the Database


# Setup the Project


# Setup a VSTS Account


# Setup CI/CD in VSTS
