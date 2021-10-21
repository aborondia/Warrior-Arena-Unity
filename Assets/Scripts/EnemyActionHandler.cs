using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
  public enum ActionType
  {
    Attack,
    Special,
    Defend,
    None
  }
  private Dictionary<ActionType, List<string>> actions;
  private CharacterAnimator characterAnimator;

  public void Start()
  {
    characterAnimator = GetComponent<CharacterAnimator>();
    actions = GetActions();
  }

  public Dictionary<ActionType, List<string>> GetActions()
  {
    string name = GetComponentInParent<EnemyAnimator>().Name;
    Dictionary<ActionType, List<string>> actions = null;

    switch (name)
    {
      case "Elicia":
        actions = new Dictionary<ActionType, List<string>>
        {
          [ActionType.Attack] = new List<string>
          {
"I'm going to attack you."
          },
          [ActionType.Special] = new List<string>
          {
"I'm going all out."
          },
          [ActionType.Defend] = new List<string>
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
    int attackChance = actions.Where(a => a.Key == ActionType.Attack).Count() - 1;
    int specialChance = actions.Where(a => a.Key == ActionType.Special).Count() + attackChance;
    int defendChance = actions.Where(a => a.Key == ActionType.Defend).Count() + specialChance;
    int actionCount = actions.Values.Sum(v => v.Count());
    int nextActionNumber = UnityEngine.Random.Range(0, actionCount);
    ActionType nextAction = ActionType.None;

    SayBattleQuote(nextAction);

    if (nextActionNumber <= attackChance)
    {
      nextAction = ActionType.Attack;
      characterAnimator.NextAction = "Attack";
      return;
    }

    if (nextActionNumber <= specialChance)
    {
      nextAction = ActionType.Special;
      characterAnimator.NextAction = "Special";
      return;
    }

    if (nextActionNumber <= defendChance)
    {
      nextAction = ActionType.Defend;
      characterAnimator.NextAction = "Defend";
      return;
    }

    characterAnimator.NextAction = "None";
  }

  public void ClearAction()
  {
    characterAnimator.NextAction = "None";
  }

  public void SayBattleQuote(ActionType action)
  {
    var quotes = actions.Where(a => a.Key == action);

    //update later
  }
}
