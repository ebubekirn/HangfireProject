using FireApp.Services;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using System.Runtime.ConstrainedExecution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- Hangfire Managements --------------------
builder.Services.AddHangfire(config => config
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("baglanti")));
builder.Services.AddHangfireServer();
// -------------------- *** --------------------

builder.Services.AddTransient<IServiceManagement, ServiceManagement>();

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

// -------------------- Hangfire Managements --------------------
app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "Drivers Dashboard",
    Authorization = new []
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass = "Passw0rd",
            User = "Ebubekir"
        }
    }
});
app.MapHangfireDashboard();
RecurringJob.AddOrUpdate<IServiceManagement>(x => x.SyncData(), "0 * * ? * *");
// -------------------- ** --------------------
app.Run();


