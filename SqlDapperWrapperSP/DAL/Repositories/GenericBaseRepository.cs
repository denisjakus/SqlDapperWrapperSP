/// <summary>
///   Author:    Denis Jakus
///    email:    djakus@outlook.com
///      web:    https://www.denisjakus.com
/// linkedIn:    https://www.linkedin.com/in/denisjakus/
/// </summary>
using Microsoft.Extensions.Configuration;
using SqlDapperWrapperSP.DAL.Contracts;


namespace SqlDapperWrapperSP.DAL.Repositories
{
    public class GenericBaseRepository<T> where T : class
    {
        public GenericBaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;

            QueryDapperHelper = new QueryDapperHelper<T>(Configuration);
        }

        public IQueryDapperHelper<T> QueryDapperHelper;
        private IConfiguration Configuration { get; }
    }
}
