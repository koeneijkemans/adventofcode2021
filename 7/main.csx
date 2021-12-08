string[] data = System.IO.File.ReadAllLines("input");

int[] crabs = data[0].Split(',').Select(c => int.Parse(c)).OrderBy(c => c).ToArray();

var fuelToPosition = (int targetPosition, int currentPosition) => {
    if (targetPosition > currentPosition)
        return targetPosition - currentPosition;
    else
        return currentPosition - targetPosition;
};

var advancedFuelToPosition = (int targetPosition, int currentPosition) => {
    int numberOfSteps = 0;

    if (targetPosition > currentPosition)
        numberOfSteps = targetPosition - currentPosition;
    else
        numberOfSteps = currentPosition - targetPosition;

    int costToPosition = new int[numberOfSteps].Select((v, i) => i + 1).Sum();

    return costToPosition;
};

int[] fuelCosts = new int[crabs[crabs.Length - 1]];

for (int i = crabs[0]; i < crabs[crabs.Length - 1]; i++)
{
    fuelCosts[i] = crabs.Select(c => advancedFuelToPosition(i, c)).Sum();
}

Console.WriteLine($"Cheapest move will cost {fuelCosts.OrderBy(c => c).First()}");