# HTTP Server using C#
A project based on Http protocol to build a server.
- design model used: 
  - MVC
  - Dependency Injection
  - Repository method
- Some key usage including: **Attribute**,**Type**, **ThreadPool** in C#
- Server Side Rendering
---

**TODO**
| Task               | IsComplete      |
| ------------------ | --------------- |
| Log File           | 2023/12/17 Done |
| Controller         | 2023/12/17 Done |
| Service            | 2023/12/21 Done |
| View               |                 |
| Dynamic Web        |                 |
| Integrate Database | 2023/12/21 Done |

Last edited @2023/12/18 16:30
1. ~~Singleton for Service~~
2. ~~Loop through controller folder to make instance for them~~
3. Make a simple home page return HTML file
4. Test how user send post and head request
---

# Integrate Database
## 1. use repository pattern for database operation
   1. 不同类型的操作放在不同的文件地下
   2. 例如，user只用user, post只用post这样吧，我不确定


## 2. **dependency injection** for managing SQL connection
   1. 不依赖于实体，依赖于abstracitons，让代码更容易管理
   2. 2023/12/18 


```cs
//Interface 
public interface IUserRepository{
    void CreateUser(string username, string password);
}

//Concrete implementation IUserRepository
public class UserRepository:IUserRepository{
    //implementation for production
    void CreateUser(string username, string password){
        //implementation of database operation
    }
}

//Concrete Implementation for testing IUserRepository
public class UserRepositoryTest:IUserRepository{
    //implementation for testing
    void CreateUser(string username, string password){
        //implementation ...
    }
}

public class SimpleHttpServer{
    private readonly IUserRepository _userRepository;

    public SimpleHttpServer(IUserRepository userRepository){
        this._userRepository = userRepository;
    }

    public void HandleCreateUserRequest(string username, string password){
        userRepository.CreateUser(username, password);
    }
}

```
## 3. asynchronous methods for database operation



## 4. configuration management for easier to change configurations without modifying code
https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration
![Alt text](assets/image.png)
在configuration这一块，可以用到.NET提供的`Configuration`包。
```cs
    string baseDirectory = AppContext.BaseDirectory;
    string rootDirectory = Path.Combine(baseDirectory, "..", "..","..");
    rootDirectory = Path.GetFullPath(rootDirectory);
    rootDirectory += "/appsettings.json";
    
    //初始化
    IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(rootDirectory)
        .AddEnvironmentVariables()
        .Build();

    //使用
    config.GetRequiredSection("URI").Get<string>()；
```

