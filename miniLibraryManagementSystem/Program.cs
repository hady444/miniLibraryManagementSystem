using Library_Management_System.Data.LibraryContext;
using Microsoft.EntityFrameworkCore;

var options = new WebApplicationOptions
{
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Web", "wwwroot") // absolute path
};

var builder = WebApplication.CreateBuilder(options);


builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
    });

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("mem"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
