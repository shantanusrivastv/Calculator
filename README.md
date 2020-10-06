# UL Expression Calculator  #

## Setup Instruction ##

 ### Client App ###
 1. Open terminal/Command line 
 2. Go to the folder location
 3. run npm install
 4. Wait for dependencies to be installed
 5. run npm start
 6. By default app will run on http://localhost:3000/
 

 ### Service App ### 
 1. Open the solution in Visual Studio
 2. Open Package Manager Console
 3. run update-database
 4. Run the application (ctrl + F5)
 5. By default appliaction will run in localhost:57190
 6. By default application start will load Swagger UI at http://localhost:57190/index.html
 6. The same address is used in client app  
  **Optional** Change the connection string in appsettings.json to Instance of Sql Server.
   By default it will run using localdb

 ### Login Information ##
 The databse is seeded with the following Login
##  Premium User:
	username: s.Thompson@ul.com
	password: admin 
		
##  Standard User:
	username: s.Gibbens@ul.com
        password: admin 

## Free User:
        username: free@free.com
        password: user 
