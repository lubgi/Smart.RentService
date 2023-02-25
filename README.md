# Smart.RentService
## Test task

The customer owns a large number of production premises and plans to lease the space of these premises for the placement of technological equipment. In the context of this, there was a need for a service that would allow
administer contracts for the placement of equipment in the premises.

## QuickStart

```sh
git clone https://github.com/lubgi/Smart.RentService.git
cd Smart.RentService
docker compose up
```
That's it :) It will locally start your API and MS SQL Server

## Azure
Link: https://smart-rentservice-webapi.azurewebsites.net/

DB Connection:
```
Server=smartbusiness.database.windows.net;Database=Rent;User=serveradmin;Password=StrongDatabasePassword123@;
```

## Usage

After successful service startup navigate to local SwaggerUI:
- http://localhost:5000/swagger/index.html

For authorization use key from _appSettings.json_ (feel free to change it):

```json
"ApiKey": "b291631a-17af-4af7-8bf0-02f79b5c2524"
```

If you want to play with your own DB simply replace connection string:
```
"ConnectionStrings": {
    "DefaultConnection": "YOUR_WONDERFUL_CONNECTION_STRING"
  }
```

Enjoy using API!

## Project Structure

* **Src**
    * **WebAPI** 
        1. ApiController
        2. Authorization FIlter
        3. Exception Handling Middleware
        4. Some services registration
        5. SwaggerUI setups
    * **Infrastructure**
        1. DbContext
        2. Entity Models Configurations
        3. DbInitialiser (for seed purposes)
        4. Interceptors
        5. Migrations
    * **SharedKernel**
        1. Repository interfaces
        2. Entity Base classes
    *  **Core**
        1. Entities
        2. ~~Domain events etc~~ not yet :)
    * **Application**
        1. Common exceptions
        2. Validation and Exception Pipelines
        3. Commands and Queries
        4. Specifications
* **Tests**
    * Application Unit Tests

