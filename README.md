# MyChannel

MyChannel is a backend project for a video streaming service. The project consists of three projects: Application, Minimal-api, and Web-api. Application contains the business logic and data access. Minimal-api and Web-api are two API interfaces that allow access to the application's functionality.

Both Minimal-api and Web-api run on port 7096.

## Project for engineering thesis

MyChannel is a project created for an engineering thesis titled "Comparison of ASP.NET Web API and Minimal API based on a video streaming service." The project aims to compare the performance and efficiency of the two APIs.

## Requirements
- .NET Core 6 SDK

## Installation and running

1. Clone the repository:
git clone https://github.com/domwitmax/mychannel-backend-app
2. Navigate to the project directory:
cd MyChannel
3. Run the application:
dotnet run --project Application
4. Open Minimal-api or Web-api in your browser:
Minimal-api: http://localhost:7096
Web-api: http://localhost:7096
## Configuration
The application uses SQL Server database. To configure the database connection, add a `ConnectionStrings` section to the `appsettings.json` file in the Application project. In the `ConnectionStrings` section, set the values for the `Server`, `Database`, `User Id`, and `Password` properties.

Example database connection configuration:
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=MyChannel;User Id=username;Password=password;"
}
## Libraries
The MyChannel project uses the following libraries:
- AutoMapper - a library that allows mapping objects between layers of the application
- Microsoft.AspNetCore.Authentication.JwtBearer - a library that handles JWT (JSON Web Token) authentication and authorization
- Microsoft.EntityFrameworkCore - a library that enables object-relational mapping (ORM) and access to the database
- Microsoft.EntityFrameworkCore.Design - a library that allows using EF Core tools such as database migrations
- Microsoft.EntityFrameworkCore.Relational - a library that contains base functions for database providers
- Microsoft.EntityFrameworkCore.SqlServer - a SQL Server database provider for EF Core
- Microsoft.EntityFrameworkCore.Tools - EF Core tools such as database migrations available in the command-line interface

## Author
- Author: Dominik Witkowski (https://github.com/domwitmax)