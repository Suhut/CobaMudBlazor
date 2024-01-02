using MyApp.Shared;

namespace MyApp.UnitTestApi
{
    public class UnitTestFilterDefinitions
    {
        public static IEnumerable<object[]> TestGetPersonItemsData =>
         new List<object[]>
         {
            new object[] 
            {
                new List<GridDataRequestFilterDefinitionsv03> 
                { 
                    new() { FieldName = "Field01", FieldType = "String", Operator = "contains", Value = "1" } 
                },
                "Field01 LIKE '%1%'"
            },
            new object[]
            {
                new List<GridDataRequestFilterDefinitionsv03>
                {
                    new() { FieldName = "Field01", FieldType = "String", Operator = "contains", Value = "1" },
                    new() { FieldName = "Field02", FieldType = "String", Operator = "contains", Value = "2" }
                },
                "Field01 LIKE '%1%' AND Field02 LIKE '%2%'"
            }, 
            new object[]
            {
                new List<GridDataRequestFilterDefinitionsv03>
                {
                    new() { FieldName = "Field01", FieldType = "String", Operator = "contains", Value = "1'" }
                },
                "Field01 LIKE '%1''%'"
            },
         };

        [Theory]
        [MemberData(nameof(TestGetPersonItemsData))]
        public void TestFilterDefinitions(List<GridDataRequestFilterDefinitionsv03> input, string expected)
        {
            //Arrange
            var utils = new GridDataRequestUtils03();

            //Act
            var result = utils.FilterDefinitionsToSql(input);

            //Assert
            Assert.Equal(expected, result);
        } 
    }
}