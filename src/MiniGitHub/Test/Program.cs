using MiniGitHub.Data;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;

namespace Test;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var userDao = new UserDao();
        UserRow? row = userDao.GetById(1);
        
        var repoDao = new RepositoryDao();
        RepositoryRow? repo = repoDao.GetById(1);

        Console.WriteLine(row);
        Console.WriteLine(repo);
    }
}