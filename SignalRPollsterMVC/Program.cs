using SignalRPollsterSiteMVC.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();  //enable SignalR
builder.Services.AddSingleton<IPollProvider, InMemoryPollProvider>();   //register the InMemoryPollProvider for use in Dependency Injection in the PollHub
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapHub<PollHub>("/pollster");   //maps the PollHub class to the route "/pollster"
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "poll",
    pattern: "poll/{pollId}",
    defaults: new { controller = "Home", action = "Index" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{pollId?}");

app.Run();
