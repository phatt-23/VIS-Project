using MiniGitHub.Data;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;

namespace Test;

internal abstract class Program
{
    private static void Main(string[] args) { 
#if false 
        GlobalConfig.SetDataSource(DataConnectorDataSource.Sqlite);
        IUserDao userDao = GlobalConfig.GetDataConnector().CreateUserDao();
        IRepositoryDao repoDao = GlobalConfig.GetDataConnector().CreateRepositoryDao();
        
        foreach (UserRow userRow in userDao.GetAll()) {
            Console.WriteLine(userRow); 
        }
#else
        // get the DAOs
        GlobalConfig.SetDataSource(DataConnectorDataSource.Text);
        var dataConnector = GlobalConfig.GetDataConnector();
        IUserDao userDao = dataConnector.CreateUserDao();
        IRepositoryDao repoDao = dataConnector.CreateRepositoryDao();

        // User data file ---------------------------- 
        
        // insert a user
        UserRow u = new UserRow {
            Username = "wendell",
            Email = "wendell@vsb.cz",
            Password = "strongRandomPassword",
        };

        userDao.Insert(u);
        
        // show all users
        foreach (UserRow userRow in userDao.GetAll()) {
            Console.WriteLine(userRow); 
        }

        // Repository data file -------------------------------
        
        // get the owners of the repositories
        var phattUser = userDao.GetByUsername("phatt");
        var wendellUser = userDao.GetByUsername("wendell");
        
        // create some repos and insert them
        // for the first user
        RepositoryRow newRepo = new RepositoryRow(
            ownerId: phattUser.UserId,
            name: "VIS-Project",
            description: "projekt do predmetu VIS",
            isPublic: true,
            createdAt: DateTime.Now 
            );

        // for the second user
        RepositoryRow newRepo2 = new RepositoryRow() {
            OwnerId = wendellUser.UserId,
            Name = "Wendell's project",
            Description = "something",
            IsPublic = true,
            CreatedAt = DateTime.Now,
        };
        
        repoDao.Insert(newRepo);
        repoDao.Insert(newRepo2);

        // show all repos
        foreach(var repo in repoDao.GetAll()) {
            Console.WriteLine(repo);
        }

#endif

    }
}
