using ContosoPizza.Services;
using ContosoPizza.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWelcomeService, WelcomeService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Middlewares
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
#endregion

app.MapGet("/", async (IWelcomeService welcomeService1, IWelcomeService welcomeService2) => 
    {
        string message1 = $"Message1: {welcomeService1.GetWelcomeMessage()}";
        string message2 = $"Message2: {welcomeService2.GetWelcomeMessage()}";
        return $"{message1}\n{message2}";
    });

app.Run();
