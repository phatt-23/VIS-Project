using MiniGitHub.Data;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Repositories;

namespace Test;

internal abstract class Program
{
    private static void Main(string[] args) {
        IUserDao userDao = GlobalConfig.GetDataConnector().CreateUserDao();
        IRepositoryDao repoDao = GlobalConfig.GetDataConnector().CreateRepositoryDao();
        
        foreach (UserRow userRow in userDao.GetAll()) {
            Console.WriteLine(userRow); 
        }

    }
}