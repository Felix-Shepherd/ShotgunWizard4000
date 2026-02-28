using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandeler : MonoBehaviour
{
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsLand = Animator.StringToHash("IsLand");
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerData playerControl;

    private void Start()
    {
        playerMovement = transform.parent.GetComponentInChildren<PlayerMovement>();
        playerControl = transform.parent.GetComponentInChildren<PlayerData>();
    }

    void Update()
    {
        //running animation
        if (playerMovement._isGrounded && playerControl.IsMoving)
        {
            playerAnimator.SetBool(IsRunning, true);
        }
        else
        {
            playerAnimator.SetBool(IsRunning, false);
        }
        
        //jumping and landing animations
        playerAnimator.SetBool(IsJump, !playerMovement._isGrounded);
        playerAnimator.SetBool(IsLand, playerMovement._isGrounded);
        

    }
}
