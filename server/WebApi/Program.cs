using Constracts.Interfaces;
using DAL;
using DAL.Services;
using Services.Services;

var allowAll = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll,

                      x =>
                      {
                          x
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true) // allow any origin
                           .AllowCredentials();
                      }); // allow credentials
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddScoped<IStudentService, StudentsService>();
builder.Services.AddScoped<IDalService, Dal>();
builder.Services.AddScoped<IConvertorService, ConvertorService>();
var app = builder.Build();
app.UseCors(allowAll);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
