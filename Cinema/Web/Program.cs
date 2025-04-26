using Application;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapping;
using Application.Services;
using Data;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSession();

builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Register generic repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register generic services
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

// Register auto-mapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add Scoped - repo
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieDetailRepository, MovieDetailRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IScheduleRepository, SchudelesRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();;
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


// Add Scoped - service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISchedulesService, SchedulesService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMovieGenreService, MovieGenreService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IBookingService, BookingService>();


// ========== Authentication & Google & Cookies & Session ==========
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    })
    .AddGoogle(options =>
    {
        var googleSection = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = googleSection["ClientId"];
        options.ClientSecret = googleSection["ClientSecret"];
        options.CallbackPath = "/signin-google"; 
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthorization();

// builder.Services.AddRazorPages()
//     .AddNewtonsoftJson(options =>
//     {
//         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//     });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseSession();

app.UseCookiePolicy();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", async context =>
{
    context.Response.Redirect("/client/index");
    await Task.CompletedTask;
});
app.UseSession();

app.MapRazorPages();
app.MapControllers();
app.MapControllers();

app.Run();
