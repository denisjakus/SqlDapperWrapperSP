/// <summary>
///   Author:    Denis Jakus
///    email:    djakus@outlook.com
///      web:    https://www.denisjakus.com
/// linkedIn:    https://www.linkedin.com/in/denisjakus/
/// </summary>
using Dapper;
using Microsoft.Extensions.Configuration;
using SqlDapperWrapperSP.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SqlDapperWrapperSP
{
    public class QueryDapperHelper<T> : IQueryDapperHelper<T> where T : class
    {

        private const string DBConnectionString = "YourProjectDBConnection"; // change name to your connection string
        public QueryDapperHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// The ExecuteAsync can execute a command one or multiple times asynchronously and return the number of affected rows.
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Returns the number of affected rows</returns>
        public async Task<int> ExecuteAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// The QueryAsync can execute a query and map the result asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type T</returns>
        public async Task<IEnumerable<T>> QueryAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QueryAsync<T>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// The QueryFirstAsync can execute a query and map asynchronously the first result.
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type T</returns>
        public async Task<T> QueryFirstAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QueryFirstAsync<T>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Executes stored procedure with parameters and returns first or default result for executed stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type T</returns>
        public async Task<T> QueryFirstOrDefaultAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<T>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// The QuerySingleAsync can execute a query and map asynchronously the first result and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type T</returns>
        public async Task<T> QuerySingleAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QuerySingleAsync<T>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// The QuerySingleOrDefaultAsync can execute a query and map asynchronously the first result, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type T</returns>
        public async Task<T> QuerySingleOrDefaultAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<T>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Executes stored procedure with parameters and returns bool if first or default result found for executed stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type bool</returns>
        public async Task<bool> QueryFirstOrDefaultBoolAsync(string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<bool>(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Executes stored procedure with parameters and returns list of objects / datatables
        /// </summary>
        /// <param name="types">List of type classes expected as a resultsets/datatables</param>
        /// <param name="storedProcedureName">Name of stored procedure to be executed</param>
        /// <param name="parameters">One or more parameters to be sent to stored procedure</param>
        /// <returns>Maps the result to type List of object</returns>

        public async Task<List<object>> QueryMultipleAsync(List<Type> types, string storedProcedureName, object parameters)
        {
            using var connection = new SqlConnection(Configuration.GetConnectionString(DBConnectionString));

            await connection.OpenAsync();

            var result = await connection.QueryMultipleAsync(
                        storedProcedureName, // name of stored procedure
                        parameters, // params for stored procedure
                        commandType: System.Data.CommandType.StoredProcedure);

            return (from type in types
                    select result.Read(type)).ToList<object>();

        }
    }

}
