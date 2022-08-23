using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "SO/Atributes")]
public class SOAtributes : ScriptableObject
{
    [Title("Targets")]
    public SearchTarget searchTarget;
    [Tooltip("Collectable, End, Attack")]
    public List<MoveTarget> targets = new List<MoveTarget>(3);
    public CharacterType characterType;
    public string enemyLabel;
    public string allyLabel;
    public float life = 5;
    
    [Title("Movement")]
    public float speed = 3;
    public float movementRange = 3;
    
    [Title("Attack")]
    public int range = 5;
    public float attackDelay = .5f;
    public float attackSync = .5f;
    public float projectileSpeed = 500;
    public GameObject attackPFB;
}
