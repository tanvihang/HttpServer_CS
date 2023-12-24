using System.Net;
using HttpServerBasic.Model;

namespace HttpServerBasic.Controller;

//returns css to user
public class CssController:IController
{
    private static CssController instance;

    private CssController()
    {
        
    }
    
    public static CssController GetInstance()
    {
        if (instance == null)
        {
            instance = new CssController();
        }

        return instance;
    }
    
    
    [Route("/css/main.css")]
    public Result<long> MainCss(HttpListenerContext context)
    {
        //read index file
        string filePath = FileUtils.GetFilePath("/Resources/Static/css/main.css");

        Result<long> result = base.GetFile(context, filePath, "text/css");
        
        return result;
    }

    
    //-------------Base----------------
    
    [Route("/css/base/typography.css")]
    public Result<long> TypographyCss(HttpListenerContext context)
    {
        string filePath = FileUtils.GetFilePath("/Resources/Static/css/base/typography.css");

        Result<long> result = base.GetFile(context, filePath, "text/css");

        return result;
    }
    
    [Route("/css/base/colors.css")]
    public Result<long> ColorsCss(HttpListenerContext context)
    {
        string filePath = FileUtils.GetFilePath("/Resources/Static/css/base/colors.css");

        Result<long> result = base.GetFile(context, filePath, "text/css");

        return result;
    }
    
    [Route("/css/base/theme.css")]
    public Result<long> ThemeCss(HttpListenerContext context)
    {
        string filePath = FileUtils.GetFilePath("/Resources/Static/css/base/theme.css");

        Result<long> result = base.GetFile(context, filePath, "text/css");

        return result;
    }
    
    //-------------Base----------------
    
    
    //-------------FONTS----------------
    [Route("/Resources/font/raleway-regular-webfont.woff2")]
    public Result<long> Font1Css(HttpListenerContext context)
    {
        string filePath = FileUtils.GetFilePath("/Resources/font/raleway-regular-webfont.woff2");

        Result<long> result = base.GetFile(context, filePath, "font/woff2");

        return result;
    }
    
    
    //-------------FONTS----------------

}