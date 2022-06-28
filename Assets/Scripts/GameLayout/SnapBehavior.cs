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

        private void OnDrawGizmos()
        {
            // print(_layout.GetColumnCenters().Length);
            var currentPosition = transform.position;
            transform.position = new Vector3(RoundingUtils.RoundToNearest(currentPosition.x, _layout.GetColumnCenters()), RoundingUtils.RoundToNearest(currentPosition.y, _layout.GetRowCenters()),
                currentPosition.z);
        }
        
        
    }
}
