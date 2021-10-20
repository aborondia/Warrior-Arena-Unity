using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
  protected float attackTime;
  protected float specialTime;
  protected float moveForwardTime;
  protected float moveBackwardTime;
  protected CharacterMover characterMover;

  protected AnimationClip clip;
  [SerializeField] CharacterAnimator enemyAnimator;
  protected Animator animator;
  protected CharacterStateHandler _characterStateHandler;
  protected SpriteRenderer _spriteRenderer;
  public void Attack()
  {
    animator.SetBool("regularAttack", true);
    animator.SetTrigger("moveForward");
  }

  public void SpecialAttack()
  {
    animator.SetBool("regularAttack", false);
    animator.SetTrigger("moveForward");
  }

  public void MoveForward()
  {
    characterMover.SetDestination(true, moveForwardTime);
  }

  public void MoveBackward()
  {
    characterMover.SetDestination(false, moveBackwardTime);
  }

  public void PauseAnimation()
  {
    animator.SetBool("animationNotPaused", false);
  }

  public void UnPauseAnimation()
  {
    animator.SetBool("animationNotPaused", true);
  }

  public void TriggerEnemyAction()
  {
    enemyAnimator.Attack();
  }

  public void UpdateAnimClipTimes()
  {
    AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
    foreach (AnimationClip clip in clips)
    {
      switch (clip.name)
      {
        case "RegularAttack":
          attackTime = clip.length;
          break;
        case "SpecialAttack":
          specialTime = clip.length;
          break;
        case "MoveForward":
          moveForwardTime = clip.length;
          break;
        case "MoveBackward":
          moveBackwardTime = clip.length;
          break;
      }
    }
  }
}
