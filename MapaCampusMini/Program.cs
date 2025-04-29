using MapaCampusMini.Services;
using MapaCampusMini.Models;
using MapaCampusMini.Services.Interfaces;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMapaService, MapaService>();
builder.Services.AddScoped<ILecturaJsonService, LecturaJsonService>();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();    
app.UseCors();       
app.UseAuthorization();
app.MapControllers();

app.Run();
