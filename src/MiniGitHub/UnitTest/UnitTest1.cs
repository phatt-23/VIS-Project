using Microsoft.Data.Sqlite;
using MiniGitHub.Data;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;

namespace UnitTest;

public class SqlDaoTest {
    [SetUp]
    public void Setup() {
        GlobalConfig.SetDataSourceType(DataSourceType.Sqlite); 
        GlobalConfig.SetDataSource("MiniGitHubDB.test.sqlite3");
        
        _dataConnector = GlobalConfig.GetDataConnector();
        _userDao = _dataConnector.CreateUserDao(); 
        _repoDao = _dataConnector.CreateRepositoryDao(); // remove everything List<UserRow> users = _userDao.GetAll();

        List<UserRow> users = _userDao.GetAll(); 
        foreach (UserRow user in users) {
            _userDao.Delete(user.Id);
        }
        
        List<RepositoryRow> repos = _repoDao.GetAll();
        foreach (RepositoryRow repo in repos) {
            _repoDao.Delete(repo.Id);
        }
    }

    [Test]
    public void Test1() {
        UserRow usr = new UserRow() {
            Username = "phatt",
            Email = "phatt@phatt",
            Password = "123",
        };

        UserRow ret = _userDao.Insert(usr);

        Assert.That(usr.Username, Is.EqualTo(ret.Username));
        Assert.That(usr.Email, Is.EqualTo(ret.Email));
        Assert.That(usr.Password, Is.EqualTo(ret.Password));

        // username unique constraint
        Assert.Throws<SqliteException>(() => {
            _userDao.Insert(usr);
        });
    }

    private IDataConnector _dataConnector;
    private IUserDao _userDao;
    private IRepositoryDao _repoDao;
}