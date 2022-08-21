using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCollectBall : BTNode
{
    public override IEnumerator Run(BehaviorTree bt)
    {
        status = Status.FAILURE;
        CharacterBase characterBase = bt.GetComponent<CharacterBase>();
        AnimationManager.Instance.SetTrigger(characterBase.animator, "Idle");

        Transform character = bt.transform;
        string label = characterBase.atributes.searchTarget.ToString();
        GameObject ball = GameObject.FindGameObjectWithTag(label);

        if (Vector3.Distance(character.position, ball.transform.position) < 1.2f && !Manager.Instance.ballIsHolded)
        {
            AnimationManager.Instance.SetTrigger(characterBase.animator, "Catch");
            yield return new WaitForSeconds(1.7f);
            if (!Manager.Instance.ballIsHolded)
            {
                Manager.Instance.ChatchBall(ball, characterBase.atributes.allyLabel, bt.gameObject);
                characterBase.HoldBall(ball);
                status = Status.SUCCESS;
            }
        }

        if (characterBase.haveTheBall)
        {
            status = Status.SUCCESS;
        }


        Print(bt.gameObject.name + " : " + Vector3.Distance(character.position, ball.transform.position).ToString());
        characterBase.currentState = State(Vector3.Distance(character.position, ball.transform.position).ToString());
        yield break;
    }
}
