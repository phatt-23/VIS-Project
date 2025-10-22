using MiniGitHub.Data;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services.TransactionScriptPattern;

namespace Test;

internal abstract class Program {
    private static void Main() {
        // TestDataLayerSql();
        // TestDataLayerText();

        TestTransactionScriptPattern();
    }

    private static void TestTransactionScriptPattern() {
        // get the DAOs
        GlobalConfig.SetDataSourceType(DataSourceType.Sqlite);
        IDataConnector dataConnector = GlobalConfig.GetDataConnector();

        IUserDao userDao = dataConnector.CreateUserDao();
        IRepositoryDao repoDao = dataConnector.CreateRepositoryDao();

        // remove all repos and users
        foreach (var repo in repoDao.GetAll()) {
            repoDao.Delete(repo.RepositoryId);
        }

        foreach (var user in userDao.GetAll()) {
            userDao.Delete(user.UserId);
        }

        // User data file ---------------------------- 

        // insert a user
        UserRow u1 = new UserRow {
            Username = "wendell",
            Email = "wendell@vsb.cz",
            Password = "strongRandomPassword",
        };

        UserRow u2 = new UserRow {
            Username = "phatt",
            Email = "tra0163@vsb.cz",
            Password = "strongRandomPassword",
        };

        userDao.Insert(u1);
        userDao.Insert(u2);

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
        foreach (var repo in repoDao.GetAll()) {
            Console.WriteLine(repo);
        }
        
        // get by calling transaction script
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Getting user with their repositories...");

        var script = new GetUserWithRepositoriesTS(dataConnector);
        
        User retUser = script.Run(phattUser.UserId)!;
        
        Console.WriteLine(retUser);
        foreach (var repo in retUser.Repositories) {
            Console.WriteLine(repo);
        }
    }

    private static void TestDataLayerSql() {
        // get the DAOs
        GlobalConfig.SetDataSourceType(DataSourceType.Sqlite);
        IDataConnector dataConnector = GlobalConfig.GetDataConnector();

        IUserDao userDao = dataConnector.CreateUserDao();
        IRepositoryDao repoDao = dataConnector.CreateRepositoryDao();

        // remove all repos and users
        foreach (var repo in repoDao.GetAll()) {
            repoDao.Delete(repo.RepositoryId);
        }

        foreach (var user in userDao.GetAll()) {
            userDao.Delete(user.UserId);
        }

        // User data file ---------------------------- 

        // insert a user
        UserRow u1 = new UserRow {
            Username = "wendell",
            Email = "wendell@vsb.cz",
            Password = "strongRandomPassword",
        };

        UserRow u2 = new UserRow {
            Username = "phatt",
            Email = "tra0163@vsb.cz",
            Password = "strongRandomPassword",
        };

        userDao.Insert(u1);
        userDao.Insert(u2);

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
        foreach (var repo in repoDao.GetAll()) {
            Console.WriteLine(repo);
        }

        
    }
    
    private static void TestDataLayerText() {
        // get the DAOs
        GlobalConfig.SetDataSourceType(DataSourceType.Text);
        IDataConnector dataConnector = GlobalConfig.GetDataConnector();

        IUserDao userDao = dataConnector.CreateUserDao();
        IRepositoryDao repoDao = dataConnector.CreateRepositoryDao();

        // remove all repos and users
        foreach (var repo in repoDao.GetAll()) {
            repoDao.Delete(repo.RepositoryId);
        }

        foreach (var user in userDao.GetAll()) {
            userDao.Delete(user.UserId);
        }

        // User data file ---------------------------- 

        // insert a user
        UserRow u1 = new UserRow {
            Username = "wendell",
            Email = "wendell@vsb.cz",
            Password = "strongRandomPassword",
        };

        UserRow u2 = new UserRow {
            Username = "phatt",
            Email = "tra0163@vsb.cz",
            Password = "strongRandomPassword",
        };

        userDao.Insert(u1);
        userDao.Insert(u2);

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
        foreach (var repo in repoDao.GetAll()) {
            Console.WriteLine(repo);
        }

        
    }



}
