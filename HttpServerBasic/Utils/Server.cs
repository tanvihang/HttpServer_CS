using System.Net;
using HttpServerBasic.Controller;
using HttpServerBasic.Sys.Repository;
using HttpServerBasic.Sys.Repository.Impl;
using HttpServerBasic.Sys.Service;
using HttpServerBasic.Sys.Service.Impl;
using Microsoft.Extensions.Configuration;

namespace HttpServerBasic;

class Server
{
    private IConfiguration _configuration;

    private const int MAX_CONNECTION = 10;
    public static int CONNECTIONS = 0;
     
    public Queue<HttpListenerContext> listOfHttpListenerContext = new Queue<HttpListenerContext>();

    private List<IController> controllers = new List<IController>();
    
    public Server(IConfiguration configuration)
    {
        _configuration = configuration;
        
        //这边去找Controller底下的所有文件并初始化他们在这里，这样我既可以让Controller知道应该使用哪一些Service实例（Dependency Injection）
        //还有要思考的是他应该只包含一个实例，不然每一个Worker都需要去创建就很麻烦。
        //Dependency Injection, Can change the service used here.
        //Repository
        string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Get<string>();
        IUserRepository userRepository = UserRepository.GetInstance();
        userRepository.SetConnectionString(connectionString);
        
        //Services
        IUserService userService = UserService.GetInstance(); 
        userService.SetRepository(userRepository);
        
        //Create controller instance
        WebIndex webController = WebIndex.GetInstance();
        webController.SetServices(userService);

        UserController userController = UserController.GetInstance();
        userController.SetServices(userService);

        CssController cssController = CssController.GetInstance();
        
        //------------------------------------------------------------------
        
        //Combine controller instance
        controllers.Add(webController);
        controllers.Add(userController);
        controllers.Add(cssController);

    }
    
    public void Run()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(_configuration.GetRequiredSection("URI").Get<string>());
        
        listener.Start();
        Console.WriteLine($"Listening for requests at {_configuration.GetRequiredSection("URI").Get<string>()}");
        
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

    public void NewConnection(object obj)
    {
        Queue<HttpListenerContext> q = obj as Queue<HttpListenerContext>;
        HttpListenerContext context = q.Dequeue();
        
        //continue until meet request
        Worker newWorker = new Worker(context, ref controllers);
        newWorker.Run();
    }
}