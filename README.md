# ToDoListAzure
This is a simple proof of concept to display an Azure App Service website communciating with an API project, which communicates to an Azure SQL backend.  The app is a To-Do application based on Microsoft's To-Do List app, but adapted for Azure deploy and to Visual Studio 2017 Preview.  The project is C#, Angular JS, and SQL. 

The primary objective of the tutorial is to show you how to deploy an Azure App Service Website + App Service API + Azure SQL and how to get it all working with a simple To-Do application. Also I am reviving the older post to update the project to Visual Studio 2017.  I hope that this is helpful to either enterprise web developers about using Azure and linking a 3 part project, or if you are a beginner this is a good project for you to learn how to code and how to use Azure.  I will only address how to deploy and setup CI/CD in this tutorial. If you are new to coding and wanted to go through what the code is actually doing this is a great beginner project to learn API's and how a simple end-to-end solution works. 

![alt text](https://docs.microsoft.com/en-us/azure/app-service-api/media/app-service-api-dotnet-get-started/todospa.png)

The code is based on this tutorial here: https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-get-started

Software used in tutorial:
* Visual Studio 2017 Preview (I am sure any version of VS2017 is fine).
* Sql Server Management Server 2016 (I am sure any reasonably current SSMS version is fine).
* Git Bash
* Azure subscription
* Visual Studio Team Services (VSTS) for CI/CD portion

### Overview of guide:
**Part 1: Setup the Azure SQL Database**
1. Setup an Azure SQL database using the Azure Portal.  
2. Sign into the database using SQL Server Management Studio
3. Create a table with an ID and Description
4. Create 5 stored procedures

**Part 2: Setup the C# Project**
1. Pull down the GitHub project
2. Make sure it builds locally
3. Test the Azure project connects locally to the Azure SQL DB 
4. Deploy the API project to Azure
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
2. Create a new SQL Database. Click "+ New", select Databases, choose SQL Database, then lastly hit Create. 
      ![Alt text](/Images/Part1/01SelectSQLDBPortal.png?raw=true "Azure Portal: how to select a SQL DB")
3. Fill in all of the highlighted inputs.  Click on Server and Pricing Tier to get slideout options.  In the Server slideout, make sure you create a username and password that you will remember, you will need this to login using SQL Server Management Studio (SSMS).  In the Pricing Tier, change to Basic so it only costs about $5 per month. Your screen will look approximately like this:
      ![Alt text](/Images/Part1/02DBOptions.png?raw=true "Creating a SQL DB")
4. Click on All Resources on the left menu. You should see your new SQL Server and SQL Database. Click on the SQL Database. 
      ![Alt text](/Images/Part1/03AllResources.png?raw=true "Select your SQL DB")
5. You land on the Overview tab.  Save the Server name from the main Overview tab of your new Azure SQL DB. Click on the Show Connection Strings hyperlink highlighted.
      ![Alt text](/Images/Part1/05DatabaseOverview.png?raw=true "SQL DB Overview tab")
  The connection string page will look like this (save this in a Notepad for the web.config in the solution later):
      ![Alt text](/Images/Part1/06ConnectionString.png?raw=true "SQL DB Overview tab")
6. Open SSMS and enter the Server name, username, and password. 
      ![Alt text](/Images/Part1/07SSMS.png?raw=true "SSMS Login")
7. Create a new table that looks exactly like the following format (two columns which are "ID" and "Description", "ID" should be an int and "Description" should be type VARCHAR(500)).. you can make the description anything you'd like: 

      ID | Description
      ------------ | -------------
      1 | Buy ketchup
      2 | Eat a hot dog
      3 | Hug a puppy

      ![Alt text](/Images/Part1/08Table.png?raw=true "Table Structure")

8. Download this whole project, either as a zip file or using Git.  
9. Create the 5 stored procedures which are found in the root project folder/**StoredProcedures** 
(Open Stored Procedure in SSMS and hit Execute and since they are "Create Stored Proc" types you should see them appear in the Stored Proc folder as shown in screenshot in next step): 
10. Your Azure SQL database should look something like this now: 
      ![Alt text](/Images/Part1/09StoredProcs.png?raw=true "SSMS Stored Procedures")

# [Part 2]: Setup the C# Project
1. Download this project if you haven't already (your should have in Part 1, step 9 though). 
2. Build the project.
3. Set the ToDoListDataAPI project as the Startup Project. 
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
16. Let's publish the API project, stop running it.  Right click on the API project adn choose Publish.
      ![Alt text](/Images/Part2/08.png?raw=true "Publishing the API project")
17. Choose an App Service.
      ![Alt text](/Images/Part2/09.png?raw=true "Publishing the API project")
18. Fill in all the settings: add in a name, choose the subscription, create a new resource group. For the App Service Plan: choose a name, the closest location to you and Free. Then on the main modal click Create. 
      ![Alt text](/Images/Part2/10.png?raw=true "Publishing the API project")
      ![Alt text](/Images/Part2/11.png?raw=true "Publishing the API project")
19. Make sure it shows up in the Azure Portal after giving it a few minutes to publish. Click on the API project to go to the overview (red arrow).
      ![Alt text](/Images/Part2/12.png?raw=true "Publishing the API project")
20. Copy the URL of the API App Service as highlighted in the screenshot. 
      ![Alt text](/Images/Part2/13.png?raw=true "Connecting your local front end project to the Azure API Service")
21. Let's connect the front end to the API project.  Open up the ToDoListAngular solution.  Go to the web.config file of your front end ToDoListAngular project. Paste in the URL from the previous step. 
      ![Alt text](/Images/Part2/14.png?raw=true "Connecting your local front end project to the Azure API Service")
22. Run the front end as Startup. It should look like this and be connected to the Azure API Service.
      ![Alt text](/Images/Part2/18.png?raw=true "Connecting your local front end project to the Azure API Service")
21. Let's do the same publishing to Azure for the front end project.  Repeat steps 16-18 for the front end ToDoListAngular project. 
22. Verify once you are done Publishing that it is in the Azure Portal.  Click on the APp Service (red arrow in screenshot). 
      ![Alt text](/Images/Part2/15.png?raw=true "Connecting your local front end project to the Azure API Service")
23. On the Overview page, get the URL and copy it. 
      ![Alt text](/Images/Part2/16.png?raw=true "Connecting your local front end project to the Azure API Service")
24. Paste the URL into your browser and click on the Todo tab to see the Todo list. You should now have a working Azure App Service Web  front end talking to an Azure App Service API which connects to Azure SQL. 
      ![Alt text](/Images/Part2/17.png?raw=true "Connecting your local front end project to the Azure API Service")
      
# [Part 3]: Setup a VSTS Account, Multiple Environments, and Continuous Integration
1. Create a VSTS account by hitting Sign In on the top right of the page at this URL: https://www.visualstudio.com/team-services/
2. Create a new VSTS Account by hitting the button on the top right. 
      ![Alt text](/Images/Part3/00.png?raw=true)
3. Pick a name for your account, hit Continue, and make the account.  
      ![Alt text](/Images/Part3/01.png?raw=true)
4. Create a new Project using Git version control and Agile called ToDoListAngular.
      ![Alt text](/Images/Part3/02.png?raw=true)
5. Add the code for ToDoListAngular. Go to the root folder of the ToDoListAngular solution (should look like below), right click and select Git Bash. *Note if you pulled this down in as a zip file, go to the next step.  If you pulled it down using Git Bash originally, make Hidden Folders visible, then delelte the .git folder before continuing.*
      ![Alt text](/Images/Part3/03.png?raw=true)
Also get the URL of your Project by going to the Code tab:
      ![Alt text](/Images/Part3/03b.png?raw=true)
6. Run the following commands in order, one by one. Make sure on the 4th command you change the URL to your project URL. 
      ```
      git init
      git add .
      git commit -m "My first commit. Adding Todo project."
      git remote add origin https://YOURPROJECT.visualstudio.com/_git/AzureWebApp
      git push -u origin --all
      ```

7. Go to VSTS and refresh, you should see your code there:
      ![Alt text](/Images/Part3/04.png?raw=true)
8.  Repeat steps 4-7 for the ToDoListDataAPI project. 
9.  Under the Code tab for each of your two Solutions, click on the Branches sub-tab. Click on New Branch. Create a Dev, QA, and Prod branch all based off master branch. Right click on your old master branch, and delete it.
      ![Alt text](/Images/Part3/05.png?raw=true)
      ![Alt text](/Images/Part3/06.png?raw=true)
10. Right click on Prod, and select Branch Security.  You can assign groups to manage your branches.  I recommend giving a DevOps team and the architects/senior lead devs the Prod rights to allow merges into it.  I recommend that QA have rights over their branch so that they can choose when they are ready to recieve new changes, and that the developers let QA know when they are done with a solid build and the build #.  Optionally, the DevOps team can manage the QA branch also.  Dev branch should be free for all the developers on the team to run the CI/CD process on their own.  We will not choose any of these options for now as this project includes only you, but please be aware of it in an enterprise setting. 
      ![Alt text](/Images/Part3/07.png?raw=true)
11. Right click on Prod, and select Policies.  Check the box for Protect this Branch to see all the options.  This will allow you to have required pull requests, a certain number of reviewers, build validation, automatically include some reviewers, and other settings.  Please review the options here.  We will not choose any of these options for now as this project includes only you, but please be aware of it in an enterprise setting. Navigate away from this page by clicking Build & Release and do not save anything. 
      ![Alt text](/Images/Part3/08.png?raw=true)
12. On the Build and Release tab, click "+ New" 
      ![Alt text](/Images/Part3/09.png?raw=true)
13. Choose the Azure Web App template and hit Apply. 
      ![Alt text](/Images/Part3/10.png?raw=true)
14. On the next landing page,  
      ![Alt text](/Images/Part3/11.png?raw=true)
      ![Alt text](/Images/Part3/11b.png?raw=true)

Go to the Build tab.
Create a new Build.
Click on the Triggers tab.
Check the box so it will create a build with every check in.
You have now setup the Dev build. 
Go to the QA environment. Repeat the process for this enviroment and Prod. 

# [Part 4]: Setup VSTS Continuous Deployment, Test Multiple Environments end to end
1. First, go to the Azure Portal and upgrade your two deployments to Standard or Premium in order to get slots.  Standard is cheaper and will work perfectly fine, as it has 5 slots.  
2. Create slots for Dev, QA, and Staging in both of your App Services. 
3. Go to the Releases tab in VSTS
4. Hit Create a new release
5. Create a Release definition for Dev
6. Add an artifact that connects to your Dev build. 
7. Add a Dev environment. 
8. Click the Tasks tab to edit the tasks under the Dev environment. 
9. On the first task, -- and add dev as the slot. 
10. Delete the second task. 
11. Save the task.
12. Run the release.
13. Go back to the Azure Portal. Click on the dev slot. Get to the Overview page and find the URL for it. Click on the URL and ensure that it was deployed successfully. 
14. Repeat steps 3-13 for the QA environment, but for the QA build and release. 
15. Repeat steps 3-9 for the Staging environment, then continue to the next step. 
16. For Staging, click on the second task and we will want to make sure the box is checked to swap with the Production enviroment. 


*Research tags
research web config for different environments in build
