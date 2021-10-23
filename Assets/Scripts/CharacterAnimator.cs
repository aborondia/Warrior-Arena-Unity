using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
  public bool performingAction = false;
  public ActionHandler.Action NextAction;
  protected float attackTime;
  protected float specialTime;
  protected float moveForwardTime;
  protected float moveBackwardTime;
  protected CharacterMover characterMover;

  protected AnimationClip clip;
  protected Animator animator;
  protected SpriteRenderer _spriteRenderer;

  public void Update()
  {
    // if (string.IsNullOrEmpty(enemyCharacterAnimator.NextAction))
    // {
    //   UnPauseAnimation();
    // }
  }

  public void Attack()
  {
    animator.SetTrigger("regularAttack");
  }

  public void Special()
  {
    animator.SetTrigger("specialAttack");
  }

  public void Defend()
  {
    animator.SetTrigger("defend");
  }
  public void MoveForward()
  {
    characterMover.SetDestination(true, moveForwardTime);
    animator.SetTrigger("moveForward");
  }
  public void MoveBackward()
  {
    characterMover.SetDestination(false, moveBackwardTime);
    animator.SetTrigger("moveBackward");
  }
  public void Flinch()
  {
    animator.SetTrigger("flinch");
  }

  public void Idle()
  {
    animator.SetTrigger("idle");
  }
  public void StartPerformingAction()
  {
    performingAction = true;
  }

  public void StopPerformingAction()
  {
    performingAction = false;
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
