/// <summary>
///   Author:    Denis Jakus
///    email:    djakus@outlook.com
///      web:    https://www.denisjakus.com
/// linkedIn:    https://www.linkedin.com/in/denisjakus/
/// </summary>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlDapperWrapperSP.DAL.Contracts
{
    public interface IQueryDapperHelper<T> where T : class
    {
        Task<int> ExecuteAsync(string storedProcedureName, object parameters = null);
        Task<IEnumerable<T>> QueryAsync(string storedProcedureName, object parameters = null);
        Task<T> QueryFirstAsync(string storedProcedureName, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync(string storedProcedureName, object parameters = null);
        Task<T> QuerySingleAsync(string storedProcedureName, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync(string storedProcedureName, object parameters = null);
        Task<bool> QueryFirstOrDefaultBoolAsync(string storedProcedureName, object parameters = null);
        Task<List<object>> QueryMultipleAsync(List<Type> types, string storedProcedureName, object parameters = null);
    }
}
