string[] data = System.IO.File.ReadAllLines("input");

List<CoordinateSet> coordinates = new List<CoordinateSet>();

List<Coordinate> allCoordinates = new List<Coordinate>();

foreach (string line in data)
{
    string[] coordinatesLine = line.Split(' ');

    int[] firstCoordinates = coordinatesLine[0].Split(',').Select(c => int.Parse(c)).ToArray();
    int[] secondCoordinates = coordinatesLine[2].Split(',').Select(c => int.Parse(c)).ToArray();
    
    coordinates.Add(new CoordinateSet(
        new Coordinate { X = firstCoordinates[0], Y = firstCoordinates[1] },
        new Coordinate { X = secondCoordinates[0], Y = secondCoordinates[1] }
    ));
}

foreach (var set in coordinates)
{
    Console.WriteLine($"Gatering coordinates for {set.ToString()}");
    List<Coordinate> coordinatesForSet = new List<Coordinate>();

    if (set.First.X == set.Second.X || set.First.Y == set.Second.Y)
    {
        if (set.First.X != set.Second.X)
        {
            int start = set.First.X;
            int end = set.Second.X;

            for (int i = start; i <= end; i++)
            {
                coordinatesForSet.Add(new Coordinate{ X = i, Y = set.First.Y });
            }
        }
        
        if (set.First.Y != set.Second.Y)
        {
            int start = set.First.Y < set.Second.Y ? set.First.Y : set.Second.Y ;
            int end = set.First.Y > set.Second.Y ? set.First.Y : set.Second.Y;

            for (int i = start; i <= end; i++)
            {
                coordinatesForSet.Add(new Coordinate{ X = set.First.X, Y = i });
            }
        }
    }
    else
    {
        // Gather the diagonal occurances.
        int totalX = (set.First.X + set.Second.X);
        int totalY = (set.First.Y + set.Second.Y);
    
        int yDirection = set.First.Y < set.Second.Y ? 1 : -1;

        for (int i = 0; i <= (set.Second.X - set.First.X); i++)
        {
            coordinatesForSet.Add(
                new Coordinate
                {
                    X = set.First.X + i,
                    Y = set.First.Y + (i * yDirection)
                });
        }
    }

    Console.WriteLine($"Found the following coordinates: {string.Join(" ", coordinatesForSet.Select(c => c.ToString()))}");
    allCoordinates.AddRange(coordinatesForSet);
}

int numberOfOverlappingCoordinates = allCoordinates.GroupBy(c => c.ToString())
    .Select(group => group.Count())
    .Where(count => count > 1)
    .Count();

//Console.WriteLine(string.Join("\n", allCoordinates.OrderBy(c => c.Y).ThenBy(c => c.X).Select(c => c.ToString())));

Console.WriteLine(numberOfOverlappingCoordinates);

class CoordinateSet
{
    public CoordinateSet(Coordinate a, Coordinate b)
    {
        if (a.X <= b.X)
        {
            First = a;
            Second = b;
        }
        else
        {
            First = b;
            Second = a;
        }
    }

    public Coordinate First {get;}

    public Coordinate Second {get;}

    public override string ToString()
    {
        return $"First: {First.ToString()}, Second: {Second.ToString()}";
    }
}

class Coordinate
{
    public int X {get;set;}

    public int Y {get;set;}

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}