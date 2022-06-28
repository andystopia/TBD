using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLayout
{
    public class RoundingUtils
    {
        public static float RoundToNearest(float alpha, IEnumerable<float> items)
        {
            var nearest = 0.0f;
            var distance = Mathf.Infinity;
            foreach (var item in items)
            {
                if (Math.Abs(item - alpha) < distance)
                {
                    nearest = item;
                    distance = Math.Abs(item - alpha);
                }
            }

            return nearest;
        }
    }
}