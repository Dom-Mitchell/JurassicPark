using System;

namespace JurassicPark
{
    class Dino
    {
        public string Name { get; set; }
        public string DinoType { get; set; }
        public string DietType { get; set; }
        public DateTime WhenAcquired { get; set; }
        public double Weight { get; set; }
        public int EnclosureNumber { get; set; }

        // public string FormattedDate()
        // {
        //     return WhenAcquired.ToString("dddd, dd MMMM yyyy");
        // }

        override public string ToString()
        {
            return $"\nName: {Name}\nDino Type: {DinoType}\nDiet: {DietType}\nDate Recieved: {WhenAcquired.ToString("dddd, MMMM dd, yyyy")}\nWeight: {Weight} pounds\nEnclosure: {EnclosureNumber}";
        }
    }
}
