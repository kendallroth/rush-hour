using System;
using System.Collections.Generic;
using System.Linq;

// Taken from: https://stackoverflow.com/a/11930875

public static class IEnumerableExtensions
{
    #region Custom Methods
    /// <summary>
    /// Get a random element from a list
    /// </summary>
    /// <typeparam name="T">List item type</typeparam>
    /// <param name="sequence">List reference</param>
    /// <param name="seedRandomizer">Reproducible randomizer for selecting weights</param>
    /// <returns>Random list element</returns>
    public static T Random<T>(this IEnumerable<T> sequence, Random seedRandomizer = null)
    {
        int randomIndex = seedRandomizer != null ? seedRandomizer.Next(0, sequence.Count()) : new Random().Next(0, sequence.Count());

        return sequence.ElementAt(randomIndex);
    }

    /// <summary>
    /// Get a random element by weight
    /// </summary>
    /// <typeparam name="T">List item type</typeparam>
    /// <param name="sequence">List reference</param>
    /// <param name="weightSelector">Selector for list member weight</param>
    /// <param name="seedRandomizer">Reproducible randomizer for selecting weights</param>
    /// <returns>Random weight-selected list item</returns>
    public static T RandomByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector, Random seedRandomizer = null)
    {
        // Random selection is calculated by summing all weights, then multiplying by a random percentage to get a "total weight percent."
        //   The random item is selected by finding the first aggregated item to exceed the randomized "total weight percentage."
        float totalWeight = sequence.Sum(weightSelector);
        float itemWeightIndex = seedRandomizer != null ? (float)seedRandomizer.NextDouble() * totalWeight : (float)new Random().NextDouble() * totalWeight;
        float currentWeightIndex = 0;

        foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
        {
            currentWeightIndex += item.Weight;

            // The first aggregated item to exceed the randomized "total weight percent" is the randomly selected value
            if (currentWeightIndex >= itemWeightIndex)
            {
                return item.Value;
            }
        }

        return default;
    }
    #endregion
}

