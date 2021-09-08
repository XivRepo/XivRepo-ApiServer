# XivRepo-ApiServer

## Installation Requirements
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Entity Framework Core Tools
  - Can be installed by running `dotnet tool install --global dotnet-ef`
- [MySql Workbench](https://www.mysql.com/products/workbench/) (Optional)

## Running Locally
- Copy `.env.example` file's contense into new file named `.env`
- Startup the database:
  - Run `docker-compose up` from the root directory. This will start a docker container for a MySQL Server. 
  If you would like to run your own DB, just run it on local host and update the `.env` file with the login
  and database information.
- Seed the database
  - Navigate to the Entity Framework project: `cd XIVRepo.EntityFramework`
  - Run migrations: `dotnet ef database update`
- Obtain Login Token:
  - Start the `XIVRepo.Authorization` project
  - If it is your first time running or the database is newly created, execute the `/api/auth/register`
  call. This will generate a user account for usage.
  - Now that an account exists, execute the `/api/auth/login` endpoint. This will output a string which is
  your login token. This token will be valid for a week at which point will need to be regenerated.
- Run the API
  - Start the `XIVRepo.GraphQL` project
  - Upon starting, it will open the GraphQL playground in your browser. By default the API is open to all without 
  authorization, but some data may be unable to be obtained without a proper login. To add your login token,
  open the HTTP Headers menu and add the following replacing `<token>` with the string you obtained earlier:
    ```json
    {
      "Authorization": "Bearer <token>"
    }
    ```
    
## Handling Mirgations
For our database we use Entitry Framework Core alongside MySQL Server. Below is a list of helpful commands you can 
run from the `XIVRepo.EntityFramework` project:
- Run Migrations: `dotnet ef database update`
- Create New Migration `donet ef migrations add <MigrationName`
- Delete Last Migration `donet ef migrations remove`
  - You will be unable to remove a migration that has already been run against the database
- Revent Migrations on Database: `donet ef database update <name of migration you want to rollback to>`
    
## Development Libraries
Below is a list of libraries we are using to make the app run. Please refer to each of their document for details on implementation:
- [Entity Framework Core v5](https://docs.microsoft.com/en-us/ef/core/)
- [HotChocolate v12](https://chillicream.com/docs/hotchocolate)
