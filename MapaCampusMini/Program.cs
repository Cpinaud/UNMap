using MapaCampusMini.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<MapaService>();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors();
    app.UseRouting();
    app.MapControllers();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html"); // Esto se asegura de que React maneje las rutas
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
