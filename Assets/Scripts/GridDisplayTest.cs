using System.Collections.Generic;
using GameLayout;
using UnityEngine;

namespace DefaultNamespace
{
    public class GridDisplayTest : MonoBehaviour
    {
        [SerializeField] private GameGridLayout layout;

        [SerializeField] private Square square;
        private List<Square> squares = new();
        
        public void Update()
        {
            foreach (var square1 in squares)
            {
                Destroy(square1.gameObject);
            }

            squares.Clear();
            
            foreach (var rowCenter in layout.GetRowCenters())
            {
                foreach (var columnCenter in layout.GetColumnCenters())
                {
                    squares.Add(Instantiate(square, new Vector3(columnCenter, rowCenter), Quaternion.identity));
                }
            }
        }
    }
}