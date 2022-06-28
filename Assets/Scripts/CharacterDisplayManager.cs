using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CharacterDisplayManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly CardinalDirectionMap<int> animatorHashes = new(
        Animator.StringToHash("Walk_North"),
        Animator.StringToHash("Walk_South"),
        Animator.StringToHash("Walk_East"),
        Animator.StringToHash("Walk_West")
    );

    
    public void ChangeDirection(CardinalDirection direction)
    {
        animator.SetTrigger(animatorHashes[direction]);
    }
}
