using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "Entity Stats", menuName = "ScriptableObjects/Create Entity Stats", order = 1)]
public class EntityStats : ScriptableObject
{
    /*
     * Attack Type (Ranged, Melee)
            Fired projectiles vs instantaneous AOE attacks
Hit Type (Magical, Physical)
    Determines which kind of damage is delt to the enemy and what part of the enemy will be damaged. In other words, Magical Hits deal damage to the target’s Health equal to the Attack Damage minus the target’s Magical Armor (if the damage is less than 1 it equals 1). Same goes for Physical Hits and Physical Armor). 
Sense Range (int)
    Distance at which a character/enemy can sense an enemy
Attack Range (int)
    Distance in which a ranged character/enemy can fire at an enemy and the size of a melee character’s attack
Attack Speed (int)
    This number changes throughout the game. Cooldown time between character attacks (time in seconds is 1/#)
Attack Damage (int)
    How much health the enemies lose when hit
Attack Effect (Poison, Frozen, Possess, Bleeding, etc)
    Some characters will have special effects that affect the enemy for a time after each successful hit.
Health (int)
    This number changes throughout the game. When the character/enemy’s health is equal to or less than 0 it dies.
Physical Armor (int)
    The # damage taken away from incoming Physical Hits
Magical Armor (int)
    The # damage taken away from incoming Magical Hits
Name
    Their name
     */

    [SerializeField] private int senseRange;
    [SerializeField] private int attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int physicalArmor;
    [SerializeField] private int magicalArmor;

    
    // IDE GENERATED
    public int SenseRange => senseRange;

    public int AttackRange => attackRange;

    public int AttackDamage => attackDamage;

    public int AttackSpeed => attackSpeed;
}
