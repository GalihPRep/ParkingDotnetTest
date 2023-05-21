using ConsoleTables;
using ParkingLotApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Menu();
    }

    private static void Menu()
    {
        var input = Console.ReadLine();
        if (input.Contains("create_parking_lot ")) CreateSlot(input.Remove(0, 19));
        else if (input.Contains("park ")) CheckIn(input.Remove(0, 5));
        else if (input.Contains("leave ")) CheckOut(input.Remove(0, 6));
        else if (input.Equals("status")) Status();
        else if (input.Equals("report")) Report();
        else if (input.Equals("odd_plate_of_vehicles")) CountByOddPlateNumber();
        else if (input.Equals("even_plate_of_vehicles")) CountByOddPlateNumber(false);
        else if (input.Contains("type_of_vehicles ")) CountByType(input.Remove(0, 17));
        else if (input.Contains("colour_of_vehicles ")) CountByColor(input.Remove(0, 19));
        else if (input.Equals("registration_numbers_for_vehicles_with_odd_plate")) PlateByOddPlateNumber();
        else if (input.Equals("registration_numbers_for_vehicles_with_even_plate")) PlateByOddPlateNumber(false);
        else if (input.Contains("registration_numbers_for_vehicles_with_colour ")) PlateByColor(input.Remove(0, 46));
        else if (input.Contains("slot_numbers_for_vehicles_with_colour ")) PlateByColor(input.Remove(0, 38), false);
        else if (input.Contains("slot_number_for_registration_number ")) SlotByPlateNumber(input.Remove(0, 36));
        else if (input.Equals("exit")) Environment.Exit(0);
        else Help();
    }

    //private static List<Vehicle?> _spaces = new List<Vehicle?> { null, null, null, null, null, null};
    //private static List<Vehicle?> _spaces = new List<Vehicle?>
    //{
    //    new Vehicle("B-1234-XYZ","Mobil","Putih"),
    //    new Vehicle("B-9999-XYZ","Motor","Putih"),
    //    new Vehicle("D-0001-HIJ","Mobil","Hitam"),
    //    new Vehicle("B-7777-DEF","Mobil","Red"),
    //    new Vehicle("B-2701-XXX","Mobil","Biru"),
    //    new Vehicle("B-3141-ZZZ","Motor","Hitam")
    //};
    private static List<Vehicle?> _spaces = new List<Vehicle?>
    {
        new Vehicle("B-1234-XYZ","Mobil","Putih"),
        new Vehicle("B-9999-XYZ","Motor","Putih"),
        new Vehicle("D-0001-HIJ","Mobil","Hitam"),
        new Vehicle("B-333-SSS","Mobil","Putih"),
        new Vehicle("B-2701-XXX","Mobil","Biru"),
        new Vehicle("B-3141-ZZZ","Motor","Hitam")
    };

    private static void CreateSlot(string inputIn)
    {
        try
        {
            int inputAsInt = int.Parse(inputIn);
            _spaces = Enumerable.Repeat<Vehicle?>(null, inputAsInt).ToList();
        }
        catch { Console.WriteLine("Invalid input!"); }
        Console.WriteLine("");
        Menu();
    }
    private static void CheckIn(string inputIn)
    {
        try
        {
            var inputSplitted = inputIn?.Split(" ");
            Vehicle vehicle = new Vehicle(inputSplitted?[0], inputSplitted?[2], inputSplitted?[1]);

            for (int i = 0; i < _spaces.Count; i++)
            {
                if (_spaces[i] == null) { _spaces[i] = vehicle; break; }
            }
            if (_spaces.IndexOf(vehicle) != -1) Console.WriteLine("Allocated slot number: " + (_spaces.IndexOf(vehicle) + 1));
            else Console.WriteLine("Sorry, parking lot is full.");
        }
        catch (Exception) { Console.WriteLine("Invalid input!"); }
        Console.WriteLine("");
        Menu();
    }
    private static void CheckOut(string inputIn)
    {
        try
        {
            int inputAsInt = int.Parse(inputIn);
            try
            {
                _spaces[inputAsInt - 1] = null;
                Console.WriteLine($"Slot number {inputAsInt} is free.");
            }
            catch { Console.WriteLine("Slot does not exist!"); }
        }
        catch { Console.WriteLine("Invalid input!"); }
        Console.WriteLine("");
        Menu();
    }

    private static void Status()
    {
        var table = new ConsoleTable("Slot", "Number", "Type", "Colour");
        foreach (var item in _spaces)
        {
            if (item != null)
            {
                table.AddRow(
                _spaces.IndexOf(item) + 1,
                item?.Number,
                item?.Type,
                item?.Color
            );
            }
        }
        Console.WriteLine(table.ToString());
        Console.WriteLine("");
        Menu();
    }
    private static void Report()
    {
        var available = 0;
        _spaces.ForEach(item => { if (item == null) available++; });
        int full = _spaces.Count - available;
        Console.WriteLine($"{full} full\n{available} available");
        Console.WriteLine("");
        Menu();
    }
    private static void CountByOddPlateNumber(bool odd = true)
    {
        int counter = 0;
        if (odd)
        {
            foreach (var item in _spaces)
            {
                if (item != null)
                {
                    string number = item.Number.Substring(2, 4);
                    if (int.TryParse(number, out _)) { if (int.Parse(number) % 2 != 0) counter++; }
                };
            }
        }
        else
        {
            foreach (var item in _spaces)
            {
                string number = item.Number.Substring(2, 4);
                if (int.TryParse(number, out _)) { if (int.Parse(number) % 2 == 0) counter++; }
            }
        }
        Console.WriteLine(counter);
        Console.WriteLine("");
        Menu();
    }

    private static void CountByType(string inputIn)
    {
        var counter = 0;
        _spaces.ForEach(item =>
        {
            if (string.Equals(item?.Type, inputIn, StringComparison.OrdinalIgnoreCase)) counter++;
        });
        Console.WriteLine(counter.ToString());
        Console.WriteLine("");
        Menu();
    }
    private static void CountByColor(string inputIn)
    {
        var counter = 0;
        _spaces.ForEach(item =>
        {
            if (string.Equals(item?.Color, inputIn, StringComparison.OrdinalIgnoreCase)) counter++;
        });
        Console.WriteLine(counter.ToString());
        Console.WriteLine("");
        Menu();
    }

    private static void PlateByOddPlateNumber(bool odd = true)
    {
        string vehicles = "";
        if (odd)
        {
            foreach (var item in _spaces)
            {
                if (item != null)
                {
                    string number = item.Number.Substring(2, 4);
                    if (int.TryParse(number, out _)) { if (int.Parse(number) % 2 != 0) vehicles += $"{item.Number}, "; }
                }
            }
        }
        else
        {
            foreach (var item in _spaces)
            {
                if (item != null)
                {
                    string number = item.Number.Substring(2, 4);
                    if (int.TryParse(number, out _)) { if (int.Parse(number) % 2 == 0) vehicles += $"{item.Number}, "; }
                }
            }
        }
        Console.WriteLine(vehicles.Substring(0, vehicles.Length - 2));
        Console.WriteLine("");
        Menu();
    }
    private static void PlateByColor(string inputIn, bool numberOfPlate = true)
    {
        string vehicles = "";
        _spaces.ForEach(item =>
        {
            if (item != null)
            {
                if (string.Equals(item?.Color, inputIn, StringComparison.OrdinalIgnoreCase))
                {
                    if (numberOfPlate) vehicles += $"{item?.Number}, ";
                    else vehicles += $"{_spaces.IndexOf(item) + 1}, ";
                }
            }
        });
        Console.WriteLine(vehicles.Substring(0, vehicles.Length - 2));
        Console.WriteLine("");
        Menu();
    }
    private static void SlotByPlateNumber(string inputIn)
    {
        var slot = 0;
        _spaces.ForEach(item =>
        {
            if (item?.Number == inputIn) slot = _spaces.IndexOf(item) + 1;
        });
        if (slot != 0) Console.WriteLine(slot.ToString());
        else Console.WriteLine("Not found");
        Console.WriteLine("");
        Menu();
    }
    private static void Help()
    {
        Console.WriteLine("\nChoose the commands:");
        Console.WriteLine("    opening a new parking lot                    create_parking_lot [number]");
        Console.WriteLine("    check-in for vehicles                        park [plate] [colour] [type]");
        Console.WriteLine("    check-out for vehicles                       leave [slot]");
        Console.WriteLine("    report in the form of table                  status");
        Console.WriteLine("    report on the slot availability              report");
        Console.WriteLine("    counting vehicles by odd plate number        odd_plate_of_vehicles");
        Console.WriteLine("    counting vehicles by even plate number       even_plate_of_vehicles");
        Console.WriteLine("    counting vehicles by type                    type_of_vehicles [type]");
        Console.WriteLine("    counting vehicles by colour                  colour_of_vehicles [colour]");
        Console.WriteLine("    finding vehicles by odd plate number         registration_numbers_for_vehicles_with_odd_plate");
        Console.WriteLine("    finding vehicles by even plate number        registration_numbers_for_vehicles_with_even_plate");
        Console.WriteLine("    finding vehicle plate numbers by color       registration_numbers_for_vehicles_with_colour [colour]");
        Console.WriteLine("    finding vehicle slot numbers by color        slot_numbers_for_vehicles_with_colour [colour]");
        Console.WriteLine("    finding slot by vehicle plate number         slot_number_for_registration_number [plate]");
        Console.WriteLine("    quitting the application                     exit");
        Console.WriteLine("");
        Menu();
    }
}