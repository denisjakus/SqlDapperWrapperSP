# Welcome to SqlDapperWrapperSP!

This package is useful for anyone using **SqlDapper** with **stored procedures** when used with repository pattern.

To learn more about **SqlDapper** go to: https://www.learndapper.com/

**QueryDapperHelper** is a wrapper class around SqlDapper. It inherits of **IQueryDapperHelper** interface which basically wraps around all SqlDapper methods. 



**GenericBaseRepository** is a class which needs to be inherited by all other repositories you create so it can instantiate **QueryDapperHelper**.

>**Disclaimer**: 
>These examples are only intended to provide you enough information on how to use SqlDapperWrapperSP functionality 

# Set-up DB connection string in QueryDapperHelper - Example

In order for **QueryDapperHelper** to connect to your database, you need to set-up connection string in it:

```
    public class QueryDapperHelper<T> : IQueryDapperHelper<T> where T : class
    {
		// change name to your connection string
        private const string DBConnectionString = "YourProjectDBConnection"; 
        
        public QueryDapperHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        .
        .
        .
}
```


# Usage in repository - example

If we have in our project class like this one:

    public class YourDomainClass
    {
        public List<int> Datatable1 { get; set; } = new List<int>();
        public YourDomainClassDatatable2 Datatable2 { get; set; }
    }

    public class YourDomainClassDatatable2
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Average { get; set; }
    }

Then in your **DAL.Repositories**  we will have something like this: 
```
public class YourDomainClassRepository : GenericBaseRepository<YourDomainClass>, IGenericRepository
{
	public YourDomainClassRepository(IConfiguration configuration) : base(configuration)
	{

	}
	// this one is straight forward; maps result with our domain class 1:1
	public async Task<YourDomainClass> GetOneAsync(Guid id)
	{
		return await QueryDapperHelper.QuerySingleOrDefaultAsync(
				 "sp_YourStoredProcedureName",
				 new { Id = id});
	}

	// a trickier example when we expect N resultsets/datatables from SP query
	public async Task<YourDomainClass> GetAllPropsAsync(Guid param1, Guid param2, int param3)
	{
		// here we add to list all expected types that are going to be mapped from multiple datatables (resultsets) received after SP executes
		List<Type> types = new List<Type>
		{
			typeof(int),
			typeof(YourDomainClassDatatable2)
		};


		// execute our wrapper method to retrieve multiple  resultsets
		var results = await QueryDapperHelper.QueryMultipleAsync(types,
			 "sp_YourStoredProcedureName",
			 new
			 {
				 Param1 = param1,
				 Param2 = param2,
				 Param3 = param3
			 });

		YourDomainClass returnValue = new YourDomainClass();
		
		var datatable1 = ((List<object>)results[0]);
		var datatable2 = ((List<object>)results[1]);

		for(var i = 0; i < datatable1.Count; i++)
		{
			var someNumber = Convert.ToInt32(datatable1[i]);
			returnValue.Datatable1.Add(someNumber);
		}

		returnValue.Datatable2 = datatable2[0] as YourDomainClassDatatable2;

		return returnValue;
	}
}

```
Hopefully this is clear enough. If any questions, send me an email.
Cheers.
