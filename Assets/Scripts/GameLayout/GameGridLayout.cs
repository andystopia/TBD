using UnityEngine;

namespace GameLayout
{
    [System.Serializable]
    public struct GameGridDimensions
    {
        // some bikeshedding could be done to argue
        // that this behavior is not intrinsically value-based
        // so using public fields is an anti-pattern. 
        // but I would be shocked if this is case where 
        // demons arise.
        [Range(1, 25)]
        public int rows;
        [Range(1, 25)]
        public int cols;

        public GameGridDimensions(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }
    }

    public class GameGridLayout : MonoBehaviour
    {
        [SerializeField]
        private Vector2 lowerLeft;
        [SerializeField]
        private Vector2 upperRight;

        [SerializeField]
        private GameGridDimensions dimensions;

        public GameGridLayout(Vector2 lowerLeft, Vector2 upperRight, GameGridDimensions dimensions)
        {
            this.lowerLeft = lowerLeft;
            this.upperRight = upperRight;
            this.dimensions = dimensions;
        }

        /// <summary>
        /// Essentially, given a range of values, and a number describing
        /// how many time that range is subdivided, will return the midpoints of
        /// all those subdivided intervals
        /// </summary>
        /// <param name="a"> the lower value of the interval </param>
        /// <param name="b"> the upper value of the interval </param>
        /// <param name="numberOfIntervals"> the number of intervals</param>
        /// <returns></returns>
        private static float[] CalculateRegularDecompositionIntervalMidpoints(float a, float b, int numberOfIntervals)
        {
            float range = b - a;
            float intervalWidths = range / numberOfIntervals;
            float[] centers = new float[numberOfIntervals];

            for (int i = 0; i < numberOfIntervals; i++)
            {
                centers[i] = i * intervalWidths + intervalWidths / 2 + a;
            }

            return centers;
        }

        public float GetCellWidth()
        {
            return (upperRight.x - lowerLeft.x) / dimensions.cols;
        }
        
        public float GetCellHeight()
        {
            return (upperRight.y - lowerLeft.y) / dimensions.rows;
        }


        public float[] GetColumnCenters()
        {
            return CalculateRegularDecompositionIntervalMidpoints(lowerLeft.x, upperRight.x, dimensions.cols);
        }

        public float[] GetRowCenters()
        {
            return CalculateRegularDecompositionIntervalMidpoints(lowerLeft.y, upperRight.y, dimensions.rows);
        }
    }
}