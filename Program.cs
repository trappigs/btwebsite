using btlast.Controller;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// YEN� EKLENEN SATIR: SmtpSettings'i appsettings'deki "Smtp" b�l�m�ne ba�la
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
