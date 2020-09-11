# BugTrackManagment

Bug Tracking Mangment is a web-based application that is designed 
to help quality assurance and programmers keep track of reported 
software bugs in their work.

### Tech
* [ASP.NET Core 3.1](https://github.com/dotnet/aspnetcore) - a free and open-source web framework
* [Microsoft Visual Studio](https://visualstudio.microsoft.com) - awesome integrated development environment from Microsoft.
* [AdminLTE](https://adminlte.io/) - open source admin dashboard & control panel theme buit on top of Bootstrap
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server) - is a relational database management system developed by Microsoft.
* [KestrelHttpServer](https://github.com/dotnet/aspnetcore/tree/master/src/Servers/Kestrel) - is a cross-platform web server for ASP.NET Core.


### Responsive Design

This project is designed  using responsive design principles with the help of bootstrap framework.  


### Modules

**Account** 

This Module deals with Authentication of the users, that is verifying the identity of a user. This is done by comparing identifying credentials ( Username and Password)  with that of in a database


![Registration Page](https://drive.google.com/uc?export=view&id=1jBbtO1TCYk0i-9nRtv5PBJfK4dqd1HWU)

![Login Page](https://drive.google.com/uc?export=view&id=1IiLir8k1jBq3l4DWn3vmvaNsJ6VnFZFH)


**Roles** 
* Admin
* Tester
* Programmer

A user role defines permissions for users to perform a group of tasks.

![Manager Dashboard](https://drive.google.com/uc?export=view&id=1VdPYuSAnu5nb4Fmlh1I5zd29SB4RJ1dI)

![Tester Dashboard](https://drive.google.com/uc?export=view&id=1SCUMvsBuj2aQeWRK770FU6mwbrZ9dFFm)

![Programmer Dashboard](https://drive.google.com/uc?export=view&id=1gzJsVbHFC0lBLjxvsPHmk6kgF2vO4yVI)

###  miscellaneous 

**[Ajax Calls](https://en.wikipedia.org/wiki/Ajax_(programming))** short for "Asynchronous JavaScript and XML", can send and retrieve data from a server asynchronously (in the background) is used thourgh out the website for both funtionality and good UX.
* example(1) showing toasts when a user does a action like adding new issue or delteting etc. 
* example(2) on the Regestration page the input email address is searched in the Database if it is already in use or not, this is all done with out page reolad in backgroud(asynchronously )
* etc



