using System.Net;
using HttpServerBasic.Model;
using HttpServerBasic.Sys.Service;

namespace HttpServerBasic.Controller;

public class UserController
{
    private IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    
    [Route("/getUser")]
    public LogData GetUser(HttpListenerContext context, LogData logData)
    {
        return logData;
    }
}