using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.DAOs;

public interface IDao<T> {
    T GetById(long id);
    List<T> GetAll();
    T Insert(T entity);
    T Update(T entity);
    bool Delete(long id);
}