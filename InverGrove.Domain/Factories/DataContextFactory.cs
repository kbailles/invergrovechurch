using System.Configuration;
using InverGrove.Data;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Factories
{
    public class DataContextFactory : IDataContextFactory
    {
        private const string Metadata = "res://*/InvergroveDb.csdl|res://*/InvergroveDb.ssdl|res://*/InvergroveDb.msl";
        private const string DataProvider = "System.Data.SqlClient";

        public static IDataContextFactory Create()
        {
            return new DataContextFactory();
        }

        /// <summary>
        /// Gets the object context.
        /// </summary>
        /// <returns></returns>
        public object GetObjectContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["InverGroveContext"].ConnectionString;

            //var connectionStringBuilder = new EntityConnectionStringBuilder
            //                                  {
            //                                      Metadata = Metadata,
            //                                      Provider = DataProvider,
            //                                      ProviderConnectionString = connectionString
            //                                  };

            return new InverGroveContext(connectionString);
        }
    }
}