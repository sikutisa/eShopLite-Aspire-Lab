using Store.Components;
using Store.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddRedisOutputCache("redis");

builder.AddServiceDefaults();

builder.Services.AddHttpClient<ProductService>(c =>
{
    c.BaseAddress = new("https+http://products");
});

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var app = builder.Build();

app.UseOutputCache();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
