using Bob.Core.Domain;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Bob.Core.Database
{
    public sealed class AppDataConnection : DataConnection
    {
        static AppDataConnection()
        {
        }

        public AppDataConnection() : base("SqlServer", "Server=localhost;Database=bobshirt;Trusted_Connection=True;TrustServerCertificate=True;") { }
    }
}
