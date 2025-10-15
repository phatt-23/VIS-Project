# MiniGitHub

## Domain layer patterns

- Transaction script
    - Organizes business logic by procedures where each procedure handles a single request from the presentation.
- Domain model
    - An object model of the domain that incorporates both behavior and data.
- Table module
    - A single instance that handles the business logic for all rows in a database table or view.
- Service layer
    - Defines an application's boundary with a layer of services that establishes a set of 
    available operations and coordinates the application's response in each operation.

## Data layer patterns

- Table data gateway
    - An object that acts as a gateway to a database table. One instance handles all the rows in the table.
- Row data gateway
    - An object that acts as a gateway to a single record in a data source. There is one instance per row.
- Active record
    - An object that wraps a row in a database table or view, encapsulates the database access, and adds domain logic on that data.
- Data mapper
    - A layer of mappers that moves data between objects and a database while keeping them independent of each other and the mapper itself.

## Object-relational behaviour

- Unit of Work
    - Maintains a list of objects affected by a business transaction and coordinates 
    the writing out of changes and the resolution of concurrency problems.
- Identity Map
    - Ensures that each object gets loaded only once by keeping every loaded object 
    in a map. Looks up objects using the map when referring to them.
- Lazy Load
    - An object that doesn't contain all of the data you need but knows how to get it.

<!-- - DAO 
    - uses EntityRow, provides CRUD upon DB, on single tables
    - GetById(userId)
- Mapper 
    - converts EntityRow to Entity
    - Map(UserRow) -> User
- Repository 
    - uses EntityRows, uses Entities, converts row to entities, gets rows by using DAO, works on more tables, orchestrates working with multiple DAOs and Mappers
    - CommentRepo.GetByUser(userId)
    - CommentRepo.GetByIssue(issueId)
- Service 
    - uses Repositories, complex business logic
    - CommectService.Remove(commentId)
    - CommectService.GetByIssueAndUser(issueId, userId)


- Presentation
    - WebUI - MVC
    - Desktop - Avalonia
- Domain 
    - patterns: 
        - Transaction Script 
        - Domain Model 
            - Objects have business logic in them, 
            - DOESN'T have data logic inside it -> that would be an active-record/row-data-gateway
        - Table Module (DONT USE, too much smell) 
- DataAccess - Microsoft.Data.SqlClient
    - Data Mapper 
        - pass the whole domain model - let the mapper do the work
    - Table Data Gateway 
        - pass parameters one by one <- coupled to the database schema
    (these are very similar, the difference is in where the delegation of parameter passing is done)
Database - local sqlite3 database
    - no need to complicate things -->

# Tables

[https://dbdiagram.io/d/github-67dfeaaf75d75cc8441f8ce5]

# CVIKO 3
- Data layer implmenetation

```
GlobalConfig --> IDataConnector

IDataConnector  <|-- SQLConnector
                <|-- TextConnector

IItemDao <|-- ItemSqlDao
         <|-- ItemTextDao
```

IDataConnector uses IItemDAO and ICustomerDAO

Implemented Connector creates DAOs
    - TextConnector creates ItemTextDAO
    - SqlConnector creates ItemSqlDAO

- global config can switch databases by changing one line

Factory - Create<IDataConnector, SqlDataConnector>(), Create<IDataConnector, TextDataConnector>()
Facade - hide under a common interface and working only with that interface, not the concrete implementation
Strategy - depending on the IDataConnector implementation, it instantiates different IItemDao and ICustomerDao
Table Data Gateway - DAOs are Table Data Gateways 
