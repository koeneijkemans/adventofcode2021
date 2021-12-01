string[] data = System.IO.File.ReadAllLines("input");
int[] depths = data.Select(d => int.Parse(d)).ToArray();

int countNumberOfDepthIncreases(int[] inputDepths) {
    return inputDepths.Select((currentDepth, index) => {
        if (inputDepths.Length > index + 1
            && currentDepth < inputDepths[index + 1])
                return 1;
        return 0;
    }).Sum();
}

int countNumberOfDepthIncreasesWithExtraSamples() {
    int[] depthsMap = depths.Select((currentDepth, index) => {
        if (depths.Length > index + 2)
            return currentDepth + depths[index + 1] + depths[index + 2];

        return 0;
    }).ToArray();

    return countNumberOfDepthIncreases(depthsMap);
}

Console.WriteLine(countNumberOfDepthIncreasesWithExtraSamples());