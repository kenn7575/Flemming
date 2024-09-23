using BL;



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
}