using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PrzychodniaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrzychodniaContext") ?? throw new InvalidOperationException("Connection string 'PrzychodniaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
	// Set a short timeout for easy testing.
	options.IdleTimeout = TimeSpan.FromSeconds(180);
	options.Cookie.HttpOnly = true;
	// Make the session cookie essential
	options.Cookie.IsEssential = true;
});

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

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
