using System.Net;
using System.Reflection;
using HttpServerBasic.Model;
using HttpServerBasic.Sys.Service;

namespace HttpServerBasic.Controller;

public class WebIndex:IController
{
    private IUserService userService;
    private static WebIndex instance;
    

    private WebIndex()
    {
        
    }

    public void SetServices(IUserService userService)
    {
        this.userService = userService;
    }

    public static WebIndex GetInstance()
    {
        if (instance == null)
        {
            instance = new WebIndex();
        }

        return instance;
    }
    
    [Route("/index")]
    public Result<long> Index(HttpListenerContext context)
    {
        //read index file
        string filePath = FileUtils.GetFilePath("/Resources/Static/index.html");

        Result<long> result = base.GetFile(context, filePath, "text/html");
        
        return result;
    }

    [Route("/about")]
    public Result<long> About(HttpListenerContext context)
    {
        var response = context.Response;
        //Construct a response according to request
        string responseString = "<HTML><BODY> About Page!</BODY></HTML>";
        //conver to byte so it can be sent through the network
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;

        output.Write(buffer, 0, buffer.Length);

        output.Close();
        
        return new Result<long>(400,"success");
    }
}