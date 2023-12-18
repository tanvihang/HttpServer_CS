namespace HttpServerBasic;
using Microsoft.Extensions.Configuration;
using System;


public class HttpServerBasicApplication
{
    public static void Main(string[] args)
    {

        string baseDirectory = AppContext.BaseDirectory;
        string rootDirectory = Path.Combine(baseDirectory, "..", "..","..");
        rootDirectory = Path.GetFullPath(rootDirectory);
        rootDirectory += "/appsettings.json";
        
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(rootDirectory)
            .AddEnvironmentVariables()
            .Build();
        
        Server server = new Server(config);
        server.Run();
    }

}