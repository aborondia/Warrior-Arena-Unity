using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
  private Dictionary<ActionHandler.Action, List<string>> actions;
  private Animator animator;
  private CharacterAnimator characterAnimator;

  public void Start()
  {
    characterAnimator = GetComponent<CharacterAnimator>();
    animator = GetComponent<Animator>();
    actions = GetActions();
  }

  public Dictionary<ActionHandler.Action, List<string>> GetActions()
  {
    string name = GetComponentInParent<EnemyAnimator>().Name;
    Dictionary<ActionHandler.Action, List<string>> actions = null;

    switch (name)
    {
      case "Elicia":
        actions = new Dictionary<ActionHandler.Action, List<string>>
        {
          [ActionHandler.Action.Attack] = new List<string>
          {
"I'm going to attack you."
          },
          [ActionHandler.Action.Special] = new List<string>
          {
"I'm going all out."
          },
          [ActionHandler.Action.Defend] = new List<string>
          {
"I'm going to defend."
          },
        };
        break;
    }

    return actions;
  }

  public void GetNextAction()
  {
    int attackChance = actions.Where(a => a.Key == ActionHandler.Action.Attack).Count() - 1;
    int specialChance = actions.Where(a => a.Key == ActionHandler.Action.Special).Count() + attackChance;
    int defendChance = actions.Where(a => a.Key == ActionHandler.Action.Defend).Count() + specialChance;
    int actionCount = actions.Values.Sum(v => v.Count());
    int nextActionNumber = UnityEngine.Random.Range(0, actionCount);
    ActionHandler.Action nextAction = ActionHandler.Action.None;

    SayBattleQuote(nextAction);

    if (nextActionNumber <= attackChance)
    {
      nextAction = ActionHandler.Action.Attack;
      characterAnimator.NextAction = ActionHandler.Action.Attack;
      return;
    }

    if (nextActionNumber <= specialChance)
    {
      nextAction = ActionHandler.Action.Special;
      characterAnimator.NextAction = ActionHandler.Action.Special;
      return;
    }

    if (nextActionNumber <= defendChance)
    {
      nextAction = ActionHandler.Action.Defend;
      characterAnimator.NextAction = ActionHandler.Action.Defend;
      return;
    }

    characterAnimator.NextAction = ActionHandler.Action.None;
  }

  public void ClearAction()
  {
    characterAnimator.NextAction = ActionHandler.Action.None;
  }

  public void StopDefending()
  {
    animator.SetBool("isDefending", false);
  }

  public void SayBattleQuote(ActionHandler.Action action)
  {
    var quotes = actions.Where(a => a.Key == action);

    //update later
  }
}
