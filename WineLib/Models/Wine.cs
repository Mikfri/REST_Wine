namespace WineLib.Models
{
    public class Wine
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public double Rating { get; set; }


        public Wine(int id, string manufacturer, int year, int price, double rating)
        {
            Id = id;
            Manufacturer = manufacturer;
            Year = year;
            Price = price;
            Rating = rating;
        }

        public Wine() { }

        public void ValidateManufacturer()
        {
            if (Manufacturer == null)
            {
                throw new ArgumentNullException("Manufacturer is null");
            }

            if (Manufacturer.Length < 2)
            {
                throw new ArgumentException($"Manufacturer must MIN be 2 char. {Manufacturer} is {Manufacturer.Length} character(s) long!");
            }
        }

        public void ValidatePrice()
        {
            if (Price < 0)  // den må gerne være 0. Nul er hverken neg eller pos
                throw new ArgumentOutOfRangeException($"Price may not be negative!");
        }

        public void ValidateRating()
        {            
            if ( (Rating < 2 || Rating > 5) && Rating != 0 )
                throw new ArgumentOutOfRangeException($"Rating has to be between 2 or 5!");
        }

        public void Validate()
        {
            ValidateManufacturer();
            ValidatePrice();
            ValidateRating();
        }
        public override string ToString()
        {
            return $"{Id} {Manufacturer} {Year} {Price} {Rating}";
        }

        public override bool Equals(object? obj)    // Dens sammenlagte attributter sammenlignes..
        {
            return obj is Wine wine &&
                Id == wine.Id &&
                Manufacturer == wine.Manufacturer &&
                Year == wine.Year &&
                Price == wine.Price &&
                Rating == wine.Rating;
        }

        public override int GetHashCode()   // Dens fysiske adresse lægges sammen i memoryen.. Så to ens objekter ikke lagres 2 steder i memory
        {
            return HashCode.Combine(Id, Manufacturer, Year, Price, Rating);
        }
    }
}
