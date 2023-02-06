using System.Text.Json.Serialization;
using Backend.Configuration;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
.AddLogging()
.AddHttpLogging(logging => {
  logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
})
.AddHttpContextAccessor()
.AddMvcCore()
.AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
.AddCors(options =>
{
  options.AddPolicy("api", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


builder.Services
.AddDbContext<TTSContext>(options => options.UseInMemoryDatabase("ttscontext"));

builder.Services
.AddJWT(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TTSContext>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher>();
    context.Database.EnsureCreated();
    await DbSetup.DbSetup.SetupAsync(context, passwordHasher);
}


app.UseHttpLogging();
app.UseCors("api");
app.UseAuthorization();

app.MapControllers();

app.Run();
