<h1 align="center">
Real Time Chat
</h1>
A real time chat using C# dotnet, SignalR, RabbitMQ, Identity and some other technologies and patterns. This is a coding challenge.

## Code Challenge

##### Assignment
The goal of this exercise is to create a simple browser-based chat application using .NET.
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
	<li> Build an installer. <b>=> I used docker-compose to build and run the application, i'm not sure if that counts</b></li>
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
These are all the technologies and patterns i used to develop this application
##### BackEnd
<ul>
	<li>C# .NET 6.0 Web API</li>
	<li>SignalR</li>
	<li>RabbitMQ</li>
	<li>Identity</li>
	<li>DDD</li>
	<li>CQRS</li>
	<li>Mediator</li>
	<li>Middlewares: Error, Request and Response</li>
	<li>FluentValidation</li>
	<li>AutoMapper</li>
	<li>Dependency Injection</li>
	<li>MassTransit</li>
</ul>

##### FrontEnd
<ul>
	<li>C# .NET 6.0 Web</li>
	<li>Blazor</li>
	<li>CSS</li>
	<li>Bootstrap</li>
	<li>Javascript</li>
</ul>

## Requirements
To execute and run the application you will need to download the following:
<ul>
	<li>.NET 6.0 SDK</li>
	<li>Docker</li>
	<li>SQLServer</li>
</ul>

## Executing the Project
To execute the project, follow the steps below:
