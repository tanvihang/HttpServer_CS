using System.Net;
using System.Runtime.CompilerServices;
using HttpServerBasic.Model;
using HttpServerBasic.Sys.Repository;

namespace HttpServerBasic.Sys.Service.Impl;

public class UserService:IUserService
{
    private static UserService instance;
    private IUserRepository repository;
    
    private UserService()
    {
        
    }
    
    public static UserService GetInstance()
    {
        if (instance == null)
        {
            instance = new UserService();
        }

        return instance;
    }

    public void SetRepository(IUserRepository repository)
    {
        this.repository = repository;
    }

    public bool GetUser(string userName)
    {
        User user = repository.GetUser(userName);
        return true;
    }
}