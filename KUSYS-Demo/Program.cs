using KUSYS_Demo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder build olmadan önce eklememiz gerekiyor.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        //appsettings içerisinde tanýmladýðýmýz connectionlara bu þekilde ulaþabiliyoruz. builer a Keylerini vermemiz yeterli.
        //farklý ortamlar için farklý connection stringler tanýmlanabilir.
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//hot reload yapmak için güzel bir paket. Ama sorunlu...
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
/*
 * migration yapmak için package manager console u açýp 
 * add-migration aciklama yazarak db migration yapýlýyor. 
 * Eðer not recognized hatasý var ise muhtemelen gerekli paket yüklü deðildir.
 * Yüklenmesi gereken paket Microsoft.EntityFrameworkCore.Tools tur.
 * migration hazýr olduktan sonra update-database komutuyla deðiþikliklerimizi dbye gönderiyoruz.
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
