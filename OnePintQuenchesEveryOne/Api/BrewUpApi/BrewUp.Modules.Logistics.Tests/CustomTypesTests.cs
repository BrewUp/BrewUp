using BrewUp.Modules.Logistics.Extensions.CustomTypes;

namespace BrewUp.Modules.Logistics.Tests
{
    public class CustomTypesTests
    {
        [Fact]
        public void Can_Compare_Two_Address()
        {
            var address1 = new Address("Via Marconi");
            var address2 = new Address("Via Marconi");

            Assert.True(address1.Equals(address2));
        }

        [Fact]
        public void Cannot_Create_Empty_Address()
        {
            var address1 = new Address(" ");

            Assert.Equal(" ", address1.Value);
        }
    }
}