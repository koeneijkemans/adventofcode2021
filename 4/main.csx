using System.Linq;

string[] data = System.IO.File.ReadAllLines("input");

int[] bingoNumbers = data[0].Split(',').Select(d => int.Parse(d)).ToArray();

List<BingoCard> bingoCards = new List<BingoCard>();

List<BingoCard> winningCards = new List<BingoCard>();

for (int i = 1; i < data.Length; i++)
{
    if (data[i].Length > 2)
    {
        bingoCards[bingoCards.Count - 1].AddLine(data[i]);
    }
    else
    {
        bingoCards.Add(new BingoCard(bingoCards.Count));
    }
}

foreach (int number in bingoNumbers)
{
    foreach (var bingoCard in bingoCards)
    {
        if (!winningCards.Any(c => c.Id == bingoCard.Id))
        {
            bingoCard.Check(number);

            if (bingoCard.DidWin())
            {
                Console.WriteLine($"We have a winner!");
                Console.WriteLine(bingoCard.ToString());
                Console.WriteLine($"The winning number is {number * bingoCard.SumOfUncheckNumbers()}");

                winningCards.Add(bingoCard);
            }

            if (winningCards.Count == bingoCards.Count)
            {
                Console.WriteLine($"This was the last winner!");
                return;
            }
        }
    }
}


class BingoCard
{
    public int Id { get; set; }

    public List<List<int>> Numbers { get; set; }

    public List<List<int>> CheckedNumbers { get; set; }

    public BingoCard(int id)
    {
        Id = id;
        Numbers = new List<List<int>>();
        CheckedNumbers = new List<List<int>>();
    }

    public void AddLine(string line)
    {
        List<int> numbers = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => int.Parse(l))
            .ToList();

        Numbers.Add(numbers);
        CheckedNumbers.Add(numbers.Select(n => -1).ToList());
    }

    public void Check(int number)
    {
        for (int row = 0; row < Numbers.Count; row++)
        {
            for (int col = 0; col < Numbers[row].Count; col++)
            {
                if (Numbers[row][col] == number)
                {
                    CheckedNumbers[row][col] = number;
                    return;
                }
            }
        }
    }

    public bool DidWin()
    {
        for (int row = 0; row < CheckedNumbers.Count; row++)
        {
            int sumOfNumbers = Numbers.Sum(n => n[row]);
            int sumOfCheckedNumbers = CheckedNumbers.Sum(n => n[row]);

            int sumOfColNumbers = Numbers[row].Sum(n => n);
            int sumOfColCheckedNumbers = CheckedNumbers[row].Sum(n => n);

            if (sumOfNumbers == sumOfCheckedNumbers || sumOfColNumbers == sumOfColCheckedNumbers)
                return true;
        }

        return false;
    }

    public int SumOfUncheckNumbers()
    {
        List<int> flatNumbers = Numbers.SelectMany(n => n).ToList();
        List<int> flatCheckedNumbers = CheckedNumbers.SelectMany(n => n).ToList();

        return flatNumbers.Where(n => !flatCheckedNumbers.Contains(n)).Sum();
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append($"Card number: {Id}\n");
        builder.Append($"\nNumbers: \n");
        builder.Append(string.Join("\n", Numbers.Select(row => string.Join(" ", row))));
        builder.Append($"\nChecked: \n");
        builder.Append(string.Join("\n", CheckedNumbers.Select(row => string.Join(" ", row.Select(n => n >= 0 ? n.ToString() : "-")))));

        return builder.ToString();
    }
}