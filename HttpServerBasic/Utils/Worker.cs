using System.Net;
using HttpServerBasic.Controller;
using HttpServerBasic.Model;

namespace HttpServerBasic;

public class Worker:WebIndex
{
    private HttpListenerContext _context;
    
    public Worker(HttpListenerContext context)
    {
        _context = context;
    }

    public void Run()
    {
        HttpListenerRequest request = _context.Request;
        HttpListenerResponse response = _context.Response;

        string fileName = request.RawUrl;
        string method = request.HttpMethod;

        if (fileName == "/")
        {
            fileName = "/index";
        }
        
        //check if we have the handler for this request
        var handler = FindHandler(method, fileName);

        //create log
        LogData logData = new LogData(request.UserHostAddress, "1", request.HttpMethod, request.Url.ToString(), fileName,
            "No URL Referrer");
        
        if (handler != null)
        {
            logData = handler(_context, logData);
            WriteLog(logData);
        }
        else
        {
            response.StatusCode = (int)Response.Enums.ClientError.FOUR;
            logData.StatusCode = (int)Response.Enums.ClientError.FOUR;
            response.Close();
            WriteLog(logData);
        }
        
        // Thread thread = Thread.CurrentThread;
        // string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
        // Console.WriteLine(message);
        
        IInfoProvider.COUNT = IInfoProvider.COUNT - 1;
    }

    public Func<HttpListenerContext, LogData, LogData> FindHandler(string method, string path)
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
                return (Func<HttpListenerContext, LogData, LogData>)Delegate.CreateDelegate(typeof(Func<HttpListenerContext, LogData, LogData>), this,handler);
            }
            
        }

        return null;
    }
    
    public void WriteLog(LogData logData)
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = $"../../../Resources/Log/{IInfoProvider.Date}.log";
        
        File.AppendAllText(filePath,logData.ToString() + Environment.NewLine);
    } 
    
}