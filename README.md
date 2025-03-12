## Instructions

The project was created following instructions.

I followed the following premises for development:
- based on microservices
- based on a read and write database
- based on messaging with rebus between microservices.

For development:
- added the template and performed refactoring to add future projects based on microservices/context
- created a project structure for developing a specific context, thinking about microservices
- created a project structure for testing a specific context, thinking about microservices
- created a project for writing and another query following CQRS premises to also test messaging that was built in a basic way using a rebus ROM that is responsible for updating the records recorded in the writing database to the reading database
- created a base project to share the basic architecture

## Instructions for running

# Run Docker:

Run RabbitMQ, abrir cli:  
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.0-management  

Run PostgreSql, abrir cli:  
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
* Step 3: run soluction (perfil star PROJECTS)

# Run FrontEnd:
* Step 1: go to template\frontend\src\AppClientSale
* Step 2: open soluction 
* Step 3: run soluction  
(Developed in a basic way for testing only)


