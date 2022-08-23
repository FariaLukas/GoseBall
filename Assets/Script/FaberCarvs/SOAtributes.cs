using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Atributes")]
public class SOAtributes : ScriptableObject
{
   public SearchTarget searchTarget;
    [Tooltip("Collectable, End, Attack")]
    public List<MoveTarget> targets = new List<MoveTarget>(3);
    public CharacterType characterType;
    public string enemyLabel;
    public string allyLabel;
    public int range = 5;
    public float speed = 3;
    public float attackDelay = .5f;
    public float attackSync = .5f;
    public float life = 5;
    public GameObject attackPFB;
    public float projectileSpeed = 500;
}
