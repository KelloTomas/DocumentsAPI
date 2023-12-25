using Ark.Tools.AspNetCore.MessagePackFormatter;

using Microsoft.AspNetCore.Mvc.Formatters;

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
            builder.Services.AddControllers(options =>
            {
                // Add support for XML
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(options));

                // Add support for MessagePack
                options.OutputFormatters.Add(new MessagePackOutputFormatter());
                options.InputFormatters.Add(new MessagePackInputFormatter());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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