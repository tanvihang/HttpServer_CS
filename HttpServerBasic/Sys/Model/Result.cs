using HttpServerBasic.Response.Enums;

namespace HttpServerBasic.Model;

public class Result<T>
{
    private int statusCode;
    private string message;
    private T data;

    //constructor
    public Result(int statusCode, string message)
    {
        this.statusCode = statusCode;
        this.message = message;
    }
    
    public Result(int statusCode, string message, T data)
    {
        this.statusCode = statusCode;
        this.message = message;
        this.data = data;
    }
    
    //Results
    public static Result<T> Success(string message)
    {
        return new Result<T>((int)Response.Enums.Success.ONE, message);
    }
    
    public static Result<T> Success(int statusCode, string message)
    {
        return new Result<T>(statusCode, message);
    }

    public static Result<T> Success(int statusCode, string message, T data)
    {
        return new Result<T>(statusCode, message, data);
    }


    public static Result<T> Fail(string message)
    {
        return new Result<T>((int)Response.Enums.ClientError.ONE, message);
    }
    
    public static Result<T> Fail(int statusCode, string message)
    {
        return new Result<T>(statusCode, message);
    }

    public static Result<T> Fail(int statusCode, string message, T data)
    {
        return new Result<T>(statusCode, message, data);
    }
    
}