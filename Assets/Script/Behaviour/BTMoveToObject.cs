using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveTarget
{
    Ball,
    EndLine,
    Ally,
    Star,
    EndLine_2,
    Enemy
}
public class BTMoveToObject : BTNode
{
    private MoveTarget _target;
    private float _range;

    public BTMoveToObject(MoveTarget target, float range)
    {
        _target = target;
        _range = range;
    }

    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;

        Print();
        CharacterBase characterBase = bt.GetComponent<CharacterBase>();

        Transform character = bt.transform;

        Transform obj = GetObject(character);

        if (obj)
            AnimationManager.Instance.SetBool(characterBase.animator, "Run_", true);

        while (true)
        {
            if (!obj)
            {
                status = Status.FAILURE;
                break;
            }

            if (Vector3.Distance(character.position, obj.position) < _range) break;

            character.LookAt(obj);
            character.position = Vector3.MoveTowards(character.position, obj.position, characterBase.atributes.speed * Time.deltaTime);
            characterBase.animator.gameObject.transform.localRotation = Quaternion.identity;
            characterBase.currentState = State(_target.ToString() + ": " + obj.name + " - " + Vector3.Distance(character.position, obj.position));
            yield return null;
        }

        if (status == Status.RUNNING)
            status = Status.SUCCESS;

        AnimationManager.Instance.SetBool(characterBase.animator, "Run_", false);
        AnimationManager.Instance.SetTrigger(characterBase.animator, "Idle");

        Print(bt.gameObject.name + " : " + _target.ToString());
        bt.GetComponent<CharacterBase>().currentState = State(_target.ToString());
        yield break;

    }

    protected Transform GetObject(Transform character)
    {
        List<GameObject> objects = SceneObjects.Instance.GetObjectsWithTag(_target.ToString());
        if (objects == null) return null;
        
        Transform currentObject = null;
        float distance = Mathf.Infinity;
        CharacterBase characterBase = character.GetComponent<CharacterBase>();
        
        foreach (GameObject o in objects)
        {
            if (character.gameObject == o) continue;

            if (GetEnemyWithBall(characterBase.atributes, o, characterBase))
                return o.transform;

            if (Condition(character, o, distance))
            {
                currentObject = o.transform;
                distance = Result(character, o);
            }
        }

        return currentObject;
    }

    protected virtual bool Condition(Transform _this, GameObject target, float compare)
    {
        return Vector3.Distance(_this.position, target.transform.position) < compare;
    }
    protected virtual float Result(Transform _this, GameObject target)
    {
        return Vector3.Distance(_this.position, target.transform.position);
    }

    private bool GetEnemyWithBall(SOAtributes atributes, GameObject enemy, CharacterBase character)
    {
        return atributes.characterType == CharacterType.Ranged && Manager.Instance.teamWithBall != null
        && Manager.Instance.teamWithBall != atributes.allyLabel && enemy.TryGetComponent(out CharacterBase enBase)
        && enBase.haveTheBall;
    }
}
