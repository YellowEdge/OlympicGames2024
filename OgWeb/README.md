# OlympicGames2024 (A basic ticketing app for my school) 📚🛒

    It is a project based on my school courses about method to manage and develop an application in .NET

## DISCLAIMER ⚠️

    [The author] assumes no responsibility or liability for any errors or omissions in the content of this site.
    The information contained in this site is provided on an "as is" basis with no guarantees of completeness, accuracy, usefulness or timeliness..."1
    
    Your use of this Website (or Mobile App) is wholly at your own risk. This Website (or Mobile App) is
    provided on an "as is" basis with no representation, guarantee or agreement of any kind as to its
    functionality.

## Git

    https://github.com/YellowEdge/OlympicGames2024

## Tech stack 🧑‍💻

   - Dotnet core
   - ASP .NET core MVC
   - ASP .NET core Razor Pages
   - ASP .NET core Identity
   - Entity Framework Core (ORM)
   - MS SQL (Database)
   - Bootstrap (frontend)

#### Note 📢

    There is a provided documentation to help users start using this application.


## How to run the project? 🌐

    I am assuming that, you have already installed **Visual Studio 2022** (It is the latest as of may,2024) and **MS SQL Server Management Studio** (I am using mssql server 2022 as of may,2024).

    Now, follow the following steps:

    1. Open command prompt. Go to a directory where you want to clone this project. Use this command to clone the project.
    
        ``` 
            git clone https://github.com/YellowEdge/OlympicGames2024
        ```
        
        Note : you can also use the UI from **Visual Studio 2022** to clone a repository

    2. Go to the directory where you have cloned this project, open the directory `OlympicGames2024`.
       You will find a file with name `OlympicGames2024.sln`. Double click on this file and this project will be opened in Visual Studio.

    3.  open `appsettings.json` file and update connection string (set your db server name)
       ```     
          "ConnectionStrings": {
            "DefaultConnection": "Server=YOURSERVERNAME;Database=OlympicDB;Trusted_Connection=True;TrustServerCertificate=True"
           }
       ```
       
    4. Delete `Migrations` folder
    
    5. Open Tools > Package Manager > Package manager console
    
    6. Run these 2 commands
        ```
         (i) add-migration init
         (ii) update-database
        ````
    
    7. Now you can run this project
    
## Data 📈📉

    I provided a dbseeder class to add some data in the tables automaticaly when you run the projet the first time.
    This will fill the tables with some data in order to have a functional application.    

## How to register - how to log as admin and login? 🧑‍💻

    There is a provided documentation to help users start using this application.
    
    For the administrator click on login use with these credentials.   
   ```
     username: admin@paris.jo
     password: Azerty01$
   ```
   
#### I hope, you will find something valuable from this work. It is just from a start course in dev.

    If you find this repository useful, then consider to leave a ⭐.
    Thanks a lot 🙂🙂