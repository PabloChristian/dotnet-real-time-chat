<h1 align="center">
⭐ Real Time Chat⭐ 
</h1>
💬 A real time chat using C# dotnet, SignalR, RabbitMQ, Identity and some other technologies and patterns. This is a coding challenge 👨‍💻

## Code Challenge

##### Assignment
📌 The goal of this exercise is to create a simple browser-based chat application using .NET.
This application should allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

##### Mandatory Features
<ul>
	<li>✔ Allow registered users to log in and talk with other users in a chatroom.</li>
	<li>✔ Allow users to post messages as commands into the chatroom with the following format /stock=stock_code</li>
	<li>✔ Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code).</li>
	<li>✔ The bot should parse the received CSV file and then it should send a message back
	into the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.</li>
	<li>✔ Have the chat messages ordered by their timestamps and show only the last 50
messages.</li>
	<li>✔ Unit test the functionality you prefer.</li>
</ul>

##### Bonus (optional)
<ul>
	<li>✔ Have more than one chatroom.</li>
	<li>✔ Use .NET identity for users authentication.</li>
	<li>✔ Handle messages that are not understood or any exceptions raised within the bot.</li>
	<li>⚠️ Build an installer. <b>=> I used docker-compose to build and run the application, i'm not sure if that counts</b></li>
</ul>

##### Considerations
<ul>
	<li>✔ We will open 2 browser windows and log in with 2 different users to test the
functionalities.</li>
	<li>✔ The stock command won’t be saved on the database as a post.</li>
	<li>✔ The project is totally focused on the backend; please have the frontend as simple as you
can.</li>
	<li>✔ Keep confidential information secure.</li>
	<li>✔ Pay attention if your chat is consuming too many resources.</li>
	<li>✔ Keep your code versioned with Git locally.</li>
	<li>✔ Feel free to use small helper libraries.</li>
</ul>

## Demonstrations
I have recorded some GIFs to demonstrate how the application works:

##### Register and Login

##### Two different users logged and chatting

##### Stock Command

## Technologies and Patterns
🛠 These are all the technologies and patterns i used to develop this application
##### BackEnd
<ul>
	<li><a url="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">C# .NET 6.0 Web API</a></li>
	<li><a url="https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR">SignalR</a></li>
	<li><a url="https://www.nuget.org/packages/MassTransit.RabbitMQ/8.0.6-develop.537">RabbitMQ</a></li>
	<li><a url="https://www.nuget.org/packages/Microsoft.AspNetCore.Identity">Identity</a></li>
	<li><a url="https://www.nuget.org/packages/MediatR">Mediator</a></li>
	<li><a url="https://www.nuget.org/packages/FluentValidation">FluentValidation</a></li>
	<li><a url="https://www.nuget.org/packages/AutoMapper">AutoMapper</a></li>
	<li><a url="https://www.nuget.org/packages/MassTransit/8.0.6-develop.537">MassTransit</a></li>
	<li><a url=""></a>DDD</li>
	<li><a url=""></a>CQRS</li>
	<li><a url=""></a>Middlewares: Error, Request and Response</li>
	<li><a url=""></a>Dependency Injection</li>
</ul>

##### FrontEnd
<ul>
	<li><a url="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">C# .NET 6.0 Web</a></li>
	<li><a url="https://docs.microsoft.com/pt-br/aspnet/core/blazor/?view=aspnetcore-6.0">Blazor</a></li>
	<li><a url="https://www.w3schools.com/css/">CSS</a></li>
	<li><a url="https://getbootstrap.com/">Bootstrap</a></li>
	<li><a url="https://developer.mozilla.org/pt-BR/docs/Web/JavaScript">Javascript</a></li>
</ul>

## Requirements
To execute and run the local application, you will need to download and install the following:
<ul>
	<li><a url="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">.NET 6.0</a></li>
	<li><a url="https://docs.docker.com/desktop/#download-and-install">Docker Desktop</a></li>
	<li><a url="https://docs.docker.com/compose/install/compose-desktop/">Docker Compose</a></li>
	<li><a url="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads">SQLServer</a></li>
</ul>

## Executing the Project
To execute the project, follow the steps below:
1. Run Docker Desktop.
2. Open the command prompt (cmd), navigate inside the project "\src" folder, and type: "docker-compose build" to build the application.
3. Type "docker-compose up -d" to start the application containers.
4. Now you can execute the application:
   4.1 To run the Web Application, navigate to http://localhost:8080
   4.2 To run the Web Api, navigate to http://localhost:8082/swagger 
