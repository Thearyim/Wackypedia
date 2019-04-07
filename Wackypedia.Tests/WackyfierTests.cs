using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wackypedia.Tests
{
    [TestClass]
    public class WackyfierTests
    {
        [TestMethod]
        public void MakeWacky_AppliesMadLibsToTheText()
        {
            string exampleText =
                "This is a wiki article that has information about who knows what. You can find more information " +
                "using random searches online like any respectable person would do. Don't hesitate to click on links to " +
                "enhance your understanding of things around you.";

            Wackyfier wackyfier = new Wackyfier();
            string wackyText = wackyfier.MakeWacky(exampleText);

            Assert.IsNotNull(wackyText);
        }
    }
}
