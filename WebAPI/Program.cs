using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.Configs;
using Core.Utils.Security.JWT;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using DataAccess.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

ConnectionConfig.ConnectionString = builder.Configuration.GetConnectionString("ECommerceContext");
builder.Services.AddScoped<DbContext, ECommerceContext>();

builder.Services.AddScoped<CategoryRepositoryBase, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<CampaignRepositoryBase, CampaignRepository>();
builder.Services.AddScoped<ICampaignService, CampaignService>();

builder.Services.AddScoped<ProductRepositoryBase, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<CartRepositoryBase, CartRepository>();
builder.Services.AddScoped<CartItemRepositoryBase, CartItemRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<OrderRepositoryBase, OrderRepository>();
builder.Services.AddScoped<OrderItemRepositoryBase, OrderItemRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IShoppingService, ShoppingService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserRepositoryBase, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenHelper,JWTHelper>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation V1");
    });
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
