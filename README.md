# DxMood
https://dxmood.mystrikingly.com/  

A diagnosis assistance and recording tool for doctors. 

DXMood is a fullstack app with a C# .NET backend and an HTML and Javascript frontend

An API can be exposed at /swagger/index.html

## Install

All you need to install for the project to work is the [.NET framework](https://dotnet.microsoft.com/en-us/download)
    
## Clone

    $ git clone https://[LASTNAME][FIRSTNAME]-admin@bitbucket.org/esof423-drrx-bitk/dxmood-bitk.git

## Start

    $ cd DxMood
    $ dotnet run
    
## File Structure

- wwwroot: Static Frontend Code
- Controllers: C# Backend Code
- Services: C# Backend Code

## API

We use Swagger API to help visualize our data models.

When running a localhost this can be accessed at /swagger/index.html

## Database

Initial Product was set up with a public database for presentation and testing purposes ONLY. To fully implement this product for it to work in the future, a new server and database will need to be set up. Here are the instructions to add a new Azure Database to the backend. Future Authentication steps may need to be taken to properly connect the database. 

    - Create a new server on Azure Services
    
    - Create a new database in that Server
    
    - Copy the connection string that is given to you and paste it into “appSettings.json” in the DxMood Project folder.
    
    - Replace ​​MultipleActiveResultSets=False -> True
    
    - Run the following migration commands in terminal
    
    - Dotnet ef migrations add init
    
    - Dotnet ef database update

