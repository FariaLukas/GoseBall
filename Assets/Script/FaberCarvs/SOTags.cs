using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tags")]
public class SOTags : ScriptableObject
{
    public SearchTarget searchTarget;
    [Tooltip("Collectable, End, Attack")]
    public List<MoveTarget> targets = new List<MoveTarget>(3);
    public CharacterType characterType;
    public string enemyLabel;
    public string allyLabel;
}
