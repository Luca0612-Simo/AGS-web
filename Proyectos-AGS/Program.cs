using AGS_services.Repositories;
using AGS_services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CarouselController).Assembly);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<iCarouselRepository, CarouselImageRepository>();
builder.Services.AddScoped<IFileStorageService, AwsS3Service>();
builder.Services.AddScoped<ICarouselService, CarouselService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
=======
using AGS_services;
using AGS_services.Handler;
using AGS_services.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));


MySqlHandler.ConnectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddSingleton<IUserRepository, UserService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock.Backend");
    c.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();

app.Run();