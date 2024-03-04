//using Serilog;
//using Serilog.Events;

//public class Program
//{
//    #region Variable 

//    public static string? countryId;
//    public static string? baseURL;
//    public static string? logFilePath;

//    #endregion

//    #region Constants

//    public const string APPSETTINGFILENAME = "appsettings.json";
//    public const string MICROSOFT = "Microsoft";
//    public const string ERROR = "/Error";
//    public const string HOST = "/_Host";

//    #endregion

//    #region Methods 
//    /// <summary>
//    /// Main() of Program class
//    /// </summary>
//    /// <param name="args"></param>
//    public static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);
//        ReadConfigurations();
//        if (baseURL != null && logFilePath is not null)
//        {
//            HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(baseURL) };
//            AddServicesToTheContainer(builder, httpClient);
//            Log.Logger = new LoggerConfiguration()
//                       .MinimumLevel.Information()
//                       .MinimumLevel.Override(MICROSOFT, LogEventLevel.Error)
//                       .Enrich.FromLogContext()
//                       .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
//                       .CreateLogger();
//        }
//        var app = builder.Build();
//        if (!app.Environment.IsDevelopment())
//        {
//            app.UseExceptionHandler(ERROR);
//        }
//        app.UseStaticFiles();
//        app.UseRouting();
//        app.MapBlazorHub();
//        app.MapFallbackToPage(HOST);
//        app.Run();
//    }

//    /// <summary>
//    /// Read Configuration from appSettings file
//    /// </summary>
//    private static void ReadConfigurations()
//    {
//        IConfigurationRoot config = new ConfigurationBuilder()
//            .AddJsonFile(APPSETTINGFILENAME)
//            .Build();
//        countryId = (string)config.GetValue(typeof(string), "CountryId");
//        baseURL = (string)config.GetValue(typeof(string), "WebApiBaseUrl");
//        logFilePath = (string)config.GetValue(typeof(string), "LogFilePath");

//    }

//    /// <summary>
//    /// Adding services to the container.
//    /// </summary>
//    /// <param name="builder"></param>
//    /// <param name="httpClient"></param>
//    private static void AddServicesToTheContainer(WebApplicationBuilder builder, HttpClient httpClient)
//    {
//        httpClient.DefaultRequestHeaders.Add("CountryId", countryId);
//        builder.Services.AddSingleton<HttpClient>(x => httpClient);
//        builder.Services.AddRazorPages();
//        builder.Services.AddServerSideBlazor();
//        if (baseURL != null)
//        {
//            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseURL) });
//        }

//        #endregion
//    }
//}
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
