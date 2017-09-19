### Shared Info
    <add name="todoItems" connectionString="Server=tcp:cattodolistdb.database.windows.net,1433;Initial Catalog=todolistdb;Persist Security Info=False;User ID=super;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" providerName="System.Data.EntityClient" />

ToDoListAzure
This is a simple proof of concept to display an Azure App Service website communicating with an API project, which communicates to an Azure SQL backend.  The app is a To-Do application based on Microsoft's To-Do List app, but adapted for Azure deploy and to Visual Studio 2017 Preview.  The project is C#, Angular JS, and SQL. The primary objective of the tutorial is to show you how to deploy an Azure App Service Website + App Service API + Azure SQL and how to get it all working with a simple To-Do application. 

**Timing: If you are a senior dev who knows Azure and VSTS very well I think you could complete this whole setup in 2-3 hours. If you are a senior dev new to Azure and VSTS I think it could be ~4-6 hours to complete.**

![alt text](https://docs.microsoft.com/en-us/azure/app-service-api/media/app-service-api-dotnet-get-started/todospa.png)

The code is based on this tutorial here: https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-get-started

Software used in tutorial:
* Visual Studio 2017 Preview (I am sure any version of VS2017 is fine).
* SQL Server Management Server 2016 (I am sure any reasonably current SSMS version is fine).
* Git Bash
* Azure subscription
* Swagger
* Visual Studio Team Services (VSTS) for CI/CD portion

### Overview of guide:
**Part 1: Setup the Azure SQL Database**

*Setup an Azure SQL database using the Azure Portal.  Sign into the database using SQL Server Management Studio. Create a table with an ID and Description and create 5 stored procedures which are available in this repo.*

**Part 2: Setup the C# Project**

*Pull down the GitHub project and make sure it builds locally. Test the Azure project connects locally to the Azure SQL DB and deploy the API project to Azure. Test the Web App can connect to the Azure Web API project you just deployed. Deploy the Web App. You should have a working app! Test it out by hitting the web front end website and checking your SQL DB! You can stop at this point if you just want to see an Azure deployment and are not interested in CI/CD.* 

**Part 3: Setup a VSTS Account, Multiple Environments, and Continuous Integration**

*Create a Visual Studio Team Services (VSTS) account. Setup 3 environments: Dev, QA, and Staging/Prod. Add code for all 3 environments. Setup a Continuous Integration Build that triggers on check-in to the code source.

**Part 4: Setup Dev, QA, and Staging/Prod environments for all tiers**

*Setup slots for Dev/QA/Staging/Prod in the Azure App Service.  Setup 3 DBs for each respective environment.*

**Part 5: Setup VSTS Continuous Deployment, Test Multiple Environments end to end**

*Setup Continuous Deployment that will deploy to each respective environment once a Successful Continuous Integration Build has been completed. Test checking into Dev and see the whole process publish a change to the Dev published website. Merge Dev into QA and view the whole process change into the QA published website.

**Cleanup**

*This will be an explanation of costs of each service and how to remove everything -or- how to remove most of the costs and keep everything running in one environment for $5 a month if you want to keep this example active in your Portal.*

# [Part 1]: Setup the Azure SQL Database
1. Go to the Azure Portal at portal.azure.com
2. Create a new SQL Database. Click "+ New", select Databases, choose SQL Database, then lastly hit Create. 
      ![Alt text](/Images/Part1/01SelectSQLDBPortal.png?raw=true "Azure Portal: how to select a SQL DB")
3. Fill in all of the highlighted inputs.  Click on Server and Pricing Tier to get slideout options.  In the Server slideout, make sure you create a username and password that you will remember, you will need this to login using SQL Server Management Studio (SSMS).  In the Pricing Tier, change to Basic so it only costs about $5 per month. Your screen will look approximately like this:
      ![Alt text](/Images/Part1/02DBOptions.png?raw=true "Creating a SQL DB")
4. Click on All Resources on the left menu. You should see your new SQL Server and SQL Database. Click on the SQL Database. 
      ![Alt text](/Images/Part1/03AllResources.png?raw=true "Select your SQL DB")
5. You land on the Overview tab.  Save the Server name from the main Overview tab of your new Azure SQL DB. Click on the Show Connection Strings hyperlink highlighted. Save the connection strings also, see image below: 
      ![Alt text](/Images/Part1/05DatabaseOverview.png?raw=true "SQL DB Overview tab")
  The connection string page will look like this (save this in a Notepad for the web.config in the solution later):
      ![Alt text](/Images/Part1/06ConnectionString.png?raw=true "SQL DB Overview tab")
6. Open SSMS and enter the Server name, username, and password. 
      ![Alt text](/Images/Part1/07SSMS.png?raw=true "SSMS Login")
      
      *Note if you cannot login because of an IP address, please go to the Portal and add your IP address as allowed by clicking on the SQL Server you created, then going to Firewall*
       ![Alt text](/Images/Part1/10.PNG?raw=true "SSMS Login")
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
      
# [Part 3]: Setup a VSTS Account, Multiple Environments, and Continuous Integration
1. Create a VSTS account by hitting Sign In on the top right of the page at this URL: https://www.visualstudio.com/team-services/
2. Create a new VSTS Account by hitting the button on the top right. 
      ![Alt text](/Images/Part3/00.png?raw=true)
3. Pick a name for your account, hit Continue, and make the account.  
      ![Alt text](/Images/Part3/01.png?raw=true)
4. Create a new Project using Git version control and Agile called ToDoListAngular.
      ![Alt text](/Images/Part3/02.png?raw=true)
5. Add the code for ToDoListAngular. Go to the root folder of the ToDoListAngular solution (should look like below), right click and select Git Bash. *Note if you pulled this down in as a zip file, go to the next step.  If you pulled it down using Git Bash originally, make Hidden Folders visible, then delete the .git folder before continuing.*
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
10. Right click on Prod, and select Branch Security.  You can assign groups to manage your branches.  I recommend giving a DevOps team and the architects/senior lead devs the Prod rights to allow merges into it.  I recommend that QA have rights over their branch so that they can choose when they are ready to receive new changes, and that the developers let QA know when they are done with a solid build and the build #.  Optionally, the DevOps team can manage the QA branch also.  Dev branch should be free for all the developers on the team to run the CI/CD process on their own.  We will not choose any of these options for now as this project includes only you, but please be aware of it in an enterprise setting. 
      ![Alt text](/Images/Part3/07.png?raw=true)
11. Right click on Prod, and select Policies.  Check the box for Protect this Branch to see all the options.  This will allow you to have required pull requests, a certain number of reviewers, build validation, automatically include some reviewers, and other settings.  Please review the options here.  We will not choose any of these options for now as this project includes only you, but please be aware of it in an enterprise setting. Navigate away from this page by clicking Build & Release and do not save anything. 
      ![Alt text](/Images/Part3/08.png?raw=true)
12. On the Build and Release tab, select Build, then click "+ New" 
      ![Alt text](/Images/Part3/09.png?raw=true)
13. Choose the Azure Web App template and hit Apply. 
      ![Alt text](/Images/Part3/10.png?raw=true)
14. On the next landing page, add a name for it as ToDoListAngular-Dev-CI.  Choose Hosted.  Add the .sln file by clicking the 3 dots and finding it. When you get to Azure Subscription, choose it from the dropdown, then hit Authorize.  Wait a minute, and it will populate the App Services you have. Choose the corresponding App Service. 
      ![Alt text](/Images/Part3/11a.png?raw=true)
15. Click on the Triggers tab. Check the slider to Enabled.  Choose to Include Develop, make sure it is the right branch. 
      ![Alt text](/Images/Part3/12.PNG?raw=true)
16. Click Save and Queue. You have now setup the Dev build. 
      ![Alt text](/Images/Part3/13.png?raw=true)
17. Repeat steps 12-16 for QA and Prod, make sure you associate the name of the build and the trigger with the correct branch name (QA and Prod). At the end your Build "All Definitions" tab should look like this:
      ![Alt text](/Images/Part3/15.PNG?raw=true)
18. Now, repeat steps 12-17 for the ToDoListDataAPI project. Switch projects by clicking top left name of project as in screenshot: 
      ![Alt text](/Images/Part3/16.png?raw=true)
19. Your ToDoListDataAPI project Builds should look like this:
      ![Alt text](/Images/Part3/17.PNG?raw=true)

# [Part 4]: Setup Dev, QA, and Staging/Prod environments for all tiers
1. First, go to the Azure Portal.  Go to the ToDoListAngular Web App, click on Scale up (App Service Plan), then choose S1 Standard.  (Standard is cheaper and will work perfectly fine, as it has 5 slots.  Choose S1 for the purposes of this tutorial.)
      ![Alt text](/Images/Part4/01.PNG?raw=true)
2. Create slots for Dev, QA, and Staging in both of your App Services. 
      ![Alt text](/Images/Part4/02.PNG?raw=true)
      ![Alt text](/Images/Part4/02b.PNG?raw=true)
3. Repeat steps 1-2 for you ToDoListDataAPI app. 
3. Go to your database.  Click Copy and create a Dev, QA, and Staging version, make sure you choose basic as the size. The existing one will be prod.  Make sure you choose the same server the original DB was one.  Keep the pricing tier as Basic so it is only $5 a month, this is the cheapest option. No elastic pool. 
      ![Alt text](/Images/Part4/03.PNG?raw=true)
      ![Alt text](/Images/Part4/03b.PNG?raw=true)
4. Login to SSMS for Dev and QA and add a Todo item for each that tells which environment you are currently in. 
5. Go to the Azure Portal and click all Resources as seen in first screenshot below.  Click on each staging slot for ToDoListDataAPI which should be Dev and QA and record the URL from the overview page. You will need these URLs for step 7.
      ![Alt text](/Images/Part4/07.png?raw=true)
      ![Alt text](/Images/Part4/07a.PNG?raw=true)
6. Go to the Azure Portal and click all Resources as seen in first screenshot below.  Click on each SQL DB environment instance: Dev, QA, and Staging. Click on Show connection strings.  Fill in the username and password of the connection string before recording it. You do not need prod as this one is already setup in the prod code environment. You will need the Dev and QA connection strings for step 9 and the Staging and Production connection strings for step 10.
      ![Alt text](/Images/Part4/08.png?raw=true)
      ![Alt text](/Images/Part4/08b.png?raw=true)
      ![Alt text](/Images/Part4/08c.png?raw=true)
7. In VSTS, for the ToDoListAngular QA branch change the web.config file to the ToDoListDataAPI URLs for the correct environment. Click on the Code tab. Click into the ToDoListAngular folder, then click on the web.config file. Click Edit on the top right of the file. Change the connection string to the toDoListAPIURL to the correct environment URL. Change to the Dev branch by clicking where the left arrow is pointed. Change the Dev connection string.
      ![Alt text](/Images/Part4/05.png?raw=true)
8. Switch projects to the ToDoListDataAPI project by clicking on the top left:
      ![Alt text](/Images/Part4/06.png?raw=true)
9.  In VSTS, for the ToDoListDataAPI QA branch change the web.config file to the ToDoListDataAPI URLs for the correct Azure DB connection String. Click on the Code tab. Click into the ToDoListAngular folder, then click on the web.config file. Click Edit on the top right of the file. Change the connection string to the toDoListAPIURL to the correct environment URL. Change to the Dev branch by clicking where the left arrow is pointed. Change the Dev connection string.
      ![Alt text](/Images/Part4/04.png?raw=true)
10. In the Azure Portal click on the ToDoListDataAPI staging app service.  Click on Application Settings.  Add the following Key to the App Settings plus the Staging DB Connection String.  Check the box next to the Key you just added for Slot Setting. Hit Save at the top.
      ![Alt text](/Images/Part4/09.png?raw=true)
11. Go to the ToDoListDataAPI app service (main production one).  Click on Application Settings.  Add the following Key to the App Settings plus the Staging DB Connection String.  Check the box next to the Key you just added for Slot Setting. Hit Save at the top.  Now, if you Swap Production and Staging the connection strings will still be correct. *Note you should keep Production and Staging DBs idential.  Also note, you added the connection strings to the code manually for Dev and QA because you will need them in the code base to work with locally.* 
      ![Alt text](/Images/Part4/10.png?raw=true)
12. Go to the ToDoListAngular staging app service in the Portal.  Add the following Key to the App Settings plus the Staging URL.  Check the box next to the Key you just added for Slot Setting. Hit Save at the top.
      ![Alt text](/Images/Part4/11.png?raw=true)
13. Go to the ToDoListAngular app service (main production one) in the Portal.  Add the following Key to the App Settings plus the Production URL.  Check the box next to the Key you just added for Slot Setting. Hit Save at the top.
      ![Alt text](/Images/Part4/12.png?raw=true)

# [Part 5]: Setup VSTS Continuous Deployment & Test Multiple Environments end to end
1. Go to the Releases tab in VSTS for the ToDoListDataAPI project and hit Create a new release
      ![Alt text](/Images/Part5/01.png?raw=true)
2. Choose the Azure App Service Deployment template. For the environment name, put Develop. Click on Policies, you can set these in the future when you add a manual approver. You can hit the "X" button to close the right slideout menu and continue setting up the Release Definition. 
      ![Alt text](/Images/Part5/02.PNG?raw=true)
      ![Alt text](/Images/Part5/02b.PNG?raw=true)
3. Click the pencil icon and change the name to "ToDoListDataAPI-CD-Dev".
      ![Alt text](/Images/Part5/03.PNG?raw=true)
4. Add an artifact that connects to your Dev build.  Choose your Dev CI build, it will add extra settings, leave these at default, then click Add. 
      ![Alt text](/Images/Part5/04.PNG?raw=true)
       ![Alt text](/Images/Part5/05.png?raw=true)
5. Click the Tasks tab to edit the tasks under the Dev environment. 
       ![Alt text](/Images/Part5/06.png?raw=true)
6. On the Dev Development Process (where you land) add your subscription and app service name. 
       ![Alt text](/Images/Part5/07.PNG?raw=true)
7. Click on the Deploy Azure App Service task.  Check the box for Deploy to slot. Add the resource group and the slot.  The slot for the dev one should be dev. All other settings on this task should remain at default. 
       ![Alt text](/Images/Part5/08.png?raw=true)
8. Click back to the Pipeline. Click the lightning icon on the artifact for the CD trigger to make it automatically deploy after a successful new build exists. Click to enable it. Choose the build branch to be develop to match the CD environment. 
       ![Alt text](/Images/Part5/12.png?raw=true)
8b. Click on the icon to the right side of the Environment square.  This will allow you to specify individuals or an entire group to approve or reject the deployment. We will not do antyhing here, this is just for your reference.  You should not set this up for Dev.  However, in QA or Prod it could be helpful for you to have a team of approvers who can "push the button" when they are ready to receive the QA or Prod deployment. 
        ![Alt text](/Images/Part5/28.png?raw=true)
9. Click on Variables.  We will not do anything here but I want to display what could be done here.  Variables here will be matched to their web.config name and the keys here will replace keys in the web.config file.  You can use this to replace environment connections during the build process if you wanted to have 1 Dev build push to QA then Prod and change the connection strings along the way. It depends on how you want to build your CI/CD process.  
       ![Alt text](/Images/Part5/10.png?raw=true)
10. Save the Release definition. Just hit OK with the modal that pops up. 
       ![Alt text](/Images/Part5/09.png?raw=true)
11. Repeat steps 1-10 for the QA and Staging environment, but for the QA or Staging build and release. 
12. Swap to the ToDoListAngular project. 
       ![Alt text](/Images/Part5/14.png?raw=true)
13. Repeat steps 1-11 for the ToDoListAngular project to setup Dev/QA/Staging environments. 
14. Let's test out deploying the Dev environment.  Make a change in VSTS to your ToDoListAngular code in the dev environment. Go into the following directories: ToDoListAngular > ToDoListAngular > app > views > Home.html (highlighted in screenshot).  Click Edit.  Change the words in the paragraph tag like adding "Hello Dev world" in there. Then see the 2nd screenshot, hit Commit. Hit commit when the modal pops up and don't change anything. (You could also go to VS and check in code to this branch but this is quicker for the purposes of the lab).
       ![Alt text](/Images/Part5/15.png?raw=true)
       ![Alt text](/Images/Part5/15b.png?raw=true)
15. Click on Build and Release > Builds.  You should see a Build in progress.  Click on the highlighted text with the # sign and date on it. You will see the progress of it, then it should show Succeeded at the end (2nd screenshot).
       ![Alt text](/Images/Part5/16.png?raw=true)
       ![Alt text](/Images/Part5/16b.png?raw=true)      
16. Click on Build and Release > Releases.  You should see a release in progress (has the blue icon with the play button).  Double click on the Release name which is highlighted in purple. You should see a Release succeed. You can hit Logs (where the arrow is in the 2nd screenshot) to see progress if it is still running or if you need to debug anything. 
       ![Alt text](/Images/Part5/17.png?raw=true)
       ![Alt text](/Images/Part5/17b.png?raw=true)      
17. Check the URL for Dev, get the URL from the Portal if you did not save it (see first screenshot).   
       ![Alt text](/Images/Part5/18.png?raw=true)
18. Your dev page should have your text added.  Also go to the ToDo list page and add some data to it. Refresh to ensure it saved. You can check in SSMS to ensure it saved correctly in the dev DB. 
       ![Alt text](/Images/Part5/19.png?raw=true)
20. Let's test out deploying to Staging and swapping it to Prod.  Make a change in VSTS to your ToDoListAngular in the production code.  Go into the following directories: ToDoListAngular > ToDoListAngular > app > views > Home.html (highlighted in screenshot).  Click Edit.  Change the words in the paragraph tag like adding "We are testing in Production ;)." in there. (You could also go to VS and check in code to this branch but this is quicker for the purposes of the lab. Also you don't have this branch pulled down locally so you would need to do that first).
       ![Alt text](/Images/Part5/20.png?raw=true)
19. Click on Build and Release > Releases. You should see a Build succeed.
20. Click on Build and Release > Releases. You should see a Release succeed.
21. Check the URL for Staging (go to the portal and overview and click on the URL).  Also go to the ToDo list page and add some data to it. Refresh to ensure it saved. You can check in SSMS to ensure it saved correctly in the staging DB optionally. 
       ![Alt text](/Images/Part5/21.png?raw=true)
22. Your Staging app page should look like this:
       ![Alt text](/Images/Part5/22.png?raw=true)
23. Check your production app page, it should be unchanged:
       ![Alt text](/Images/Part5/23.png?raw=true)
24. On your production app Overview, click on Swap.
       ![Alt text](/Images/Part5/24.png?raw=true)
25. Choose your source as the production slot and the destination as staging. Hit OK at the bottom.   Give it 2-3 mins to swap. Note the swap is seamless for the users, they will not notice downtime. You will get a notification in Azure when the swap has been completed successfully on the top right of the Portal. 
       ![Alt text](/Images/Part5/25.png?raw=true)
26. Check your production app page again, and it should have the text now. *You may need an incognito window or to clear your cache to see the change.*  Also go to the ToDo list page and check that the data you added to Staging is not visible here. 
       ![Alt text](/Images/Part5/26.png?raw=true)
27. Verify that your connection strings are still correct and going to the production API. We are ensuring the web config settings did not get swapped with the application. You don't want your production environment going to your staging DB!!! Click on Application Settings in your production CatToDoListAngular. 
       ![Alt text](/Images/Part5/27.png?raw=true)
28. Congratulations, you made it through the whole tutorial! You're done! :) 
       ![Alt text](/Images/Part5/Congratulations.png?raw=true)

# ARM Template - Adding Slots
Note, if you wanted to add Slots and the App Settings as "sticky" to stay with the slot you would need to generate the ARM template for the deployment and add the following into the ARM template for each slot.  You can see an explanation here: http://anthonychu.ca/post/azure-app-service-resource-templates-tips-tricks/ and at the bottom of the page there is a full GitHub JSON sample. 

```
"resources": [
        {
          "apiVersion": "2015-08-01",
          "name": "appsettings",
          "type": "config",
          "dependsOn": [
            "[resourceId('Microsoft.Web/Sites/Slots', variables('webSiteName'), 'Staging')]"
          ],
          "properties": {
            "AppSettingKey1": "Some staging value",
            "AppSettingKey2": "My second staging setting",
            "AppSettingKey3": "My third staging setting"
          },
      {
        "apiVersion": "2015-08-01",
        "name": "slotconfignames",
        "type": "config",
        "dependsOn": [
          "[resourceId('Microsoft.Web/Sites', variables('webSiteName'))]"
        ],
        "properties": {
          "appSettingNames": [ "AppSettingKey1", "AppSettingKey2" ]
        }
      }
    ]
```

# [Cleanup]: Removing partial or all resources / saving costs
Current costs of operation for keeping this tutorial in Azure ~$100 (or credits) per month:
Each Basic DB costs $5 per month, you have four. 
S1 App Services cost $40 per month each, you have two.  
VSTS is free with your "MSDN"/"My Visual Studio" subscription. 

You have two options: 
1. Remove all associated resource groups to delete everything = $0. 
2. If you want to keep most of the process intact in one environment: downgrade your two Azure App Services back to Free (which will remove your slots for Dev/QA/staging), and remove the Dev and QA versions of the DB. You will now just have the production slot to experiment with for a total cost of $5 per month of Azure credits =). Note your VSTS extra environments will no longer work, you can only use the Prod Build and Release process now. 


