using System.Net;

namespace HttpServerBasic.Controller;

public class WebIndex
{
    [Route("/index")]
    public void Index(HttpListenerContext context)
    {
        var response = context.Response;
        //Construct a response according to request
        string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
        //conver to byte so it can be sent through the network
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;

        output.Write(buffer, 0, buffer.Length);

        output.Close();
    }

    [Route("/about")]
    public void About(HttpListenerContext context)
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
    }
}