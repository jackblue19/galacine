# Galacine
Cinema management

# Solution Project Structure *(Rcm by GPT)*

    CinemaSystem.sln
    ├── CinemaSystem.Web/                    # Razor Pages UI (tầng giao diện)
    │   ├── Pages/
    │   │   ├── Movies/
    │   │   │   ├── Index.cshtml
    │   │   │   ├── Create.cshtml
    │   │   │   └── Edit.cshtml
    │   │   ├── Schedules/
    │   │   ├── Bills/
    │   │   ├── Tickets/
    │   │   ├── Shared/
    │   │   │   └── _Layout.cshtml
    │   │   └── Index.cshtml
    │   ├── wwwroot/
    │   ├── Program.cs
    │   ├── Startup.cs
    │   └── appsettings.json
    │
    ├── CinemaSystem.Application/           # Business logic layer (service, DTO, interface)
    │   ├── Interfaces/
    │   │   ├── Services/
    │   │   │   ├── IMovieService.cs
    │   │   │   └── IScheduleService.cs
    │   │   └── Repositories/
    │   │       ├── IMovieRepository.cs
    │   │       ├── IGenericRepository.cs
    │   │       └── ...
    │   ├── Services/
    │   │   ├── MovieService.cs
    │   │   └── ...
    │   ├── DTOs/
    │   │   ├── MovieDto.cs
    │   │   └── ScheduleDto.cs
    │   └── Validators/
    │       └── MovieValidator.cs
    │
    ├── CinemaSystem.Infrastructure/        # Data access layer (EF Core + Repositories)
    │   ├── Data/
    │   │   ├── CinemaDbContext.cs
    │   │   └── DbInitializer.cs
    │   ├── Models/                         # Entities từ Scaffold-DbContext
    │   │   ├── Movie.cs
    │   │   ├── User.cs
    │   │   ├── Category.cs
    │   │   └── ...
    │   ├── Repositories/                  # Repository implementations
    │   │   ├── MovieRepository.cs
    │   │   ├── GenericRepository.cs
    │   │   └── ...
    │   └── Migrations/                    # Nếu dùng Code First sau này
    │
    ├── CinemaSystem.Tests/                 # Unit Test với xUnit
    │   ├── ApplicationTests/
    │   │   ├── MovieServiceTests.cs
    │   │   └── ...
    │   ├── InfrastructureTests/
    │   │   └── MovieRepositoryTests.cs
    │   └── TestHelpers/
    │       └── DbContextMocker.cs
    │
    └── CinemaSystem.Build/                 # CI/CD, DevOps config
        └── github-actions.yml / azure-pipelines.yml

