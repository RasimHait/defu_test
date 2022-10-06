    using System.Collections.Generic;
    using UnityEngine;

    public static class ListExtension
    {
        public static List<T> Randomize<T>(this IEnumerable<T> initial, int seed)
        {
            var result = new List<T>(initial);
            Random.InitState(seed);

            for (var i = 0; i < result.Count; i++)
            {
                var from = Random.Range(0, result.Count);
                var to   = Random.Range(0, result.Count);

                var elementA = result[from];
                var elementB = result[to];

                result[from] = elementB;
                result[to]   = elementA;
            }

            return result;
        }
    }
