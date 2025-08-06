using Manager.Repositorio;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Datos
{
    public class EfRepository(AppDbContext.AppDbContext dbContext) : IRepository
    {
        private protected readonly AppDbContext.AppDbContext _dbContext = dbContext;

        public T? GetById<T>(int id) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            return connection.Query<T>($"SELECT * FROM {typeof(T).Name} where Id = '{id}'").SingleOrDefault();
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            var sql = $"SELECT * FROM {typeof(T).Name} where Id = '{id}';";
            return await connection.QuerySingleOrDefaultAsync<T>(sql).ConfigureAwait(false);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            var sql = $"SELECT * FROM {typeof(T).Name}";
            return (await connection.QueryAsync<T>(sql)).ToList();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            return await AddAsync(entity, connection, null);
        }

        public async Task<T> AddAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity
        {
            await connection.InsertAsync(entity, transaction: transaction);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync<T>(List<T> entities) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            return await AddRangeAsync(entities, connection, null);
        }

        public async Task<IEnumerable<T>> AddRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity
        {
            await connection.InsertAsync(entities, transaction: transaction);
            return entities;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            await UpdateAsync(entity, connection, null);
        }

        public async Task UpdateAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity
        {
            await connection.UpdateAsync(entity, transaction: transaction);
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(List<T> entities) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            return await UpdateRangeAsync(entities, connection, null);
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity
        {
            await connection.UpdateAsync(entities, transaction: transaction);
            return entities;
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            using var connection = _dbContext.CreateConnection();
            await DeleteAsync(entity, connection, null);
        }

        public async Task DeleteAsync<T>(T entity, IDbConnection connection, IDbTransaction transaction) where T : BaseEntity
        {
            await connection.DeleteAsync(entity, transaction: transaction);
        }

        public async Task DeleteRangeAsync<T>(List<T> entities, IDbConnection connection, IDbTransaction transaction)
            where T : BaseEntity
        {
            foreach (var baseEntity in entities)
            {
                await connection.DeleteAsync(baseEntity, transaction: transaction);
            }
        }
    }
}
