using System.Net;
using System.Threading.Channels;
using HttpServerBasic.Controller;

namespace HttpServerBasic;

public class Worker:WebIndex
{
    private HttpListenerContext _context;
    
    public Worker(HttpListenerContext context)
    {
        _context = context;
    }

    public void run()
    {
        HttpListenerRequest request = _context.Request;
        HttpListenerResponse response = _context.Response;
        
        string fileName = (request.Url).ToString().Split("/")[3];
        string method = request.HttpMethod;

        if (fileName.Length == 0)
        {
            fileName = "index";
        }

        fileName = "/" + fileName;
        
        //check if we have the handler for this request
        var handler = FindHandler(method, fileName);

        if (handler != null)
        {
            // TODO implement WriteLog when succeed
            WriteLog();
            handler(_context);
        }
        else
        {
            // TODO implement WriteLog when not found
            WriteLog();
            response.StatusCode = (int)Response.Enums.ClientError.FOUR;
            response.Close();
        }
        
        // Thread thread = Thread.CurrentThread;
        // string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
        // Console.WriteLine(message);
        
        IInfoProvider.COUNT = IInfoProvider.COUNT - 1;
    }

    public Action<HttpListenerContext> FindHandler(string method, string path)
    {
        
        var handlers = GetType().GetMethods();

        foreach (var handler in handlers)
        {
            var attribute = (RouteAttribute)Attribute.GetCustomAttribute(handler, typeof(RouteAttribute));
            
            if (attribute != null && attribute.Path == path)
            {
                //创建一个delegate函数指针类似的东西
                //该delegate返回Action<HttpListenerContext>
                //接收参数未
                Console.WriteLine(handler);
                return (Action<HttpListenerContext>)Delegate.CreateDelegate(typeof(Action<HttpListenerContext>), this,handler);
            }
            
        }

        return null;
    }

    // TODO write logic of WriteLog method 
    public void WriteLog()
    {
        
    } 
    
}