using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum CharacterType
{
    Melee,
    Ranged,
    Support
}

public class BTAttackEnemy : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.RUNNING;
        CharacterBase character = bt.GetComponent<CharacterBase>();
        SOAtributes atributes = character.atributes;

        GameObject enemy = GetEnemyCloser(character);

        if (enemy)
        {
            character.charRigidbody.velocity = Vector3.zero;
            bt.transform.DOLookAt(enemy.transform.position, .1f);

            AnimationManager.Instance.SetTrigger(character.animator, "Attack");
            yield return new WaitForSeconds(atributes.attackSync);

            if (enemy)
            {
                character.Attack(enemy);
                status = Status.SUCCESS;
            }


        }
        else
        {
            status = Status.FAILURE;
        }
        yield return new WaitForSeconds(atributes.attackDelay);
        Print(bt.gameObject.name);
        character.currentState = State();

        yield break;

    }

    private GameObject GetEnemyCloser(CharacterBase character)
    {
        GameObject enemy = null;
        List<GameObject> enemies = SceneObjects.Instance.GetObjectsWithTag(character.atributes.enemyLabel);
        float distance = Mathf.Infinity;

        foreach (GameObject en in enemies)
        {
            if (character.gameObject == en) continue;

            if (GetEnemyWithBall(character.atributes, en, character))
            {
                return en;
            }

            if (Vector3.Distance(character.transform.position, en.transform.position) < distance)
            {
                enemy = en;
                distance = Vector3.Distance(character.transform.position, en.transform.position);
            }

        }

        return enemy;
    }

    private bool GetEnemyWithBall(SOAtributes atributes, GameObject enemy, CharacterBase character)
    {
        return atributes.characterType == CharacterType.Ranged && Manager.Instance.teamWithBall != null
        && Manager.Instance.teamWithBall != atributes.allyLabel && enemy.TryGetComponent(out CharacterBase enBase)
        && enBase.haveTheBall && Vector3.Distance(enemy.transform.position, character.transform.position)
        <= character.atributes.range;

    }
}
