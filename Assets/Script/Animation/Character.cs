using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        movement = Input.GetAxis("Horizontal") * transform.right;
        movement += Input.GetAxis("Vertical") * transform.forward;
        Debug.DrawRay(transform.position, movement * 2, Color.cyan);

        animator.SetFloat("Velocity", movement.magnitude);
    }

    private void OnAnimatorIK()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 2;
        Vector3 position = Camera.main.ScreenToWorldPoint(mouse);

        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.2f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, position);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.2f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, position);
    }


}
