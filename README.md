# Autobooks Coding Challenge
This project is for a simple grocery store api for the Autobooks coding challenge. 
The project lets you interact with customer data for a grocery store database.
This includes basic functions for a customer such as get, add, update, etc.

In the appsettings.json file, set Grocery.DatabasePath to the file path of the json file for the database you wish to use.

Database schema:
```json 
{
  "customers": [ {
    "id": 0,
    "name": "string"
  } ]
}
```

## How to run
Option 1: Open in Visual Studio and run using IIS Express or by running Autobooks.API

Option 2: Open the Autobooks.API.csproj project folder, and run the following command within the terminal.

`dotnet run --project Autobooks.API.csproj`
