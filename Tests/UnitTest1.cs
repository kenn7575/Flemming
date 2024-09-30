using Azure;
using BL;
using BL.OpenAI;



namespace Tests
{
    public class ImoValidationTests
    {
        [Theory]
        [InlineData("IMO 9179311", false)]//no
        [InlineData("IMO9179311", false)]//no
        [InlineData("12345678", false)]//no
        [InlineData("9179311", true)]//yes
        public void ValidateTest(string input, bool shouldEqualTrue)
        {
            Vessel vessel = new Vessel();
            vessel.ImoNumber = input;

            if(shouldEqualTrue)
            {
                Assert.True(vessel.ValidateImoNumber());
            }
            else
            {
                Assert.False(vessel.ValidateImoNumber());
            }
            

        }
    }

    public class TestJsonParser
    {
        [Theory]
        [InlineData("{\"categoryName\": \"New Pilotage Requests\", \"categoryId\": 1}")]
        [InlineData("{\"categoryName\": \"Cargo Operations\", \"categoryId\": 3}")]
        

    public void ValidateTest(string input)
        {
            
            CategorizeEmailResponse category = new(input);

            CategorizeEmailResponse categorizeEmailResponse = new() { CategoryId = 1, CategoryName = "New Pilotage Requests" };
            
        


            Assert.True(category.CategoryId == categorizeEmailResponse.CategoryId);



        }
    }
}