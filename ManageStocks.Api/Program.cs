
using ManageStocks.Api.Helper;
using ManageStocks.ApplicationCore.unitOfWork;
using ManageStocks.Infrastructure.unitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string text = "";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add service AddDbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"),
    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

//add service IUnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//add service  AddSignalR
builder.Services.AddSignalR()
                .AddHubOptions<SocketHub>(options =>
                {
                    options.EnableDetailedErrors = true; // Enable detailed error messages
                });
//builder.Services.AddScoped<SocketHub>();


//add cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(text,
//    builder =>
//    {
//       // builder.AllowAnyOrigin();
//        builder.WithOrigins("https://localhost:4200");// the Angular app url

//        //builder.WithOrigins("url");
//        builder.AllowAnyMethod();
//        builder.AllowAnyHeader();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Specify the allowed origin(s)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Allow credentials (e.g., cookies, authorization headers)
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors();
app.UseRouting(); // Add this line to enable routing
app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SocketHub>("/socketHub");
});
ApplicatioContextSeeding.Seed(app);


app.Run();
