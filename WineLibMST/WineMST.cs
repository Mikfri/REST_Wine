using WineLib.Models;

namespace WineLibMST
{
    [TestClass]
    public class WineMST
    {


        private Wine wine = new() { Id = 1, Manufacturer = "Royal", Rating = 2 };
        private Wine wine2 = new() { Id = 1, Manufacturer = "Royal2" };
        private Wine wineTooLowRating = new() { Id = 2, Manufacturer = "TooLowRating", Rating = 1 };
        private Wine wineTooHighRating = new() { Id = 3, Manufacturer = "TooHighRating", Rating = 6 };
        private Wine wineZeroRating = new() { Id = 5, Manufacturer = "ZeroRating", Rating = 0 };


        //---: TO STRING
        [TestMethod]
        public void ToStringTest()
        {
            string wineString = wine.ToString();            // Act
            Assert.AreEqual("1 Royal 0 0 2", wineString);  // Assert (0 = year, 0 = price.. De er ikke angivet)
            string wineString2 = wine2.ToString();         // Act
            Assert.AreEqual("1 Royal2 0 0 0", wineString2);  // Assert (0 = year, 0 = price.. De er ikke angivet)
        }

        //---: MANUFACTURER
        [TestMethod]
        [DataRow("12")] //2 tegn GYLDIG
        public void TestValidManufacturer(string manufacturer)
        {
            wine.Manufacturer = manufacturer;
            wine.ValidateManufacturer();
        }

        [TestMethod]
        [DataRow("1")] //1 tegn UGYLDIG
        public void TestInvalidManufacturerTooShort(string manufacturer)
        {
            wine.Manufacturer = manufacturer;
            Assert.ThrowsException<ArgumentException>(() => wine.ValidateManufacturer());
        }

        [TestMethod]
        [DataRow(null)] // null UGYLDIG
        public void TestInvalidManufacturerNull(string manufacturer)
        {
            wine.Manufacturer = manufacturer;
            Assert.ThrowsException<ArgumentNullException>(() => wine.ValidateManufacturer());
        }

        //---: RATING
        [TestMethod]
        public void ValidateRating()
        {
            wine.ValidateRating();
            wineZeroRating.ValidateRating();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => wineTooLowRating.ValidateRating());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => wineTooHighRating.ValidateRating());
        } 
    }
}