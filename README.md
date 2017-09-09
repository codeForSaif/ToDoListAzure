# ToDoListAzure
This is a simple proof of concept to display an Azure App Service website communciating with an API project, which communicates to an Azure SQL backend.  The app is a to-do application based on Microsoft's ToDo List app, but adapted for Azure deploy and to Visual Studio 2017 Preview.

![alt text](https://docs.microsoft.com/en-us/azure/app-service-api/media/app-service-api-dotnet-get-started/todospa.png)

The code is based on this tutorial here: https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-get-started

Software used in tutorial (other versions should work fine but this is what I used, note I will not be covering how to install these and will assume you have them already or can find another tutorial if needed to install these):
* Visual Studio 2017 Preview
* Sql Server Management Server 2016
* Azure subscription
* Visual Studio Team Services (VSTS) for CI/CD portion

### Overview of guide:
**Part 1: Setup the Azure SQL Database**
1. Setup an Azure SQL database using the Azure Portal
2. Sign into the database using SQL Server Management Studio
3. Create a table with an ID and Description
4. Create 5 stored procedures

** Part 2: Setup the C# Project **
1. Pull down the GitHub project
2. Make sure it builds locally
3. Test the Azure project connects locally to the Azure SQL DB 
4. Deploy the API project to Azure and ensure it still connects to Azure SQL
5. Test the Web App can connect to tne Azure Web API project you just deployed 
6. Deploy the Web App
7. You should have a working app! Test it out by hitting the web front end website and checking your SQL DB!

** Part 3: Setup a VSTS Account, Multiple Environments, and Continuous Integration **
1. Create a Visual Studio Team Services (VSTS) account. 
2. Setup 3 environments: Dev, QA, and Prod
3. Add code for all 3 environments
4. Setup a Continuous Integration Build that triggers on check-in to the code source

**Part 4: Setup VSTS Continuous Deployment, Test Multiple Environments end to end **
1. Setup Continuous Deployment that will deploy to each respective environment once a Successful Continuous Integration Build has been completed
2. Test checking into Dev and see the whole process publish a change to the Dev published website
3. Merge Dev into QA and view the whole process change into the QA published website

# [Part 1]: Setup the Azure SQL Database
1. Go to the Azure Portal at portal.azure.com
2. Create a new SQL Database
3. 
4. Make sure you create a username and password that you will remember, you will need this to login using SQL Server Management Studio. 

# [Part 2]: Setup the C# Project
1. Download the 

# [Part 3]: Setup a VSTS Account, Multiple Environments, and Continuous Integration
1. 

# [Part 4]: Setup VSTS Continuous Deployment, Test Multiple Environments end to end
1. 
