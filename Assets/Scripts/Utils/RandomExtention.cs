using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class RandomExtentions
    {
        static Random random = new Random();

        public static TValue Random<TValue>(this IEnumerable<TValue> list)
        {
            var count = list.Count();
            return list.Skip(random.Next(0, count)).First();
        }

        public static TValue Random<TValue>(this IEnumerable<ValueWithProbability<TValue>> values)
        {
            var sum = values.Sum(o => o.Probability);
            if (sum != 1)
                throw new InvalidOperationException("Sum of all probabilities should be 1.");

            var currentSpread = 0f;
            var spreatedValues = values.Select(o =>
            {
                currentSpread += currentSpread + o.Probability;
                return new ValueWithProbability<TValue>(currentSpread, o.Value);
            }).ToArray();

            currentSpread = 0;
            var value = random.NextDouble();
            foreach (var valueWithProbability in values)
            {
                currentSpread += valueWithProbability.Probability;
                if (currentSpread >= value)
                    return valueWithProbability.Value;
            }

            return values.Last().Value;
        }
    }

    public class ValueWithProbability<TValue>
    {
        public readonly float Probability;
        public readonly TValue Value;

        public ValueWithProbability(float probability, TValue value)
        {
            this.Probability = probability;
            this.Value = value;
        }
    }
}
