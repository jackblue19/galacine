# Galacine
Cinema management

# Solution Project Structure *(Ref: GPT)*

    CinemaSystem.sln
    ├── CinemaSystem.Web/                # UI Layer - Razor Pages
    │   ├── Pages/
    │   │   ├── MoviePages/
    │   │   │   ├── Index.cshtml
    │   │   │   ├── Index.cshtml.cs
    │   │   │   ├── Create.cshtml
    │   │   │   ├── Create.cshtml.cs
    │   │   │   ├── Edit.cshtml
    │   │   │   ├── Edit.cshtml.cs
    │   ├── Shared/                      # Partial views, layout, error pages
    │   │   ├── _Layout.cshtml
    │   │   └── _ValidationScriptsPartial.cshtml
    │   ├── wwwroot/                     # Static files (css/js/img)
    │   │   ├── css/
    │   │   ├── js/
    │   ├── Program.cs                  # DI, Middleware config
    │   └── appsettings.json

    ├── CinemaSystem.Data/              # DB Layer - scaffolded entities + DbContext
    │   ├── Entities/
    │   │   ├── Movie.cs
    │   │   ├── User.cs
    │   │   ├── Schedule.cs
    │   │   └── ...                    # Các bảng scaffold khác
    │   ├── CinemaDbContext.cs
    │   ├── DbInitializer.cs           # Tùy chọn: seed data mẫu
    │   ├── Mappings/                  # Tùy chọn: Fluent API override cho table khó
    │   └── Migrations/                # EF Core migrations

    ├── CinemaSystem.Application/       # Business Logic Layer
    │   ├── DTOs/
    │   │   ├── MovieDto.cs
    │   │   └── CreateMovieRequest.cs
    │   ├── Interfaces/
    │   │   ├── Services/
    │   │   │   └── IMovieService.cs
    │   │   └── Repositories/
    │   │       └── IMovieRepository.cs
    │   ├── Services/
    │   │   └── MovieService.cs        # Dùng DI inject repository, xử lý nghiệp vụ
    │   ├── Validators/
    │   │   └── MovieValidator.cs      # FluentValidation cho MovieDto
    │   └── Mapping/                   # AutoMapper profiles
    │       └── AutoMapperProfile.cs

    ├── CinemaSystem.Infrastructure/    # DB Repository Implementations
    │   ├── Repositories/
    │   │   ├── MovieRepository.cs     # Implements IMovieRepository
    │   │   └── GenericRepository.cs
    │   └── Helpers/
    │       └── PagingHelper.cs        # Tùy chọn: hỗ trợ phân trang, filter

    ├── CinemaSystem.Tests/             # Unit & Integration Tests
    │   ├── ApplicationTests/
    │   │   └── MovieServiceTests.cs
    │   ├── InfrastructureTests/
    │   │   └── MovieRepositoryTests.cs
    │   ├── WebTests/
    │   │   └── PageModelTests.cs
    │   └── TestHelpers/
    │       └── DbContextMocker.cs

    ├── CinemaSystem.Build/             # CI/CD, Docker, Pipeline
    │   ├── docker-compose.yml
    │   ├── github-actions.yml
    │   └── launchSettings.json

### Web cần dùng Application và Infrastructure

* dotnet add CinemaSystem.Web/CinemaSystem.Web.csproj reference CinemaSystem.Application/CinemaSystem.Application.csproj
* dotnet add CinemaSystem.Web/CinemaSystem.Web.csproj reference CinemaSystem.Infrastructure/CinemaSystem.Infrastructure.csproj

### Infrastructure triển khai interface từ Application
* dotnet add CinemaSystem.Infrastructure/CinemaSystem.Infrastructure.csproj reference CinemaSystem.Application/CinemaSystem.Application.csproj

### Test project cần dùng Application + Infrastructure
* dotnet add CinemaSystem.Tests/CinemaSystem.Tests.csproj reference CinemaSystem.Application/CinemaSystem.Application.csproj
* dotnet add CinemaSystem.Tests/CinemaSystem.Tests.csproj reference CinemaSystem.Infrastructure/CinemaSystem.Infrastructure.csproj

