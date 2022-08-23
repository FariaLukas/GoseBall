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

        GameObject enemy = null;
        List<GameObject> enemies = SceneObjects.Instance.GetObjectsWithTag(atributes.enemyLabel);
        float distance = Mathf.Infinity;

        foreach (GameObject en in enemies)
        {
            if (bt.gameObject == en) continue;

            if (GetEnemyWithBall(atributes, en, character))
            {
                enemy = en;
                break;
            }

            if (Vector3.Distance(bt.transform.position, en.transform.position) < distance)
            {
                enemy = en;
                distance = Vector3.Distance(bt.transform.position, en.transform.position);
            }

        }

        if (enemy)
        {
            character.charRigidbody.velocity = Vector3.zero;
            bt.transform.DOLookAt(enemy.transform.position, .1f);
            GameObject prefab = atributes.attackPFB;
            float speed = atributes.projectileSpeed;

            AnimationManager.Instance.SetTrigger(character.animator, "Attack");
            yield return new WaitForSeconds(atributes.attackSync);
            
            if (enemy)
            {
                Vector3 position = bt.transform.position + bt.transform.forward;
                bt.transform.DOLookAt(enemy.transform.position, .1f);

                GameObject projectile = GameObject.Instantiate(prefab, position, bt.transform.rotation);

                GameObject.Destroy(projectile, 5);

                if (atributes.characterType == CharacterType.Ranged)
                {
                    projectile.GetComponent<Rigidbody>().AddForce(bt.transform.forward * speed);
                }
                else
                {
                    enemy.GetComponent<Health>().TakeDamage(1);

                }

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

    private bool GetEnemyWithBall(SOAtributes atributes, GameObject enemy, CharacterBase character)
    {
        return atributes.characterType == CharacterType.Ranged && Manager.Instance.teamWithBall != null
        && Manager.Instance.teamWithBall != atributes.allyLabel && enemy.TryGetComponent(out CharacterBase enBase)
        && enBase.haveTheBall && Vector3.Distance(enemy.transform.position, character.transform.position)
        <= character.atributes.range;

    }
}
