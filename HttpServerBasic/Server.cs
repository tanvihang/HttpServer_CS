using System.Net;

namespace HttpServerBasic;

class Server
{
    const string PORT = "8080";
    private const string PROTOCOL = "http://";
    private const string DOMAIN = "localhost";
    
    private const int MAX_CONNECTION = 10;
    public static int CONNECTIONS = 0;
    
    
    
    public static Queue<HttpListenerContext> listOfHttpListenerContext = new Queue<HttpListenerContext>();
    
    public static void Main(string[] args)
    {
        const string URI = PROTOCOL + DOMAIN + ":" + PORT + "/";
        
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(URI);
        
        listener.Start();
        Console.WriteLine($"Listening for requests at {URI}");
        
        ThreadPool.SetMinThreads(1, 0);
        ThreadPool.SetMaxThreads(1, 0);
        
        while (true)
        {
            //listener.GetContext() is blocked until a request come
            HttpListenerContext context = listener.GetContext();
            listOfHttpListenerContext.Enqueue(context);

            if (IInfoProvider.COUNT < MAX_CONNECTION)
            {
                IInfoProvider.COUNT = IInfoProvider.COUNT + 1;
                ThreadPool.QueueUserWorkItem(NewConnection, listOfHttpListenerContext as object);
            }
            
        }
        
    }

    public static void NewConnection(object obj)
    {
        Queue<HttpListenerContext> q = obj as Queue<HttpListenerContext>;
        HttpListenerContext context = q.Dequeue();
        
        //continue until meet request
        Worker newWorker = new Worker(context);
        newWorker.Run();
    }
}