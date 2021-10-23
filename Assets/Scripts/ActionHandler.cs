using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
  public enum Action
  {
    Attack,
    Special,
    ToBeDeterminedAttack,
    ToBeDeterminedIdle,
    ToBeDeterminedTakeDamage,
    Defend,
    MoveForward,
    MoveBackward,
    Flinch,
    None
  }
  [SerializeField] PlayerAnimator playerAnimator;
  [SerializeField] EnemyAnimator enemyAnimator;
  [SerializeField] CharacterStateHandler playerStateHandler;
  [SerializeField] CharacterStateHandler enemyStateHandler;
  private Action playerChosenAction
  {
    get
    {
      return playerAnimator.ChosenAction;
    }
  }
  private Action enemyChosenAction
  {
    get
    {
      return enemyAnimator.ChosenAction;
    }
  }
  private bool startNewRound = false;
  private bool currentRoundOver = true;
  private Dictionary<string, List<Action>> currentSequence;
  public int currentSequenceIndex = 0;
  private int currentSequenceLength
  {
    get
    {
      return currentSequence != null ? currentSequence["Player"].Count : 0;
    }
  }



  public Dictionary<string, List<Action>> NonDesiciveRoundSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.ToBeDeterminedAttack,
Action.ToBeDeterminedIdle,
Action.ToBeDeterminedTakeDamage,
Action.ToBeDeterminedIdle,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.ToBeDeterminedIdle,
Action.ToBeDeterminedTakeDamage,
Action.None,
Action.ToBeDeterminedAttack,
Action.None,
Action.None,
Action.None,
}
  };
  //add fight ending sequences
  void Update()
  {
    if (startNewRound == true)
    {
      startNewRound = false;
      currentRoundOver = false;
      SetRoundSequence();
    }

    if (currentRoundOver == false)
    {
      if (!playerAnimator.performingAction && !enemyAnimator.performingAction)
      {
        PerformNextSequence();
      }
    }
  }

  public void StartRound()
  {
    startNewRound = true;
  }

  public void SetRoundSequence()
  {
    //set up damage function and refactor to integrate into victory
    string fightVictor = GetRoundVictor();

    switch (fightVictor)
    {
      case "":
        SetNonDecisiveFightSequence();
        break;
      case "player":
        SetPlayerWinSequence();
        break;
      case "enemy":
        SetEnemyWinSequence();
        break;
    }
  }

  public void PerformNextSequence()
  {
    if (currentSequenceIndex >= currentSequenceLength)
    {
      currentSequenceIndex = 0;
      currentSequence = null;
      currentRoundOver = true;
      return;
    }

    playerAnimator.NextAction = currentSequence["Player"][currentSequenceIndex];
    enemyAnimator.NextAction = currentSequence["Enemy"][currentSequenceIndex];
    currentSequenceIndex++;

    SignalNextAction(playerAnimator);
    SignalNextAction(enemyAnimator);
  }

  public void SignalNextAction(CharacterAnimator characterAnimator)
  {
    var chosenAction = characterAnimator.GetType() == typeof(PlayerAnimator) ? playerChosenAction : enemyChosenAction;

    switch (characterAnimator.NextAction)
    {
      case Action.None:
        characterAnimator.Idle();
        break;
      case Action.MoveForward:
        characterAnimator.MoveForward();
        break;

      case Action.MoveBackward:
        characterAnimator.MoveBackward();
        break;

      case Action.ToBeDeterminedAttack:
        if (chosenAction == Action.Attack)
        {
          characterAnimator.Attack();
        }
        else
        {
          characterAnimator.Special();
        }
        break;

      case Action.ToBeDeterminedIdle:
        if (chosenAction == Action.None)
        {
          characterAnimator.Idle();
        }
        else
        {
          characterAnimator.Defend();
        }
        break;

      case Action.ToBeDeterminedTakeDamage:
        if (chosenAction == Action.Defend)
        {
          characterAnimator.Defend();
        }
        else
        {
          characterAnimator.Flinch();
        }
        break;
    }
  }

  public string GetRoundVictor()
  {
    if (playerStateHandler.Health <= 0)
    {
      return "enemy";
    }
    if (enemyStateHandler.Health <= 0)
    {
      return "player";
    }

    return "";
  }

  public void SetNonDecisiveFightSequence(int currentAction = 0)
  {
    currentSequence = NonDesiciveRoundSequence;
  }

  public void SetPlayerWinSequence()
  {

  }

  public void SetEnemyWinSequence()
  {

  }
}
