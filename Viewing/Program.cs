var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Add all services BEFORE builder.Build()
builder.Services.AddControllersWithViews();  // MVC
builder.Services.AddHttpClient();             // HttpClientFactory
builder.Services.AddSession();                // optional, for storing token later

var app = builder.Build();
app.UseStaticFiles(); // <<< THIS ENABLES ~/path resolution

// 2️⃣ Configure middleware AFTER build
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession(); // optional

// 3️⃣ Configure routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");  // start at Login
// app.MapControllerRoute(
//     name: "default2",
//     pattern: "{controller=Home}/{action=Index}/{id?}"); // remove duplicate

app.Run();
