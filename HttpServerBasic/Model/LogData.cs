using System.Runtime.InteropServices.JavaScript;

namespace HttpServerBasic.Model;

public class LogData
{
    private string ipAddr;
    private string identity = null;
    private string id;
    private DateTime date;
    private string method;
    private string filePath;
    private string fileName;
    private int statusCode;
    private long fileSize;
    private string referer = null;

    public LogData(string ipAddr, string id, string method, string filePath, string fileName, string referer )
    {
        this.ipAddr = ipAddr;
        this.id = id;
        this.method = method;
        this.filePath = filePath;
        this.fileName = fileName;
        this.referer = referer;
        this.date = DateTime.Now;
    }
    
    public override string ToString()
    {
        string datee = date.ToString("yyyy-M-d dddd h:mm:ss tt zz");
        var msg = $"IpAddr:{ipAddr} UserID:{id} -- [{datee}] \" {method} {filePath} {statusCode} {fileSize} -- {referer} \"";

        return msg;
    }

    public string IpAddr
    {
        get => ipAddr;
        set => ipAddr = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Identity
    {
        get => identity;
        set => identity = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Id
    {
        get => id;
        set => id = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Date
    {
        get => date;
        set => date = value;
    }

    public string Method
    {
        get => method;
        set => method = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FilePath
    {
        get => filePath;
        set => filePath = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FileName
    {
        get => fileName;
        set => fileName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int StatusCode
    {
        get => statusCode;
        set => statusCode = value;
    }

    public long FileSize
    {
        get => fileSize;
        set => fileSize = value;
    }

    public string Referer
    {
        get => referer;
        set => referer = value ?? throw new ArgumentNullException(nameof(value));
    }
}