# .NET 5 basic CRUD API template 
Basic .NET core API template using common design patterns and best practices 
This project is using PostgreSQL database with entity framework.

Design patterns and best practices present here:
- Command and Query Responsibility Segregation (CQRS)
- Validators
- Repositories
- Handlers
- Abstractions
- Mediator
- Mapper
- Domain Driven Design (DDD)
- Domain Transfer Objects (DTO)
- Entity Framework (EF)
- Unit Testing

This project supports Docker containers
    docker-compose up --build -d

Endpoints exposed on http://localhost:5000/

available endpoints : 
- GET       http://localhost:5000/api/v1/users
- POST      http://localhost:5000/api/v1/users
- GET       http://localhost:5000/api/v1/users/{userId}
- PUT       http://localhost:5000/api/v1/users/{userId}
- DELETE    http://localhost:5000/api/v1/users/{userId}

TODO: 
- Add JWT authentication 
- Add caching support
- TBD