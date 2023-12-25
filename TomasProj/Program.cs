using TomasProj.Interfaces;
using TomasProj.Services;

namespace TomasProj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IDocumentStorageInit, DocumetStorageInit>();
            builder.Services.AddSingleton<IDocumentStorage, DocumentStorage>();
            builder.Services.AddSingleton<IFormatResolver, FormatResolver>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Documents",
                pattern: "{controller=Documents}/{action=Index}/{id?}");

            app.Run();
        }
    }
}