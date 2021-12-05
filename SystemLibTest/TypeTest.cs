using System;
using System.Data;
using System.Linq;
using Infrastructure.Extensions;
using Npgsql;
using SystemLibTest.Models;
using Xunit;
using Xunit.Abstractions;

namespace SystemLibTest
{
    public class TypeTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TypeTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TypeNameTest()
        {
        //Given
            var typeName = GetType().ToString();
        //When
        
        //Then
            Assert.Equal("SystemLibTest.TypeTest", typeName);
        }

        [Fact]
        public void TestDataRowToPOCO()
        {
            using var dbConnection =
                new Npgsql.NpgsqlConnection("Host=localhost;Port=5432;Username=yim;Database=yim;Password=;");
            dbConnection.Open();

            var command = dbConnection.CreateCommand();
            command.CommandText = "select p.* from public.\"Person\" as p";
            
            
            var sqlAdapter = new NpgsqlDataAdapter(command);
            var dataTable = new DataTable();
            sqlAdapter.Fill(dataTable);
            dbConnection.Close();
            

            var person = dataTable.ToEntities<Person>();
            person.ToList().ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.Name);
                _testOutputHelper.WriteLine(p.Adult.ToString());
                _testOutputHelper.WriteLine(p.Age.ToString());
            });
        }
    }
}