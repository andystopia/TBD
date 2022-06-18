using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLayout
{
    public class SnapBehavior : MonoBehaviour
    {
        [SerializeField] private GameGridLayout _layout;

        private float RoundToNearest(float alpha, IEnumerable<float> items)
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
        private void OnDrawGizmos()
        {
            // print(_layout.GetColumnCenters().Length);
            var currentPosition = transform.position;
            transform.position = new Vector3(RoundToNearest(currentPosition.x, _layout.GetColumnCenters()), 
                RoundToNearest(currentPosition.y, _layout.GetRowCenters()),
                currentPosition.z);
        }
        
        
    }
}
