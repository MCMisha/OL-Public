# Opera Lubelska Website Prototype – Backend

A modern ASP.NET Core Web API developed as part of a master's thesis. This project is a prototype of a redesigned Opera Lubelska website, providing RESTful APIs for both public users and administrators.

## Features

- 🔐 JWT Authentication
- 🎭 Performance management
- 📅 Event scheduling
- 👥 Artist and implementer management
- 🎟 Ticket price management
- 📰 News management
- 🏛 About & Contact section management
- 🖼 Image upload support
- 📄 Rich text content management
- ⚠ Global exception handling
- 📚 Swagger / Scalar API documentation

## Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- JWT Bearer Authentication
- Swagger / Scalar
- Dependency Injection

## Architecture

```
Controllers
    │
    ▼
 Services
    │
    ▼
Repositories
    │
    ▼
Entity Framework Core
    │
    ▼
 PostgreSQL
```

## Project Structure

```
Controllers/
DTOs/
Entities/
Exceptions/
Middleware/
Repositories/
Services/
Migrations/
```

## Getting Started

### Clone the repository

```bash
git clone https://github.com/your-username/OL-Back.git
```

### Configure the application

Update the connection string and JWT settings in:

```
appsettings.json
```

### Apply migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

API documentation will be available at:

```
https://localhost:xxxx/swagger
```

or

```
https://localhost:xxxx/scalar
```

## Main Functionalities

- Public API for performances, events, artists, news and contact information
- Administration API secured with JWT authentication
- CRUD operations for all managed entities
- Dynamic homepage content management
- Image storage and retrieval
- Database migrations with Entity Framework Core

## Related Project

Frontend repository:
> https://github.com/MCMisha/OL-Front

## Future Improvements

- Refresh Token authentication
- Role-based authorization
- Unit and integration tests
- Docker support
- Cloud deployment
- Caching

## Author

**Mikhail Yakushevich**

Master's thesis project  
Maria Curie-Skłodowska University (UMCS)
