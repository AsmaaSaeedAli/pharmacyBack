using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Pharmacy.Tests.General
{
    // ReSharper disable once InconsistentNaming
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=Pharmacy; Trusted_Connection=True;");
            csb["Database"].ShouldBe("Pharmacy");
        }
    }
}
