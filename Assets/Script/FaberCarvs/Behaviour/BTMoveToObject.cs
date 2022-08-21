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
        Transform obj = null;

        GameObject[] objects = GameObject.FindGameObjectsWithTag(_target.ToString());
        float distance = Mathf.Infinity;

        foreach (GameObject ob in objects)
        {
            if (bt.gameObject == ob) continue;
            if (Condition(character, ob, distance))
            {
                obj = ob.transform;
                distance = Result(character, ob);
            }
        }
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
            characterBase.currentState = State(_target.ToString() + " - " + Vector3.Distance(character.position, obj.position));
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

    protected virtual bool Condition(Transform _this, GameObject target, float compare)
    {
        return Vector3.Distance(_this.position, target.transform.position) < compare;
    }
    protected virtual float Result(Transform _this, GameObject target)
    {
        return Vector3.Distance(_this.position, target.transform.position);
    }
}
