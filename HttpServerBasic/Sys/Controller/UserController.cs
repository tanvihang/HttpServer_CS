using System.Net;
using System.Reflection;
using HttpServerBasic.Model;
using HttpServerBasic.Sys.Service;

namespace HttpServerBasic.Controller;

public class UserController:IController
{
    private IUserService userService;
    private static UserController instance;
    
    private UserController()
    {
        
    }

    public void SetServices(IUserService userService)
    {
        this.userService = userService;
    }
    
    public static UserController GetInstance()
    {
        if (instance == null)
        {
            instance = new UserController();
        }

        return instance;
    }

    
    [Route("/getUser")]
    public LogData GetUser(HttpListenerContext context, LogData logData)
    {
        Console.WriteLine(context.Request.Headers);
        userService.GetUser("tanvihang");
        return logData;
    }
}