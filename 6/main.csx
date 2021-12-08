string[] data = System.IO.File.ReadAllLines("input");

Dictionary<int, long> fishesPerDay = data[0].Split(',').GroupBy(fish => int.Parse(fish)).ToDictionary(fish => fish.Key, fish => (long)fish.Count());

int totalOfDays = 256;

for (int i = 0; i < totalOfDays; i++)
{
    // Get the number of new fishes
    fishesPerDay.TryGetValue(0, out long newBreed);

    // Create a new dictionary, skipping the fishes that are new and have bread today
    fishesPerDay = fishesPerDay.Where(fish => fish.Key > 0).ToDictionary(fish => fish.Key - 1, fish => fish.Value);

    // Reset the fish that bread today
    if (fishesPerDay.ContainsKey(6))
        fishesPerDay[6] += newBreed;
    else
        fishesPerDay.Add(6, newBreed);

    // Add the new fish
    if (fishesPerDay.ContainsKey(8))
        fishesPerDay[8] += newBreed;
    else
        fishesPerDay.Add(8, newBreed);
}

Console.WriteLine($"Number of fishes after {totalOfDays} days is {fishesPerDay.Sum(f => f.Value)}");