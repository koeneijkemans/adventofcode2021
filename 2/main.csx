string[] data = System.IO.File.ReadAllLines("input");

int x, y, aim = 0;

foreach (string line in data)
{
    string command = line.Split(' ')[0];
    int amount = int.Parse(line.Split(' ')[1]);

    switch (command)
    {
        case "forward":
            x += amount;
            y += (aim * amount);
        break;
        case "up":
            aim -= amount;
        break;
        case "down":
            aim += amount;
        break;
    }
}

Console.WriteLine(x * y)