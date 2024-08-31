using BackEnd.Configs.Enviroments;

var builder = WebApplication.CreateBuilder(args);
var env = new EnviromentVariables();

//bonus config
//Addconsole logging
builder.Logging.AddConsole();

//Configures
//PostgreSQL
builder.Services.ConfigureDatabase(env.GetConnectionString());
//JWT
builder.Services.ConfigureJwt(env.GetJwtKey());

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:8080")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.UseCors("AllowLocalhost");

app.Run();
