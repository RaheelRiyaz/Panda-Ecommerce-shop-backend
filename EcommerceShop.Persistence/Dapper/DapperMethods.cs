using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Dapper
{
    public static class DapperMethods
    { 



        // Query Async
        public static async Task<IEnumerable<T>> QueryAsync<T>
            (this DbContext dbContext,
            string sql,
            object? param =default,
            IDbTransaction? dbTransaction = null,
            CommandType commandType = CommandType.Text,
            int? commandtimeOut = null
            )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());

            sqlConnection.Open();

            return await sqlConnection.QueryAsync<T>(sql, param, dbTransaction, commandtimeOut, commandType);
        }




        // Firstordefaultasync
        public static async Task<T?> FirstOrDefaultAsync<T>
           (this DbContext dbContext,
           string sql,
           object? param,
           IDbTransaction? dbTransaction = null,
           CommandType commandType = CommandType.Text,
           int? commandtimeOut = null
           )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());

            sqlConnection.Open();
            return await sqlConnection.QueryFirstOrDefaultAsync<T>(sql, param, dbTransaction, commandtimeOut, commandType);
        }




        // ExecuteAsyc
        public static async Task<int> ExecuteAsync
          (this DbContext dbContext,
          string sql,
          object? param = default,
          IDbTransaction? dbTransaction = null,
          CommandType commandType = CommandType.Text,
          int? commandtimeOut = null
          )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());

            sqlConnection.Open();
            return await sqlConnection.ExecuteAsync(sql, param, dbTransaction, commandtimeOut, commandType);
        }
    }
}
