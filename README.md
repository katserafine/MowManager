# MowManager
## .NET Core Customer Relationship Management Portal for Mow Managers, LLC.
### Tools for Development
(This development guide assumes you are using the latest version of Mac OSX)
To get started you need the following tools:
1. [Docker](https://docs.docker.com/docker-for-mac/install/)
2. [VSCode](https://code.visualstudio.com/)
3. [DBMgmt](https://google.com)
4. [Google Chrome](https://www.google.com/chrome/)
5. [SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

The 5th tool will become optionally downloadable with the implementation of the debugger injection within the docker.yaml workflow

### Get up and running!
1. Open your terminal and do the following:

   // Install homebrew if not on computer
   
                sudo /usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"

  // Install github cli tools if not on computer
  
                brew install git

  // List all files
                
                ls

  // Navigate to Documents directory
  
                cd Doc + tab key // OR
                cd Documents

  // Create Dev directory
                
                mkdir Dev
                cd Dev

  // Create dotnet directory
                
                mkdir dotnet
                cd dotnet

  // Pull github repo
                
                git clone https://github.com/dgonzo27/MowManager

  // Change into directory
                
                cd MowManager

  // Open in VSCode (see below if first time vscode use)
                
                code .

If this is your first time using VSCode, the "code" command will not be recognized by your terminal application.  You will need to open VSCode and once it has loaded hit CMD + Shift + P.  A search bar will drop down from the top center of the application window.  Type "Shell Command" and several filtered results will return.  Select the one that says "Install 'code' command in PATH".  Quit the VSCode application with CMD + Q and go back to your terminal.  Run 'code .' to open your vscode editor in your current directory.

2. From your terminal application, run:

                docker-compose up

This will fetch the base image and explicitly defined dependencies from our Dockerfile in addition to starting and connecting your db and web server containers.
By default, the web server should fail when trying to connect to your database, because the database being referenced does not yet exist.  

3. Open your db management application and import the dump file shared by the team member.  

4. Open Google Chrome and type http://localhost:8080 to reach the homepage of our application.  If nothing is found, then we are still in early development and you will need to go to http://localhost:8080/api/pricing to hit an API endpoint to return some json.

5. From your terminal application, hit:

                CTRL + C

This key combo will _gracefully_ quit your application.  

Read below to get a better understanding of the tools used to develop this application.

## What is Docker?

Docker is a **containerization** platform, meaning that it enables you to **package** your applications into **images** and run them as "**containers**" on any platform that can run Docker. 

If you are familiar with _object oriented programming_ principles, you can think of **images** like a _Class_ and **containers** like an _object_ or instance of that class.  

## What does our Dockerfile define?

A Dockerfile is the blueprint which is passed to the _Docker Engine_ at build time.  The file is parsed into separate commands that are run in an empty container (most cases a linux-based container) and the result is a _Docker Image_ that can be used to spin up and run container instances of our application.

To generate the _leanest_ image possible, we are using a "multi-stage build" architecture in our Dockerfile.  This means that the image built locally on our machines for development is different from the image we want to push to production - again we want a lean production image.  This has been previously achieved by writing multiple Dockerfiles to control different types of builds.  The beauty of a Dockerfile - when written correctly - can understand the requirements for different builds and manage these complexities for us, as you will see in the explanation below.

Line 2 pulls the full sdk from microsoft as the base image in order to build our dotnet app

        FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

Line 3 creates a working directory inside our empty linux container called app, this is where our source code will be stored

        WORKDIR /app

Line 6 copies the project files into the root directory of our linux container

        COPY *.csproj ./

Line 7 updates our application with the dotnet restore command

        RUN dotnet restore

Line 10 copies the current directory into our linux container

        COPY . ./

Line 11 publishes our dotnet application and puts the resulting output into a directory called "out"

        RUN dotnet publish -c Release -o out

Line 14 signals our multi-stage build with the FROM command and pulls the minimal aspnet image for our production image

        FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

Line 15 creates a working directory in our production empty linux-based container with the same convention as above - app

        WORKDIR /app

Line 16 exposes port 80 of our container so that services can attach to the application instance

        EXPOSE 80

Line 17 copies the build output from our base image into the root directory of our production container

        COPY --from=build-env /app/out .

Line 18 defines our container entrypoint by referencing the dotnet command for execution, and our project .dll file

## MowManager - Docker Architecture

For smaller applications - maybe a Single-Page React Application - you can run one application, in one container and leave it up to tools like Kubernetes or ECS in AWS to orchestrate how those applications are served to the user.  For our application - specifically an MVC application - we will rely heavily on API calls to serve pages or _Views_ to our users.  This means that we will need a container running our API or Web Application, and another container running a database.  We will be using SQLServer at the time of writing.

In order to orchestrate the use of both containers in our local development environments, we will use a _Method of Networking Containers_, see below.

### Method of Networking Containers

There are only two methods supported at the time of writing:
1. Software Defined Networks (go look up on your own) and;
2. Use Docker Compose

You guessed it, we're using Docker Compose.

### What is Docker Compose?

* Reduces reliance on, and simplifies the use of the Docker Command Line; ex:

                docker run -p 8080:80 dgonzo27/mowmanager

* Allows us to start up multiple containers quickly (web and db)

* Allows us to set up connections between containers (again - web and db)

We can use _docker-compose up_ to start our application locally with all services, and we can use _docker-compose build_ to rebuild our images.

Utilizing Docker Compose initially requires building Docker container/images with traditonal methods.  See below.

## Docker Build

The Docker _build_ command is used for building our image from the Dockerfile generated above.  To do so, you can navigate to your terminal and run:

        docker build -t [Docker Hub Username]/[Name of Proj (lowercase)]:[Version] .

The naming convention for images begins by referencing a docker hub username that you have access to, or optionally naming it whatever you would like if you don't want to have builds in your Docker Hub.  Followed by a '/' project name in lower case.  This is important because it will not work if it contains uppercase or unknown characters.  Followed by a : with the version.  By default, Docker will tag the image as 'latest' if you do not provide a version number, which is what we'll be using.  Best practices suggest that we should explicitly define the version - even if using latest - so I have done so.

To list your built images you can run:

        docker images

In your terminal to see your locally built or hub connected images

## Docker Run

Now that we have successfully built our image, the next step is to start up our container and run the instance.  To do this run:

        docker run -p 8080:80 dgonzo27/mowmanager

The -p flag tells our local machine that we want to map port 8080 (or any arbitrary port) to the port that was exposed in our container - defined in our Dockerfile.  Lastly, we append the name of our image that we want to run as a container instance.  

## Docker Hub

If you named your image based on your Docker Hub username, you can now create a repository for this image and push the build to the web.  To do this, run:

        docker push dgonzo27/mowmanager

If you have not logged in yet, it may ask you to authenticate - in which you should reference your username and password to login.

## API Model Generation within a containerized .NET Application

When working with a containerized solution, the requirements for generating new services - such as a model - may vary from step-to-step.

The creation of your model follows standard .NET procedures; navigate to the _Models_ folder, create a new class - _Pricing.cs_ - and create a public class, with attributes inside the project namespace.

Once done, you will need to create a database context class so that the model can be accessed from our db.  In the _Models_ folder, create a new class - _PricingContext.cs_ - add the using statement for entity framework core, and reference Model as a DbSet.  You can reference an existing file for an example. 

After that, you will need to add the context to the the ConfigureServices method of the _Startup.cs_ file.  Connection strings to our database will need to be passed correctly, but you can reference an existing db context option as an example.  This is traditionally done in the appsettings.json file, but is done here since our application is containerized.

Lastly, you will need to leverage the _code-first approach_ to migrate our new models into our database.  Start by creating a migration file by running the following command in your terminal > project directory:

        dotnet ef migrations add PricingModel

This generates the migration file for the database.  Ordinarily, you would run a command from the terminal to execute the migration file.  We don't have a database to execute that migration file in, since we are using a SQLServer container for the database.  For our local development environment, we have implemented a class that will set up a database if one does not exist, and execute any pending migrations that have not been executed in the container.  

Obviously, this is not to be implemented in a production environment.  We don't want our application connecting, referencing, and creating new migrations or models upon application startup.  

Should you ever need to generate a new SQLServer database in a docker container, you can do so by running the following command in your terminal > project directory:

        docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<password>' -e 'MSSQL_PID=Express' -p 1427:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu

The __ACCEPT_EULA__ flag accepts the license agreement, __MSSQL_SA_PASSWORD__ is the system administrator password, __MSSQL_PID__ is the version being used, and the -p flag maps our external port - 1427 - to our internal port, also defined 1433 in this example. The -d flag is referencing the SQLServer image from microsoft and running the container in "detatched" mode, so that other services - our application - can connect to it.

