using System.Net;
using HttpServerBasic.Model;
using HttpServerBasic.Sys.Repository;

namespace HttpServerBasic.Sys.Service;

public interface IUserService
{
    public void SetRepository(IUserRepository userRepository);
    public bool GetUser(string userName);
}