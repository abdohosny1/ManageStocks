
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
builder.Services.AddSignalR();
builder.Services.AddScoped<SocketHub>();


//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(text,
    builder =>
    {
        builder.AllowAnyOrigin();
        //builder.WithOrigins("url");
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
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

app.UseAuthorization();

app.UseCors(text);
app.UseRouting(); // Add this line to enable routing

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SocketHub>("/sockethub");
    endpoints.MapControllers();
});
ApplicatioContextSeeding.Seed(app);


app.Run();
