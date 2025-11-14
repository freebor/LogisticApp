# LogisticApp

# 🚚 Logistics Management System

A comprehensive RESTful API for managing logistics operations including driver management, delivery tracking, and real-time notifications. Built with ASP.NET Core 8, Entity Framework Core, and JWT authentication.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication with refresh tokens
- Role-based access control (Admin, Driver, Client)
- Secure password hashing with BCrypt
- Token expiry and refresh mechanism

### 📦 Delivery Management
- Create and track deliveries
- Automatic driver assignment based on proximity
- Real-time delivery status updates
- Complete delivery lifecycle management

### 🚗 Driver Management
- Driver registration and profile management
- Real-time GPS location tracking
- Availability status management
- Automatic driver assignment algorithm

### 🔔 Real-Time Notifications
- SignalR integration for live updates
- Instant delivery status notifications
- Driver assignment alerts
- Real-time dashboard updates

### 📊 Analytics & Reporting
- Delivery statistics and metrics
- Driver performance tracking
- Business intelligence dashboard
- Comprehensive audit trail

### 🛡️ Advanced Features
- Soft delete with audit trail (track who created/modified/deleted records)
- Database migrations for version control
- Optimized database indexes for performance
- Connection resilience with automatic retry
- Comprehensive data validation
- Global exception handling

## 🏗️ Architecture

```
LogisticAppManagement/
├── Controllers/          # API endpoints
├── Services/            # Business logic layer
├── Repository/          # Data access layer
├── Models/              # Entities and DTOs
├── Data/                # Database context
├── Middleware/          # Custom middleware
├── Common/              # Shared utilities
└── Hubs/                # SignalR hubs
```

**Design Pattern:** Repository Pattern with Unit of Work

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/logistics-management-system.git
   cd logistics-management-system
   ```

2. **Update connection string**
   
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=LogisticsDB;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Install EF Core tools**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   ```
   https://localhost:5001/swagger
   ```

## 📖 API Documentation

### Authentication Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/auth/register` | Register new user | Public |
| POST | `/api/auth/login` | User login | Public |
| POST | `/api/auth/refresh-token` | Refresh access token | Public |
| POST | `/api/auth/logout` | User logout | Authenticated |
| GET | `/api/auth/me` | Get current user | Authenticated |

### Driver Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/drivers` | Create driver | Admin |
| GET | `/api/drivers/available` | Get available drivers | Authenticated |
| PUT | `/api/drivers/{id}/location` | Update driver location | Driver, Admin |

### Delivery Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/deliveries` | Create delivery | Client, Admin |
| POST | `/api/deliveries/{id}/assign` | Assign driver | Admin |
| PUT | `/api/deliveries/{id}/status` | Update status | Driver, Admin |
| GET | `/api/deliveries` | Get all deliveries | Authenticated |

### Client Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/clients/register` | Register client | Admin |
| GET | `/api/clients` | Get all clients | Admin |
| GET | `/api/clients/{id}` | Get client by ID | Admin, Client |

### Analytics Endpoints

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| GET | `/api/analytics/dashboard` | Get dashboard stats | Authenticated |

## 🔑 Authentication

All protected endpoints require a JWT token in the Authorization header:

```bash
Authorization: Bearer {your-access-token}
```

### Example Usage

```bash
# 1. Register
curl -X POST http://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123",
    "confirmPassword": "Password123",
    "fullName": "John Doe",
    "role": 3
  }'

# 2. Login
curl -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123"
  }'

# 3. Use token
curl -X GET http://localhost:5001/api/deliveries \
  -H "Authorization: Bearer {token-from-login}"
```

## 🎯 User Roles

| Role | Permissions |
|------|-------------|
| **Admin** | Full system access - manage all resources |
| **Driver** | Update location, manage assigned deliveries |
| **Client** | Create deliveries, view delivery status |

## 🛠️ Technologies

- **Framework:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core 8.0
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **Password Hashing:** BCrypt.NET
- **Real-Time:** SignalR
- **API Documentation:** Swagger/OpenAPI
- **Validation:** Data Annotations

## 📊 Database Schema

### Core Entities

- **Users** - System users with role-based access
- **Drivers** - Driver profiles with location tracking
- **Deliveries** - Delivery orders with status tracking
- **Clients** - Client/customer information

### Audit Features

All entities include:
- `CreatedAt` / `CreatedBy` - Creation tracking
- `UpdatedAt` / `UpdatedBy` - Modification tracking
- `IsDeleted` / `DeletedAt` / `DeletedBy` - Soft delete support

## 🧪 Testing

```bash
# Run unit tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

## 📦 Deployment

### Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release
dotnet run --environment Production
```

### Environment Variables

```bash
# Required
ASPNETCORE_ENVIRONMENT=Production
JWT_SECRET_KEY=your-secret-key-here
CONNECTION_STRING=your-production-connection-string

# Optional
JWT_EXPIRY_MINUTES=60
REFRESH_TOKEN_EXPIRY_DAYS=7
```

## 🔒 Security Features

- ✅ JWT authentication with refresh tokens
- ✅ Role-based authorization
- ✅ Password hashing with BCrypt
- ✅ Input validation and sanitization
- ✅ SQL injection prevention (EF Core parameterization)
- ✅ CORS configuration
- ✅ HTTPS enforcement
- ✅ Audit trail for all operations

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- LinkedIn: [Your Name](https://linkedin.com/in/yourprofile)
- Email: your.email@example.com

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- Authentication using [JWT](https://jwt.io/)
- Real-time updates with [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)

---

<div align="center">
  Made .Net ❤️ by Freedom Chisom
</div>
   ```bash
   git clone https://github.com/your-username/your-repo.git
