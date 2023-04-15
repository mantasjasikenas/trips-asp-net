using TripsAgency.Database;
using TripsAgency.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services
       .AddMvc(options =>
       {
           options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
               _ => "This field is required.");
           
           options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
               _ => "The value is invalid.");
       })
       .WithRazorPagesRoot("/Views");


builder.Logging
       .ClearProviders()
       .AddConfiguration(builder.Configuration.GetSection("Logging"))
       .AddConsole()
       .AddDebug()
       .AddEventSourceLogger();


builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<AgentsRepository>();
builder.Services.AddScoped<OrdersRepository>();
builder.Services.AddScoped<AgentOrdersRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
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