using KUSYS_Demo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder build olmadan �nce eklememiz gerekiyor.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        //appsettings i�erisinde tan�mlad���m�z connectionlara bu �ekilde ula�abiliyoruz. builer a Keylerini vermemiz yeterli.
        //farkl� ortamlar i�in farkl� connection stringler tan�mlanabilir.
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//hot reload yapmak i�in g�zel bir paket. Ama sorunlu...
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
/*
 * migration yapmak i�in package manager console u a��p 
 * add-migration aciklama yazarak db migration yap�l�yor. 
 * E�er not recognized hatas� var ise muhtemelen gerekli paket y�kl� de�ildir.
 * Y�klenmesi gereken paket Microsoft.EntityFrameworkCore.Tools tur.
 * migration haz�r olduktan sonra update-database komutuyla de�i�ikliklerimizi dbye g�nderiyoruz.
 */

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
