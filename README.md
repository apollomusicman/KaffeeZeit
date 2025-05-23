# KaffeeZeit
A web applicate to help you and your coworkers decide who's turn it is to buy coffee.

# Build
KaffeZeit requires [.NET 9.0.300 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) and [ASP.NET Core Runtime 9.0.5](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) to build and run 

Once the .NET 9 SDK is installed the project can be built at the solution directory with this command:
```
dotnet build
```
The project is current configured to be built in Debug. the .dlls and exe will be written to the following directory:
```
\KaffeeZeit\KaffeeZeit.Server\bin\Debug\net9.0
```
Oncec built it can be run with the following command from the `\KaffeeZeit\KaffeeZeit.Server` directory:
```
dotnet run
```
The default client server should be reachable now at:
```
https://localhost:50577
```

# Steps to use

- Add coworkers 
- Get the list of current running tabs. 
- Create a new order, users can either order their favorite drink or enter a custom amount if a different drink is ordered. 
- Choose a payer, this can be either the user deemed next, or can be overridden if someone else wants to pickup the next tab.  
- Get the new list of current running tabs. 

# Design choices

- Designed as a web application with a RESTful webserver so that ideally it could be hosted on the web. That way users could access the web app from their phones on the go, or even from the coffee shop, and not have to come back to the server to log orders and payments.
- Orders have a revision numbers, this is to prevent two orders from coming in and overriding each other. 
- Orders must be paid before a new order can be created.

# What could be improved
- The app could be fault tolerant and store tab state in a permanent storage such as a yaml/json file or use a light weight database. 
- Input validation. There is a minimal amount of validation that should prevent the most common issues. Although there are certainly corner cases not accounted for. 
- GUI design. It's minimal and functional, but not the most user friendly, especially on mobile which would be the ultimate goal for useability.
- the app would also need to be changed into release to deploy in a production environment.  
- Authn/Authz right now it's assumed all users act in good will and do not purposefully enter erroreous information.
- edit tab information like coworkers name, favorite drink cost, and potentially the amount  of their running tab.
- Sometimes people want to pay for birthdays/special events, you could add or remove an amount to a tab.
