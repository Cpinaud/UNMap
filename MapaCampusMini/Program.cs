using MapaCampusMini.Services;
using MapaCampusMini.Models;
using MapaCampusMini.Services.Interfaces;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ILecturaJsonService, LecturaJsonService>();
builder.Services.AddScoped<MapaService>();
builder.Services.AddOpenApi();
builder.Services.AddScoped<MapaService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MapaNew}/{action=Index}/{id?}");

app.Run(); 

