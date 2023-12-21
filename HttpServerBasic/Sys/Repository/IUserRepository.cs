using HttpServerBasic.Model;

namespace HttpServerBasic.Sys.Repository;

public interface IUserRepository
{
    public void SetConnectionString(string connectionString);
    public User GetUser(string userName);
}