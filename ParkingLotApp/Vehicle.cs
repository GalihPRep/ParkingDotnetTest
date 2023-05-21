namespace ParkingLotApp
{
    internal class Vehicle
    {
        public Vehicle(string? number, string? type, string? color)
        {
            Number = number;
            Type = type;
            Color = color;
        }

        public string? Number { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
    }
}
