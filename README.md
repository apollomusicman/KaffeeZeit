# KaffeeZeit
A web applicate to help you and your coworkers decide who's turn it is to buy coffee.

# Build
KaffeZeit requires [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) to build and run 

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