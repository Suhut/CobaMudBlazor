using MyApp.Shared;

namespace MyApp.UnitTestApi
{
    public class UnitTestSortDefinitions
    {
        public static IEnumerable<object[]> TestGetPersonItemsData =>
         new List<object[]>
         {
            new object[] 
            {
                new List<GridDataRequestSortDefinitionsv03> 
                { 
                    new() { SortBy = "Field01", Descending=false} 
                },
                "Field01 ASC"
            },
            new object[]
            {
                new List<GridDataRequestSortDefinitionsv03>
                {
                    new() { SortBy = "Field01", Descending=false},
                    new() { SortBy = "Field02", Descending=true}
                },
                "Field01 ASC, Field02 DESC"
            }
         };

        [Theory]
        [MemberData(nameof(TestGetPersonItemsData))]
        public void TestSortDefinitions(List<GridDataRequestSortDefinitionsv03> input, string expected)
        {
            //Arrange
            var utils = new GridDataRequestUtils03();

            //Act
            var result = utils.SortDefinitionsToSql(input);

            //Assert
            Assert.Equal(expected, result);
        } 
    }
}