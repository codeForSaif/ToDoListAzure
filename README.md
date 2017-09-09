# ToDoListAzure
This is a simple proof of concept to display an Azure App Service website communciating with an API project, which communicates to an Azure SQL backend.  The app is a To-Do application based on Microsoft's To-Do List app, but adapted for Azure deploy and to Visual Studio 2017 Preview.  

The primary objective of the tutorial is to show you how to deploy an Azure App Service Website + App Service API + Azure SQL and how to get it all working with a simple To-Do application. Also I am reviving the older post to update the project to Visual Studio 2017.  I hope that this is helpful to either enterprise web developers about using Azure and linking a 3 part project, or if you are a beginner this is a good project for you to learn how to code and how to use Azure.  I will only address how to deploy and setup CI/CD in this tutorial. If you are new to coding and wanted to go through what the code is actually doing this is a great beginner project to learn API's and how a simple end-to-end solution works. 

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

**Part 2: Setup the C# Project**
1. Pull down the GitHub project
2. Make sure it builds locally
3. Test the Azure project connects locally to the Azure SQL DB 
4. Deploy the API project to Azure and ensure it still connects to Azure SQL
5. Test the Web App can connect to tne Azure Web API project you just deployed 
6. Deploy the Web App
7. You should have a working app! Test it out by hitting the web front end website and checking your SQL DB!

**Part 3: Setup a VSTS Account, Multiple Environments, and Continuous Integration**
1. Create a Visual Studio Team Services (VSTS) account. 
2. Setup 3 environments: Dev, QA, and Prod
3. Add code for all 3 environments
4. Setup a Continuous Integration Build that triggers on check-in to the code source

**Part 4: Setup VSTS Continuous Deployment, Test Multiple Environments end to end**
1. Setup Continuous Deployment that will deploy to each respective environment once a Successful Continuous Integration Build has been completed
2. Test checking into Dev and see the whole process publish a change to the Dev published website
3. Merge Dev into QA and view the whole process change into the QA published website

# [Part 1]: Setup the Azure SQL Database
1. Go to the Azure Portal at portal.azure.com
2. Create a new SQL Database
3. 
4. Make sure you create a username and password that you will remember, you will need this to login using SQL Server Management Studio (SSMS). 
5. Save the Server name from the main Overview tab of your new Azure SQL DB. 
6. Open SSMS and enter the Server name, username, and password. 
7. Create a new table that looks exactly like the following (two columns which are "ID" and "Description", "ID" should be an int and "Description" should be type VARCHAR(500)): 

      ID | Description
      ------------ | -------------
      1 | Buy ketchup
      2 | Eat a hot dog
      3 | Hug a puppy

8. Download this whole project, either as a zip file or using Git.  
9. Create the 5 stored procedures which are found in the project folder: 
10. Your Azure SQL database should look something like this now: 

# [Part 2]: Setup the C# Project
1. Download this project if you haven't already (your should have in Part 1, step 9 though). 
2. Build the project.
3. Set the ToDoListDataAPI project as the Startup Project. 
4. Run the project. 
5. Add "/swagger" to the end of your URL if it is not already there, you should see a page like this: 
6. Go to your Azure Portal and get the connection string from your Azure SQL DB. It will be found:
7. Add this to your web.config file. Your web.config file should look like this:
8. Save, run your project again, and add the "/swagger" if needed. 
9. Try to run a GET all, you should see:
10. Try to run a POST, you should see (also check in SSMS and refresh your table):
11. Try to run a GET all again, you should see your added value:
12. Try to run a PUT:
13. Try to run a GET by ID:
14. Your SSMS table should look like this now:
15. You have now tested all of the APIs and seen that your API project is linked to your Azure SQL DB. 
16. Let's publish the API project, right click on the project
17. 

# [Part 3]: Setup a VSTS Account, Multiple Environments, and Continuous Integration
1. Create a VSTS account at this URL: 
2. Create 3 environments
3. Check in code to each one by .. 


# [Part 4]: Setup VSTS Continuous Deployment, Test Multiple Environments end to end
1. Go to the Releases tab
