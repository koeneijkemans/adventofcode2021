string[] data = System.IO.File.ReadAllLines("input");

var stringToNumber = (string input) => {
    return input.ToCharArray().Select(c => (int)c).Sum();
};

var orderString = (string input) => {
    return string.Join("", input.ToCharArray().OrderBy(c => c));
};

int numberOfOccurances = data.Select(line => {
    string[] splitLine = line.Split('|');
    string[] output = splitLine[1].Split(' ');
    List<string> digits = splitLine[0].Split(' ').ToList();

    List<int> knownDigits = digits.Where(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7)
        .Select(d => stringToNumber(d))
        .ToList();

    return output.Count(o => knownDigits.Contains(stringToNumber(o)));
}).Sum();

// Console.WriteLine(numberOfOccurances);

int number = data.Select(line => {
    string[] splitLine = line.Split('|');
    string[] encodedDigits = splitLine[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    string[] output = splitLine[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    Dictionary<int, string> map = new Dictionary<int, string>();

    map.Add(1, orderString(encodedDigits.Single(d => d.Length == 2)));
    map.Add(4, orderString(encodedDigits.Single(d => d.Length == 4)));
    map.Add(7, orderString(encodedDigits.Single(d => d.Length == 3)));    
    map.Add(8, orderString(encodedDigits.Single(d => d.Length == 7)));

    // The 6 is 6 chars long and the 1 should not fit in it.
    map.Add(6, orderString(encodedDigits.Single(d => d.Length == 6 && map[1].ToCharArray().Any(c => !d.ToCharArray().Contains(c)))));

    // The 3 is 5 chars long and the 1 should completly fit in it.
    map.Add(3, orderString(encodedDigits.Single(d => d.Length == 5 && map[1].ToCharArray().All(c => d.ToCharArray().Contains(c)))));

    // The 9 is 6 chars long and the 3 should completly fit in
    map.Add(9, orderString(encodedDigits.Single(d => d.Length == 6 && map[3].ToCharArray().All(c => d.ToCharArray().Contains(c)))));

    // The 0 is 6 chars long and is the last of 6 chars to be mapped
    map.Add(0, orderString(encodedDigits.Single(d => d.Length == 6 && orderString(d) != map[6] && orderString(d) != map[9])));

    // The 5 is 5 chars long and should completly fit a the digits of 4 minus the 7.
    map.Add(5, orderString(encodedDigits.Single(d => d.Length == 5 && map[4].ToCharArray().Except(map[7].ToCharArray()).All(c => d.ToCharArray().Contains(c)))));

    // The last one that isnt mapped yet.
    map.Add(2, orderString(encodedDigits.Single(d => d.Length == 5 && orderString(d) != map[3] && orderString(d) != map[5])));

    return output.Select((o, i) =>
    {
        var pair = map.Single(pair => pair.Value == orderString(o));

        if (i == 0)
            return pair.Key * 1000;
        else if (i == 1)
            return pair.Key * 100;
        else if (i == 2)
            return pair.Key * 10;

        return pair.Key;
    })
    .Sum();
}).Sum();

Console.WriteLine(number);
