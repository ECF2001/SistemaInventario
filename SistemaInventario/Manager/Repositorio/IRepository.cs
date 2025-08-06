using Manager.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Repositorio
{
    public interface IRepository
    {
        Task<T?> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;

        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task<T> AddAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity;
        Task<IEnumerable<T>> AddRangeAsync<T>(List<T> entities) where T : BaseEntity;

        Task<IEnumerable<T>> AddRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction)
            where T : BaseEntity;

        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity;
        Task<IEnumerable<T>> UpdateRangeAsync<T>(List<T> entities) where T : BaseEntity;

        Task<IEnumerable<T>> UpdateRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction)
            where T : BaseEntity;

        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity;
        Task DeleteRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity;
    }
}
