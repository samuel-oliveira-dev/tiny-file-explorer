using TinyFileExplorer;
using TinyFileExplorer.Configurations;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTinyFileExplorer(options => options.RootDirectories = new List<RootDirectory>() 
{
    new RootDirectory(){Name = "Default", Path =  "C:\\Users\\samuk\\Documents\\FileHub\\RootFolder"},
    new RootDirectory(){Name = "Alternative", Path = "C:\\Users\\samuel.oliveira\\Documents\\RootFolder"}
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseEmbeddedStaticFiles("TinyFileExplorer", typeof(Program).Assembly);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
