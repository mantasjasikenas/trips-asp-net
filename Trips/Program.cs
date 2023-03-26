using MySql.Data.MySqlClient;
using TripsAgency.Database;
using TripsAgency.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc().WithRazorPagesRoot("/Views");
builder.Logging
    .ClearProviders()
    .AddConfiguration(builder.Configuration.GetSection("Logging"))
    .AddConsole()
    .AddDebug()
    .AddEventSourceLogger();





builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<AgentsRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
