using System.Net;
using HttpServerBasic.Model;

namespace HttpServerBasic.Controller;

public class IController
{
    protected Result<long> GetFile(HttpListenerContext context, string filePath, string contentType)
    {
        string content = File.ReadAllText(filePath);

        var response = context.Response;
        response.ContentType = contentType;

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);

        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        
        output.Write(buffer,0,buffer.Length);
        output.Close();
        
        return Result<long>.Success((int)Response.Enums.Success.ONE,$"Got file {filePath}", buffer.Length);
    }
}