using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using netcoreDapper.Models;
using Dapper;
using netcoreDapper.Data;

namespace netcoreDapper.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private  IDbConnection _connection { get { return new SqlConnection(_connectionString); }}

        public ProductRepository()
        {
            // TODO: It will be refactored...
            _connectionString = "server=.;user id=sa;password=StrongPassw0rd;database=RESTfulSampleDb";
            //_connectionString = "Server=.;Database=RESTfulSampleDb;user id=sa;Password=19Kerim87*;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public async Task<Product> GetAsync(long id)
        {
            string query = @"SELECT [Id] ,[CategoryId]
                                ,[Name]
                                ,[Description]
                                ,[Price]
                                ,[CreatedDate]
                                FROM [dbo].[Products]
                                WHERE [Id] = @Id";
            var product =  await _connection.QueryFirstOrDefaultAsync<Product>(query, new{ @Id = id });

            return product;
            // using (IDbConnection dbConnection = _connection)
            // {
            //     string query = @"SELECT [Id] ,[CategoryId]
            //                     ,[Name]
            //                     ,[Description]
            //                     ,[Price]
            //                     ,[CreatedDate]
            //                     FROM [dbo].[Products]
            //                     WHERE [Id] = @Id";

            //     var product = await dbConnection.QueryFirstOrDefaultAsync<Product>(query, new{ @Id = id });

            //     return product;
            // }
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //TODO: Paging...
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [Id] ,[CategoryId]
                                ,[Name]
                                ,[Description]
                                ,[Price]
                                ,[CreatedDate]
                                FROM [dbo].[Products]";

                var product = await dbConnection.QueryAsync<Product>(query);

                return product;
            }
        }

        public async Task AddAsync(Product product)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"INSERT INTO [dbo].[Products] (
                                [CategoryId],
                                [Name],
                                [Description],
                                [Price],
                                [CreatedDate]) VALUES (
                                @CategoryId,
                                @Name,
                                @Description,
                                @Price,
                                @CreatedDate)";

                await dbConnection.ExecuteAsync(query, product);
            }
        }
    }
}