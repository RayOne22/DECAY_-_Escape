using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] Player player;


    public void JumpUp()
    {
        if (player.isGrounded && !player.s_AnimationDown && !player.isNoJump)
        {
            player.isGrounded = false;
            player.velocity.y = player.jumpVelocity;
            player.isHoldingJump = true;
            player.holdJumpTimer = 0;
            player.componentAnimator.SetTrigger("jump");
            player.componentAudioSource.PlayOneShot(player.playerFX[0]);
            player.s_AnimationDown = true;
        }
    }

    public void JumpDown()
    {
        player.isHoldingJump = false;
    }
}
