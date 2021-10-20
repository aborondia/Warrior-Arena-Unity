using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
  public string NextAction;
  protected float attackTime;
  protected float specialTime;
  protected float moveForwardTime;
  protected float moveBackwardTime;
  protected CharacterMover characterMover;

  protected AnimationClip clip;
  [SerializeField] CharacterAnimator enemyAnimator;
  [SerializeField] CharacterMover enemyMover;
  protected Animator animator;
  protected CharacterStateHandler _characterStateHandler;
  protected SpriteRenderer _spriteRenderer;

  public void ReturnToIdle()
  {
    animator.SetBool("isDefending", true);
    //if <50% HP Idle injured
    //initialize enemy action
  }

  public void Attack()
  {
    animator.SetBool("regularAttack", true);

    if (enemyMover.transform.position == enemyMover.basePosition)
    {
      animator.SetTrigger("moveForward");
    }
    else
    {
      animator.SetTrigger("attack");
    }
  }

  public void SpecialAttack()
  {
    animator.SetBool("regularAttack", false);

    if (enemyMover.transform.position == enemyMover.basePosition)
    {
      animator.SetTrigger("moveForward");
    }
    else
    {
      animator.SetTrigger("attack");
    }
  }

  public void Defend()
  {
    animator.SetBool("isDefending", true);
  }

  public void DeliverDamage()
  {
    enemyAnimator.TakeDamage();
  }

  public void TakeDamage()
  {
    //take damage
    if (!animator.GetBool("isDefending"))
    {
      animator.Play("Flinch");
      // animator.SetTrigger("takeDamage");
    }

    if (characterMover.transform.position != characterMover.basePosition)
    {
      animator.SetBool("movingBackward", true);
    }
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
    animator.SetBool("animationPaused", true);
  }

  public void UnPauseAnimation()
  {
    animator.SetBool("animationPaused", false);
  }

  public void TriggerEnemyAction()
  {
    enemyAnimator.PerformNextAction();
  }

  public void PerformNextAction()
  {
    if (_characterStateHandler.IsDead)
    {
      NextAction = "Dead";
    }

    switch (NextAction)
    {
      case "Attack":
        Attack();
        break;
      case "SpecialAttack":
        SpecialAttack();
        break;
      // case "Dead": TriggerEnd()
      // break;
      default:
        UnPauseAnimation();
        break;
    }
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
