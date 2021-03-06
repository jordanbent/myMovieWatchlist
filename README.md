# myMovieWatchlist
This is my implementation of a CRUD application. With this web application a user creates a list item and within a list item a user then creates a movie item, that is associated with the list. All items and their data is readable, editable and deletable.

# Table of Contents
1. [Technologies Used](#technologiesused)
2. [Development](#development)
    
    2.1 [Requirements](#requirements)
    
    2.2 [User Stories](#userstories)

    2.3 [Trello Board](#trelloboard)
3. [Architecture](#architecture)

    3.1 [Database](#database)
    * Entity Relationship Diagram 
    * Current Live Database

    3.2 [CI Pipeline](#ci)
4. [Risk Assesment](#risk)
5. [Testing](#test)
6. [Front-End](#frontend)
7. [Issues and Improvements](#issues)

## Technologies Used <a name="technologiesused"></a>
This application is a ASP.NET Core MVC Web Applcation with an Azure Database, deployed on Azure App Services.
* ASP.NET
* C#
* Git
* Azure MySQL Database
* Azure App Service
* Azure DevOps
* Trello

## Development <a name="development"></a>

### Requirements <a name="requirements"></a>
* List Item
    * Create List
    * View Lists
    * Edit List
    * Delete List
    * Add Movie to List
* Movie Item 
    * Create Movie
    * View Movies in List
    * Edit Movie
    * Delete Movie
### User Stories <a name="userstories"></a>
* As a movie watcher, I want to be able to add movies to my movie watchlist, so that I can always have something to watch.

* As a movie watcher, I want to be able to edit or delete a movie I added to my movie watchlist, so that if I make any mistakes or change my mind it doesn't require much effort.

* As a movie watcher, I want to know if I have watched a movie or not, so that I can remember all the movies I want to watch and can look back on what I have already watched.

* As a user, I want a clean interface with no fuss, so that I can quickly and easily add/delete/edit/look at all the movies I have watched and want to watch.

### Trello Board <a name="trelloboard"></a>
![Trello Board](./imgs/trello.png)

## Architecture <a name="architecture"></a>
### Database <a name="database"></a>
#### Entity Relationship Diagram
The database changed once throughout development - with the addition of Year in Movie and the changing of typr for DateAdded to string from DateTime. There existsed a many-to-many relationship between list and movie so a third database was need in order to normalise the database. The MovieList table relates the two tables wihtout them relating to eachother.

![Entity Relationship Diagram](./imgs/erd.PNG)

#### Current Live Database
##### Lists
![List Database](./imgs/listDB.PNG)
##### Movies
Through development the DateAdded attribute was changed from a DateTime to a string for displaying purposes.

![Movie Database](./imgs/movieDB.PNG)
##### MovieLists
![Movie List Database](./imgs/movielistDB.PNG)

### CI Pipeline <a name="ci"></a>
This was the aimed CI Pipeline implementation and nearly every aspect was a success, failing at the publish cycle in Azure Pipelines.

![CI Pipeline](./imgs/ci.PNG)

Unfortunately after publishing through Azure pipelines there is an error:
![CI Pipeline Error](./imgs/errorCI.PNG)

So I had to Publish through the Visual Studio publisher. 
![Publish](./imgs/publish.PNG)

* Developer: I as the developer interact with Visual Studio to create the WebApp adn test it.
* Visual Studio: This is the IDE to implement all code. From here the project is pushed to a GitHub repository for version control.
* GitHub: GitHub holds the project in a repository. It is connected to Azure Repos within Azure DevOps. 
* Azure Repos: This holds a pointer to the GitHub repository with the project in it.
* Azure Pipelines: This is triggered when code is committed to main. My pipeline onyl manages to build the project to ensure it is compilable. Ideally, it would restore, build and publish the project to the WebApp.
* Azure App Service: This hosts the WebApp online at https://mymoviewatchlist.azurewebsites.net. This allows users to interact with the WebApp. 

## Risk Assesment <a name="risk"></a>
A lot of the risk associated with this project came from it's nature of being online and the risks that come with cloud hosting. There is never full security that the WebApp is not at risk, so there are some precautions to take. 
 
![Risk Assesment](./imgs/riskassesment.PNG)

## Testing <a name="test"></a>
All controllers and models were tested using xUnit testing with a code coverage report generated by the extension Fine Code Coverage.

The testing of the models included:
* Null test
* Attribute tests
* Constructor test

The testing of the controllers included:
* Repositroy Patterns
* Mocking using Moq
* Controller Tests
* Null Test for each Function

Testing revealed issues I had in my code with poor exception handling. I fixed the issues with some simple conditional statements. 

![Code Coverage Report](./imgs/codecoveragereport1.PNG)
![Code Coverage Report2](./imgs/codecoveragereport2.PNG)

## Front-End <a name="frontend"></a>
When accessing the WebApp, the user is brought to the homepage that introduces the List CRUD functionailty. From the hompage, providing a list has been created, the user can navigate to the list page where all movies associated with the list are displayed.
### Homepage
![Homepage](./imgs/homepage1.png)
### Create a List Page
![Add A List](./imgs/homepage_addlist.png)
### Homepage Showing New List
![Homepage With New List](./imgs/homepage_addlist2.png)
### Update List Page
![Update List Page Old Info](./imgs/homepage_updatelist1.png)
![Update List Page New Info](./imgs/homepage_updatelist2.png)
### Homepage With Updated List
![Homepage with Updated List](./imgs/homepage_deletelist1.png)
### Deleted List
![Code Coverage Report](./imgs/homepage_deletelists2.png)

### List Page 
Accessed through clicking a list on the homepage.
![List Page - Empty](./imgs/listpage_home.png)
### Create a Movie Page
![Add A List](./imgs/listpage_addMovie.png)
### List Page with Added Movie
![List Page with Added Movie](./imgs/listpage_withmovei.png)
### Update Movie Page
![Update Movie Page](./imgs/listpage_updatemovie1.png)
### Updated Movie
![Updated Movie](./imgs/listpage_updatedmovie.png)
### Deleted Movie
![Deleted Movie](./imgs/listpage_deletedmovie.png)

## Issues and Improvements <a name="issues"></a>
### Issues
Issues I see:
* Not Publish Through Pipelines: As I couldn't get publishing through pipelines to work, there could be some long term CI/CD issues.
* Back Button Functionality: When using Chrome back buttons, after a delete for example, the same page is re-loaded making for a poor user experience. I added functioning back buttons to each page as a quick-fix but would like to work on a more robust solution.
* Redundant Data in Database: When adding movies there is no check to see if the movie added, already exists. While this is not a threat to the WebApp it is not the most ideal solution.

### Improvements
I would like to improve:
* The UI: Add some more unique features and styling ad it is currently quite basic.
* The Input: Ideally I would like to have the input areas as a partial view within the same page so the user isn't navigating between seperate pages.
* Edit of Movies: I would like to make the movie item editable within the movie view page, with the watched attribute like a radio button immitating a checklist.
* Add Publish Through Azure Pipelines: I would like to do it and get more comfortable with Pipelines in gerneral. I had a hard time with it with a lot of issues.

### Additional Features
I would like to add:
* All Movie Feature: At the moment movies are only viewable within a list. I would like to add functionality to see all of the users added movies in one view.
    * This would reveal additional issues with how movies are added to the database, as a movie is added without checking if that movie item already exists. That would need to be updated.
* Add Movie Information: It would be nice to have each movie to have a movie page, like the likes of IMDB, with photo, actor and director information.
* A lot more Styling: I would like to add a lot more styling to the page.
* Accounts: Ideally the WebApp would work best with each using having their own account as to have their own database. 