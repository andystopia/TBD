using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayManager : MonoBehaviour
{
    public enum CardinalDirections
    {
        North,
        South,
        West,
        East
    }

    [SerializeField] private Animator animator;

    public void ChangeDirections(CardinalDirections direction)
    {
        switch (direction)
        {
            case CardinalDirections.North:
                animator.SetTrigger("Walk_North");
                break;
            case CardinalDirections.South:
                animator.SetTrigger("Walk_South");
                break;
            case CardinalDirections.East:
                animator.SetTrigger("Walk_East");
                break;
            case CardinalDirections.West:
                animator.SetTrigger("Walk_West");
                break;
            default:
                Debug.LogWarning("Invalid Cardinal Direction Enum");
                break;
        }
    }


}
