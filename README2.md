
This is a simple proof of concept to display an Azure App Service website communicating with an API project, which communicates to an Azure SQL backend.  The app is a To-Do application based on Microsoft's To-Do List app (https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-get-started), but adapted for Azure deploy and to Visual Studio 2017.  The project technology stack is C#, Angular JS, Swagger, Dapper, and SQL. The primary objective of the tutorial is to show you how to deploy an Azure App Service Website + App Service API + Azure SQL and how to get it all working with a simple To-Do application. 

This tutorial is split into parts so that you can choose what you would like to complete. Depending on your interest, for example, you can complete Part 1 and Part 3, or just Part 1 and Part 2.

Timing Estimates to complete full tutorial: 
* Senior dev with Azure and VSTS experience: 1 hour
* Senior dev with no Azure and VSTS experience: 2 hours
* Junior dev: 3 - 5 hours

Azure Credit Costs (total is $5 per month): 
* Each Basic DB costs $5 per month 
* Basic App Services cost $0 (free)
* VSTS account costs $0 for up to 5 Basic users (free)

### Overview of guide:
**Part 1: Setup the C# Project, learn Swagger, and Deploy it manually to Azure**

*The web project consists of a Web App and an API app that communicate.  Note the API app is currently hardcoded with values since it does not yet have a database connection.  You will test the Web App can connect to the Azure Web API project locally and on the cloud.  You will also learn how to manually deploy your Web App and Web API to the cloud.* 

      Pre-requisites for Part 1:
      * Visual Studio 2017 (other versions should work fine, but the tutorial will use 2017)
      * Azure subscription
      * Swagger

**Part 2: Setup a VSTS Account with Continuous Integration and Continuous Delivery (Build and Release)**

*Create a Visual Studio Team Services (VSTS) account.  Add code using Git Bash.  Setup a Continuous Integration Build that triggers on check-in to the code source. Setup Continuous Deployment that will deploy to your Azure instance.*

      Pre-requisites for Part 2:
      * Azure subscription
      * Visual Studio Team Services (VSTS) for CI/CD portion
      * Git Bash

**Part 3: Setup the Azure SQL Database and learn Dapper**

*The app has some hardcoded values that we will remove and setup with Azure SQL using Dapper. Setup an Azure SQL database using the Azure Portal.  Sign into the database using SQL Server Management Studio. Create a table with an ID and Description and create 5 stored procedures which are available in this repo.*

      Pre-requisites for Part 3:
      * Azure subscription
      * SQL Server Management Server 2016 (other versions should work fine, but the tutorial will use 2016)

**Part 4: Cleanup**
*This will be an explanation of costs of each service and how to remove everything -or- how to remove most of the costs and keep everything running in one environment for $5 a month if you want to keep this example active in your Portal.*


# [Part 0]: Software Installs / Setup
1. You can get Visual Studio 2017 Community for free here: https://www.visualstudio.com/downloads/
2. You can create a free Azure account with an MSDN / MyVisualStudio account or by signing up for a free month trial here: https://azure.microsoft.com/en-us/free/?v=17.39&WT.srch=1&WT.mc_id=AID631184_SEM_bp6n8E3v&lnkd=Google_Azure_
3. You can sign up for a free VSTS account here: https://www.visualstudio.com/team-services/
You need to make sure that you use the same email address for your Azure and VSTS account so they can automatically recognize each other's resources, otherwise you will need to add both emails to your Azure subscription. 

# [Part 1]: Setup the C# Project
1. Download this project if you haven't already (your should have in Part 1, step 9 though). 
2. Open two solutions (ToDoListAngular and ToDoListAPI folders each contain a .sln file). Build each of the solutions.
3. Go to the ToDoListDataAPI solution.. 
4. Run the project. 
5. Add "/swagger" to the end of your URL if it is not already there, you should see a page like this: 
      ![Alt text](/Images/Part2/01.png?raw=true "Swagger main page")
6. Go to your Azure Portal and get the connection string from your Azure SQL DB. You should have saved this in a Notepad from Part 1, step 5.
7. Add your connection string to your web.config file in your ToDoListDataAPI project.  Just delete whatever is in connectionString="" and add your value in. Don't forget to manually put in your username and password where it is blank! Your web.config file should look like this:
      ![Alt text](/Images/Part2/00.png?raw=true "Web config with Connection String")
8. Save, run your project again, and add the "/swagger" if needed. 
9. Try to run a GET all which is the first API on the page /api/ToDoList, you should see:
      ![Alt text](/Images/Part2/02.png?raw=true "More Swagger API calls..")
10. Try to run a POST, first click where the screenshot shows, and fill in an ID with 10 and any description you want.  You should see (also check in SSMS and refresh your table):
      ![Alt text](/Images/Part2/03.png?raw=true "More Swagger API calls..")
11. Try to run a GET all again, you should see your added value:
      ![Alt text](/Images/Part2/04.png?raw=true "More Swagger API calls..")
12. Try to run a PUT, again click to get the format from where it's shown in the screenshot and modify an existing record's description, remember the ID number is 10 for the next step:
      ![Alt text](/Images/Part2/05.png?raw=true "More Swagger API calls..")
13. Try to run a GET by ID, use 10:
      ![Alt text](/Images/Part2/06.png?raw=true "More Swagger API calls..")
14. Your SSMS table should look like this now:
      ![Alt text](/Images/Part2/07.png?raw=true "SSMS Table Updated")
15. You have now tested all of the APIs and seen that your API project is linked to your Azure SQL DB. 
16. Let's publish the API project, stop running it.  Right click on the API project and choose Publish.
      ![Alt text](/Images/Part2/08.png?raw=true "Publishing the API project")
17. Choose an App Service.
      ![Alt text](/Images/Part2/09.png?raw=true "Publishing the API project")
18. Fill in all the settings: add in a name, choose the subscription, create a new resource group. For the App Service Plan: choose a name, the closest location to you and Free. Then on the main modal click Create. 
      ![Alt text](/Images/Part2/10.png?raw=true "Publishing the API project")
If you are on the ToDoListAPI project, make sure you have API selected. 
      ![Alt text](/Images/Part2/18.png?raw=true "Publishing the API project")
If you are on the ToDoListAngular project, make sure you have Web App selected. 
      ![Alt text](/Images/Part2/19.png?raw=true "Publishing the API project")
      ![Alt text](/Images/Part2/11.png?raw=true "Publishing the API project")

19. Make sure it shows up in the Azure Portal after giving it a few minutes to publish. Click on the API project to go to the overview (red arrow).
      ![Alt text](/Images/Part2/12.png?raw=true "Publishing the API project")
20. Copy the URL of the API App Service as highlighted in the screenshot. 
      ![Alt text](/Images/Part2/13.png?raw=true "Connecting your local front end project to the Azure API Service")
21. Let's connect the front end to the API project.  Open up the ToDoListAngular solution.  Go to the web.config file of your front end ToDoListAngular project. Paste in the URL from the previous step. 
      ![Alt text](/Images/Part2/14.png?raw=true "Connecting your local front end project to the Azure API Service")
22. Run the ToDoListAngular solution. It should look like this and be connected to the Azure API Service.
      ![Alt text](/Images/Part2/18.png?raw=true "Connecting your local front end project to the Azure API Service")
21. Let's do the same publishing to Azure for the front end project.  Repeat steps 16-18 for the front end ToDoListAngular project. 
22. Verify once you are done Publishing that it is in the Azure Portal.  Click on the APp Service (red arrow in screenshot). 
      ![Alt text](/Images/Part2/15.png?raw=true "Connecting your local front end project to the Azure API Service")
23. On the Overview page, get the URL and copy it. 
      ![Alt text](/Images/Part2/16.png?raw=true "Connecting your local front end project to the Azure API Service")
24. Paste the URL into your browser and click on the Todo tab to see the Todo list. You should now have a working Azure App Service Web  front end talking to an Azure App Service API which connects to Azure SQL. 
      ![Alt text](/Images/Part2/17.png?raw=true "Connecting your local front end project to the Azure API Service")
 

# [Part 4]: Removing partial or all resources / saving costs
Current costs of operation for keeping this tutorial in Azure ~$100 (or credits) per month:

VSTS is free with your "MSDN"/"My Visual Studio" subscription. 

You have two options: 
1. Remove all associated resource groups to delete everything = $0. 
2. If you want to keep most of the process intact in one environment: downgrade your two Azure App Services back to Free (which will remove your slots for Dev/QA/staging), and remove the Dev and QA versions of the DB. You will now just have the production slot to experiment with for a total cost of $5 per month of Azure credits =). Note your VSTS extra environments will no longer work, you can only use the Prod Build and Release process now. 


