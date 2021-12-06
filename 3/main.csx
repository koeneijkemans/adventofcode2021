string[] data = System.IO.File.ReadAllLines("input");

Func<int,  string[], int> MostCommonBitForPosition = (int index, string[] input) =>
    input.Select(d => d[index] == '0' ? true : false).Count(n => n) > input.Select(d => d[index] == '1' ? true : false).Count(n => n) ? 0 : 1;
Func<int,  string[], int> LeastCommonBitForPosition = (int index, string[] input) =>
    input.Select(d => d[index] == '0' ? true : false).Count(n => n) <= input.Select(d => d[index] == '1' ? true : false).Count(n => n) ? 0 : 1;

Func<(long MostCommon, long LeastCommon)> PartTwo = () => {
    string[] remainingMostCommonData = data;
    string[] remainingLeastCommonData = data;

    for (int i = 0; i < data[0].Length; i++)
    {
        if (remainingMostCommonData.Length > 1)
        {
            int mostCommonBit = MostCommonBitForPosition(i, remainingMostCommonData);

            remainingMostCommonData = remainingMostCommonData.Where(d => d[i].ToString() == mostCommonBit.ToString()).ToArray();
        }

        if (remainingLeastCommonData.Length > 1)
        {
            int leastCommonBit = LeastCommonBitForPosition(i, remainingLeastCommonData);

            remainingLeastCommonData = remainingLeastCommonData.Where(d => d[i].ToString() == leastCommonBit.ToString()).ToArray();
        }
    }

    return (Convert.ToInt64(remainingMostCommonData.First(), 2), Convert.ToInt64(remainingLeastCommonData.First(), 2));
};

Func<(long MostCommon, long LeastCommon)> PartOne = () => {
    int[] counts = new int[data[0].Length];

    for (int i = 0; i < counts.Length; i++)
    {
        counts[i] = MostCommonBitForPosition(i, data);
    }

    return (Convert.ToInt64(string.Join("", counts), 2), Convert.ToInt64(string.Join("", counts.Select(c => c == 0 ? 1 : 0)), 2));
};

(long MostCommon, long LeastCommon) = PartOne();
Console.WriteLine(MostCommon * LeastCommon);

(long oxygen, long co2) = PartTwo();
Console.WriteLine(oxygen * co2);
