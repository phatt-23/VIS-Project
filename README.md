# GitHub like platform

DAO - uses EntityRow, provides CRUD upon DB, on single tables
    - GetById(userId)
Mapper - converts EntityRow to Entity
    - Map(UserRow) -> User
Repository 
    - uses EntityRows, uses Entities, converts row to entities, gets rows by using DAO, works on more tables, orchestrates working with multiple DAOs and Mappers
    - CommentRepo.GetByUser(userId)
    - CommentRepo.GetByIssue(issueId)
Service - uses Repositories, complex business logic
    - CommectService.Remove(commentId)
    - CommectService.GetByIssueAndUser(issueId, userId)


Presentation
    - WebUI - MVC
    - Desktop - Avalonia
Domain 
    - patterns: 
        - Transaction Script (DONT USE, leads to complex long functions) 
        - Domain Model (OK) 
            - Objects have all business logic in them, 
            - DOESN'T have data logic inside it -> that would be an active-record/row-data-gateway
        - Table Module (DONT USE, too much smell) 
DataAccess - Microsoft.Data.SqlClient
    - Data Mapper 
        - pass the whole domain model - let the mapper do the work
    - Table Data Gateway 
        - pass parameters one by one <- coupled to the database schema
    (these are very similar, the difference is in where the delegation of parameter passing is done)
Database - local sqlite3 database
    - no need to complicate things

# Tables

[https://dbdiagram.io/d/github-67dfeaaf75d75cc8441f8ce5]

