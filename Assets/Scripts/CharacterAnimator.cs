using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
  public ActionHandler.Action ChosenAction;
  public bool performingAction = false;
  public ActionHandler.Action NextAction;
  protected float attackTime;
  protected float specialTime;
  protected float moveForwardTime;
  protected float moveBackwardTime;
  protected float evadeTime;
  protected float counterTime;
  protected float dashAttackTime;
  protected CharacterMover characterMover;

  protected AnimationClip clip;
  protected Animator animator;
  protected SpriteRenderer _spriteRenderer;

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
    bool isPlayerCharacter = this.GetType() == typeof(PlayerAnimator) ? true : false;

    characterMover.SetDestination(isPlayerCharacter, true, moveForwardTime);
    animator.SetTrigger("moveForward");
  }
  public void MoveBackward()
  {
    bool isPlayerCharacter = this.GetType() == typeof(PlayerAnimator) ? true : false;

    characterMover.SetDestination(isPlayerCharacter, false, moveBackwardTime);
    animator.SetTrigger("moveBackward");
  }
  public void Flinch()
  {
    animator.SetTrigger("flinch");
  }

  public void Evade()
  {
    bool isPlayerCharacter = this.GetType() == typeof(PlayerAnimator) ? true : false;

    characterMover.SetDestination(isPlayerCharacter, false, evadeTime, true);
    animator.SetTrigger("evade");
  }

  public void Counter()
  {
    bool isPlayerCharacter = this.GetType() == typeof(PlayerAnimator) ? true : false;

    characterMover.SetDestination(isPlayerCharacter, true, counterTime, true);
    animator.SetTrigger("counter");
  }

  public void DashAttack()
  {
    bool isPlayerCharacter = this.GetType() == typeof(PlayerAnimator) ? true : false;

    characterMover.SetDestination(isPlayerCharacter, true, dashAttackTime);
    animator.SetTrigger("dashAttack");
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
        case "Evade":
          evadeTime = clip.length;
          break;
        case "Counter":
          counterTime = clip.length;
          break;
        case "DashAttack":
          dashAttackTime = clip.length;
          break;
      }
    }
  }
}
