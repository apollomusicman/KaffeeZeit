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

# UI walkthrough

Lauch the UI by navigating a browser to the hosted app.  

![This is what a first time launched application looks like](/img/00-new-instance.png)  

Currently the only thing that works in this state is the "Add Coworker button" go ahead and add a few! 

![Oh no, we don't have any tabs. We need to add a coworker or two.](/img/01-Add%20coworker.png)  

Enter the coworkers name and favorite drink cost one at a time, push the "Submit" button when you are ready to create the coworker that was entered. 

![That's better, it's good to have people to work and get coffee with](/img/02-coworkers-added.png)  

Now that we have some coworkers with tabs lets use the "New Order" button to create a new order.  

![Let's get some coffee!](/img/03-order-started.png)  

Select the checkbox next to their name to include that coworker in the order.  You can also choose to use their favorite drink, or if they want to order something different. Click the "Submit" button when you are ready to commit to this order.  

![Woah! Jay you sleeping okay at night?](/img/04-order-selections.png) 

The order has now been submitted, you can see whos turn it is next by the yellow "next" icon that appears on someones tab.  

![Jay you're up! Are you okay taking this round?](/img/05-order-submitted.png)  

Now it's time to choose who is going to pick up this round.  

![Just because Jay is next doesn't  mean he's on the hook](/img/06-select-payer.png)  

Select only one coworker to pay for this round, don't worry if you accidentally pick two, the app will remind you to only pick one. 

![Thanks, dude! IOU1, Jordan you've got the next round.](/img/07-payment-submited.png)  

You're all done and ready to create another order! Notice how the next tag has been applied to who is next to pay, and the tabs have been updated with who still owes what.  

if you do need to part ways with a coworker (or remove and re-add since update isn't implemented yet) a coworker can be removed by pressing the "Remove Coworker" button.  Just select the checkboxes next to who you want to remove and click the "Submit" button.  

![No worries, no one is leaving just yet](/img/08-remove-coworker.png)  

# Design choices

- Designed as a web application with a RESTful webserver so that ideally it could be hosted on the web. That way users could access the web app from their phones on the go, or even from the coffee shop, and not have to come back to the server to log orders and payments.
- Orders have a revision numbers, this is to prevent two orders from coming in and overriding each other. 
- Orders must be paid before a new order can be created.
- Coworkers can be added or removed at any time. 
- Not all coworkers have to be on every order. If someone is out sick or misses coffee day their tab won't be changed. 

# What could be improved
- The app could be fault tolerant and store tab state in a permanent storage such as a yaml/json file or use a light weight database. 
- Input validation. There is a minimal amount of validation that should prevent the most common issues. Although there are certainly corner cases not accounted for. 
- GUI design. It's minimal and functional, but not the most user friendly, especially on mobile which would be the ultimate goal for useability.
- the app would also need to be changed into release to deploy in a production environment.  
- Authn/Authz right now it's assumed all users act in good will and do not purposefully enter erroreous information.
- edit tab information like coworkers name, favorite drink cost, and potentially the amount  of their running tab.
- Sometimes people want to pay for birthdays/special events, you could add or remove an amount to a tab.
- Coworkers can be removed before they've zeroed out their tab.  
- REST api docs. 