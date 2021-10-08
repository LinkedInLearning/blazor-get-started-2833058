# Blazor: Getting Started
This is the repository for the LinkedIn Learning course Blazor: Getting Started. The full course is available from [LinkedIn Learning][lil-course-url].

![Blazor: Getting Started][lil-thumbnail-url] 
Get started with Microsoft Blazor, the framework for building rich web apps with C# and .NET. Learn how Blazor allows .NET code to run in the browser, allowing programmers to leverage their existing .NET skills for front-end development. Explore the Blazor component model, including topics such as routing, dependency injection, data binding, and layouts. Then discover the debugging and unit testing workflow for Blazor apps, and see how to validate form data. Instructor Richard Goforth—a senior software architect and consultant—also shows how to implement authentication and authorization and interact with JavaScript code. Plus, learn how to take advantage of Blazor component libraries to save time and create your own libraries in order to package and reuse code across projects.

### Learning objectives
 - Creating a new Blazor project
 - Working with Blazor components
 - Routing in Blazor
 - Data binding and events
 - Debugging
 - Unit testing
 - Authorization in Blazor apps
 - Maintaining state
 - Calling JavaScript from Blazor
 - Creating and using component libraries

## Instructions
This repository has branches for each of the videos in the course. You can use the branch pop up menu in github to switch to a specific branch and take a look at the course at that stage, or you can add `/tree/BRANCH_NAME` to the URL to go to the branch you want to access.

## Branches
The branches are structured to correspond to the videos in the course. The naming convention is `CHAPTER#_MOVIE#`. As an example, the branch named `02_03` corresponds to the second chapter and the third video in that chapter. 
Some branches will have a beginning and an end state. These are marked with the letters `b` for "beginning" and `e` for "end". The `b` branch contains the code as it is at the beginning of the movie. The `e` branch contains the code as it is at the end of the movie. The `master` branch holds the final state of the code when in the course.

## Installing
1. To use these exercise files, you must have the following installed:
	- Local mssql server or [Docker Desktop](https://www.docker.com/products/docker-desktop) to run the sql container
	- [.net core sdk 3.1.301](https://dotnet.microsoft.com/download/dotnet-core/3.1)
2. Clone this repository into your local machine using the terminal (Mac), CMD (Windows), or a GUI tool like SourceTree.
3. Update the connection string in app.settings if you are using your own sql server

	OR
	
   `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=@#^fcIen&*asd" -p 1433:1433 --name sql -d mcr.microsoft.com/mssql/server`
    
    to run the sql server docker container
       
4. Run these commands from the folder root
	```
	dotnet restore
	dotnet tool restore
	dotnet ef database update --project Beam.Server/Beam.Server.csproj 
	```
	
# Blazor Beams!

Features
- Blazor WebAssembly Client
- Hosted in ASP.Net core, with a Web Api backend
- SQL Server Database
- EF Core

Develop, Build and Run in a container locally (with docker desktop and the [Remote - Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) extension or in [Codespaces](https://visualstudio.microsoft.com/services/visual-studio-codespaces/)


### Instructor

**Richard Goforth**

_Software Architect and Consultant_

[View on LinkedIn](https://www.linkedin.com/in/richard-goforth-83582b33/?trk=lil_instructor)

[Other Courses by the instructor](https://www.linkedin.com/learning/instructors/richard-goforth)

[0]: # (Replace these placeholder URLs with actual course URLs)

[lil-course-url]: https://www.linkedin.com/learning/blazor-getting-started/
[lil-thumbnail-url]: https://cdn.lynda.com/course/2833058/2833058-1597766168005-16x9.jpg
