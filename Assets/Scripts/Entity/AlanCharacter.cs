using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;


public class AlanCharacter : MonoBehaviour
{
    private Health health;
    [SerializeField]
    private EntityStats stats;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    public Health GetHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
