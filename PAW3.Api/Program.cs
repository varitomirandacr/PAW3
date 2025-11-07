using Microsoft.EntityFrameworkCore;
using PAW3.Core.BusinessLogic;
using PAW3.Data.MSSQL;
using PAW3.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with connection string from appsettings
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB")));

// Register Business Logic Services
builder.Services.AddScoped<ICategoryBusiness, CategoryBusiness>();
builder.Services.AddScoped<IComponentBusiness, ComponentBusiness>();
builder.Services.AddScoped<IInventoryBusiness, InventoryBusiness>();
builder.Services.AddScoped<INotificationBusiness, NotificationBusiness>();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
builder.Services.AddScoped<IRoleBusiness, RoleBusiness>();
builder.Services.AddScoped<ISupplierBusiness, SupplierBusiness>();
builder.Services.AddScoped<ITaskBusiness, TaskBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IUserActionBusiness, UserActionBusiness>();
builder.Services.AddScoped<IUserRoleBusiness, UserRoleBusiness>();

// Register Repositories
builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryComponent, RepositoryComponent>();
builder.Services.AddScoped<IRepositoryInventory, ReposityryInventory>();
builder.Services.AddScoped<IRepositoryNotification, RepositoryNotification>();
builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddScoped<IRepositoryRole, RepositoryRole>();
builder.Services.AddScoped<IRepositorySupplier, RepositorySupplier>();
builder.Services.AddScoped<IRepositoryTask, RepositoryTask>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryUserAction, RepositoryUserAction>();
builder.Services.AddScoped<IRepositoryUserRole, RepositoryUserRole>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
