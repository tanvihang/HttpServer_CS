using System.Net;
using System.Runtime.InteropServices.JavaScript;

namespace HttpServerBasic;

public static class IInfoProvider
{
    //Keep track of maximum count of threads
    private static int _COUNT = 0;

    //keep the HTTPRequest
    private static Queue<HttpListenerContext> _listenerContexts = new Queue<HttpListenerContext>();

    private static string _date;

    static IInfoProvider()
    {
        _date = DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss");
    }

    public static string Date => _date;

    public static int COUNT
    {
        get
        {
            return _COUNT;
        }
        set
        {
            _COUNT = value;
        }
    }

    public static HttpListenerContext HttpListenerContextItem
    {
        get
        {
            return _listenerContexts.Dequeue();
        }
        set
        {
            _listenerContexts.Enqueue(value);
        }
    }
}