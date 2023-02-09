using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    static Animator animator;
    private bool isMovingForward, isMovingBackward, isMovingLeft, isMovingRight, isMoving, isSprinting, isAttacking;
    private int isForwardHash, isSprintingHash, isBackwardHash, isLeftHash, isRightHash, isAttackingHash;
    public static bool startJump;
    public static bool startDodge;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.fireEvents = false;
        isForwardHash = Animator.StringToHash("isForward");
        //isBackwardHash = Animator.StringToHash("isBackward");
        //isLeftHash = Animator.StringToHash("isLeft");
        //isRightHash = Animator.StringToHash("isRight");
        isSprintingHash = Animator.StringToHash("isSprinting");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        isMovingForward = animator.GetBool(isForwardHash);
        //isMovingBackward = animator.GetBool(isBackwardHash);
        //isMovingLeft = animator.GetBool(isLeftHash);
        //isMovingRight = animator.GetBool(isRightHash);
        isSprinting = animator.GetBool(isSprintingHash);
        isAttacking = animator.GetBool(isAttackingHash);
        isMoving = isMovingForward; //|| isMovingBackward || isMovingLeft || isMovingRight;
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool movePressed = forwardPressed || backwardPressed || leftPressed || rightPressed;
        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);
        bool attackPressed = Input.GetKey(KeyCode.Mouse0);

        if (!isMoving && movePressed)
        {
            animator.SetBool("isForward", true);
        }

        if (isMoving && !movePressed)
        {
            animator.SetBool("isForward", false);
        }
        /*
        if (!isMovingBackward && backwardPressed)
        {
            animator.SetBool("isBackward", true);
        }

        if (isMovingBackward && !backwardPressed)
        {
            animator.SetBool("isBackward", false);
        }

        if (!isMovingLeft && leftPressed)
        {
            animator.SetBool("isLeft", true);
        }

        if (isMovingLeft && !leftPressed)
        {
            animator.SetBool("isLeft", false);
        }

        if (!isMovingRight && rightPressed)
        {
            animator.SetBool("isRight", true);
        }

        if (isMovingRight && !rightPressed)
        {
            animator.SetBool("isRight", false);
        }
        */

        if (!isSprinting && (isMoving && sprintPressed))
        {
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && (!isMoving || !sprintPressed))
        {
            animator.SetBool("isSprinting", false);
        }

        if (!startJump && !isAttacking && attackPressed)
        {
            animator.SetBool("isAttacking", true);
        }

        if (isAttacking && !attackPressed)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public static void StartJumpAnimation()
    {
        animator.SetBool("isJumping", true);
    }

    public static void StopJumpAnimation()
    {
        animator.SetBool("isJumping", false);
    }

    public static void StartDodgeAnimation()
    {
        animator.SetBool("isDodging", true);
    }

    public static void StopDodgeAnimation()
    {
        animator.SetBool("isDodging", false);
    }

    public static void StartCrouchAnimation()
    {
        animator.SetBool("isCrouching", true);
    }

    public static void StopCrouchAnimation()
    {
        animator.SetBool("isCrouching", false);
    }
}
