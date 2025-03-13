## Instructions

The project was created following the guidelines:
- based on microservices
- based on a read-write database
- based on messages with rebus between microservices.

For development:
- added the template and performed refactoring to add context boundary
- created a project structure for context boundary development, thinking about microservices
- created a project structure for context boundary development, thinking about microservices
- created a base structure to share the basic architecture

# Project Structure

Project Structure:

- Base: basic architecture to avoid code duplication
- User: user context (template)
- Sale: sales context
    * Sale Command: responsible only for writing to the write database
    * Sale Query: responsible only for reading from the read database
    * Sale Query Consumer: responsible only for updating the read database, consuming changes made by the command
    * Sale Contracts: Shared interfaces.

## Instructions for running

# Run Docker:

Run RabbitMQ, open cli:  
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.0-management  

Run PostgreSql, open cli:  
docker run -d --name postgres-container -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=root -p 5432:5432 -v postgres_data:/var/lib/postgresql/data postgres

# Run Migration to Inicialize database:

WriteDb
* Step 1: go to backend\src\Sale\src\Ambev.Sale.Command\Ambev.Sale.Command.Infrastructure.Orm
* Step 2: open terminal
* Step 3: run scripts
    * dotnet ef migrations add InitialCreate
	* dotnet ef database update

ReadDb
* Step 1: go to backend\src\Sale\src\Ambev.Sale.Query\Ambev.Sale.Query.Infrastructure.Orm
* Step 2: open terminal
* Step 3: run scripts
    * dotnet ef migrations add InitialCreate
	* dotnet ef database update

# Run Backend:
* Step 1: go to template\backend\src
* Step 2: open soluction
* Step 3: run soluction (profile start PROJECTS)  
(Some cases need to restore. In this case open CLI in folder backend/src and run: dotnet restore Ambev.Backend.sln)

# Run FrontEnd:
* Step 1: go to template\frontend\src\AppClientSale
* Step 2: open soluction 
* Step 3: run soluction  
(Developed in a basic way for testing only)


