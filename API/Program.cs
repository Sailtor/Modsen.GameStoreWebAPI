using API.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();
builder.Services.ConfigureMsSqlServerContext(builder.Configuration);
builder.Services.ConfigureEntityServices();
builder.Services.ConfigureUnitOfWork();
builder.Services.ConfigureTokenService();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureAutomapper();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureFluentValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Configuration.AddUserSecrets<Program>();
}

app.UseLoggerMiddleware();

app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();