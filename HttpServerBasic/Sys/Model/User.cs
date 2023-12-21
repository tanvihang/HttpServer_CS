namespace HttpServerBasic.Model;

public class User
{
    private string userName;
    private string password;

    public User()
    {
    }

    public User(string userName, string password)
    {
        this.userName = userName;
        this.password = password;
    }

    public string UserName
    {
        get => userName;
        set => userName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => password;
        set => password = value ?? throw new ArgumentNullException(nameof(value));
    }
}