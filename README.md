# ModularPlatform

A clean, modular .NET 8 platform designed for scalability and flexibility.  
This solution demonstrates a feature-based architecture with independently deployable modules.

## 🚀 Features

- **Modular architecture** with Auth and Logging modules  
- **Dependency Injection** via interfaces and extension methods  
- **Authentication** using JWT with ASP.NET Identity  
- **Logging abstraction** compatible with Serilog, built on `ILogger<T>`  
- **Testable design** with xUnit and Moq  
- **Framework-agnostic** services (e.g., database and logging)  

## 🧱 Module Overview

| Module    | Description                          |
|-----------|--------------------------------------|
| `Auth`    | Handles user registration, login, and password operations |
| `Logging` | Centralized logging abstraction      |

## 🧪 Tests

Unit tests live under the `tests/` directory, following the same module structure.

```bash
dotnet test
```

## 🛠️ Getting Started

1. Clone the repo  
2. Setup your `appsettings.json` with `Jwt` config  
3. Add module services via DI:

```csharp
services.AddAuthModule(Configuration);
services.AddLoggingModule();
```

4. Run the API!

## 📁 Structure

```text
src/
  Modules/
    Auth/
      Application/
      Infrastructure/
    Logging/
      Application/
      Infrastructure/
tests/
  ModularPlatform.Tests/
```

### 📜 License

MIT License.
