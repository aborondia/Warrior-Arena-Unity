using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
 
  [SerializeField] ActionHandler actionHandler;
  void Start()
  {
    animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    characterMover = transform.parent.GetComponent<CharacterMover>();
    UpdateAnimClipTimes();
  }

  public void SetActionToAttack()
  {
    ChosenAction = ActionHandler.Action.Attack;
    actionHandler.StartRound();
  }

  public void SetActionToSpecial()
  {
    ChosenAction = ActionHandler.Action.Special;
    actionHandler.StartRound();
  }

  public void SetActionToDefend()
  {
    ChosenAction = ActionHandler.Action.Defend;
    actionHandler.StartRound();
  }
}
