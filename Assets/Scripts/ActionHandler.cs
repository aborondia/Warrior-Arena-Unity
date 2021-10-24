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
    Evade,
    Counter,
    DashAttack,
    Flinch,
    None
  }
  [SerializeField] PlayerAnimator playerAnimator;
  [SerializeField] EnemyAnimator enemyAnimator;
  [SerializeField] CharacterStateHandler playerStateHandler;
  [SerializeField] CharacterStateHandler enemyStateHandler;
  [SerializeField] EnemyActionHandler enemyActionHandler;
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
  public Dictionary<string, List<Action>> currentSequence;
  public int currentSequenceIndex = 0;
  private int currentSequenceLength
  {
    get
    {
      return currentSequence != null ? currentSequence["Player"].Count : 0;
    }
  }


  public Dictionary<string, List<Action>> PlayerCounteredSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.Special,
Action.Flinch,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.None,
Action.Evade,
Action.Counter,
Action.None,
Action.None,
}
  };

  public Dictionary<string, List<Action>> EnemyCounteredSequence = new Dictionary<string, List<Action>>
  {
    ["Enemy"] = new List<Action>{
Action.MoveForward,
Action.Special,
Action.Flinch,
Action.MoveBackward,
Action.None,
},
    ["Player"] = new List<Action>{
Action.None,
Action.Evade,
Action.Counter,
Action.None,
Action.None,
}
  };

  public Dictionary<string, List<Action>> PlayerDefendSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.Defend,
Action.Defend,
Action.None,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.MoveForward,
Action.Attack,
Action.MoveBackward,
Action.None,
}
  };

  public Dictionary<string, List<Action>> EnemyDefendSequence = new Dictionary<string, List<Action>>
  {
    ["Enemy"] = new List<Action>{
Action.Defend,
Action.Defend,
Action.None,
Action.None,
},
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.Attack,
Action.MoveBackward,
Action.None,
}
  };

  public Dictionary<string, List<Action>> AttackVsAttackSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.ToBeDeterminedAttack,
Action.Flinch,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.None,
Action.Flinch,
Action.ToBeDeterminedAttack,
Action.None,
Action.None,
}
  };

  public Dictionary<string, List<Action>> DefendVsDefendSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.Defend,
Action.Defend,
Action.Defend,
Action.None,

},
    ["Enemy"] = new List<Action>{
Action.Defend,
Action.Defend,
Action.Defend,
Action.None,

}
  };

  public Dictionary<string, List<Action>> SpecialVsSpecialSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.DashAttack,
Action.Flinch,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.DashAttack,
Action.Flinch,
Action.MoveBackward,
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
        SetNonDecisiveFightSequences();
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
      EndRound();
      return;
    }

    playerAnimator.NextAction = currentSequence["Player"][currentSequenceIndex];
    enemyAnimator.NextAction = currentSequence["Enemy"][currentSequenceIndex];
    currentSequenceIndex++;


    SignalNextAction(playerAnimator);
    SignalNextAction(enemyAnimator);
  }

  public void EndRound()
  {
    currentSequenceIndex = 0;
    currentSequence = null;
    currentRoundOver = true;
    playerAnimator.ChosenAction = Action.None;
    enemyActionHandler.GetNextAction();
  }

  public void SignalNextAction(CharacterAnimator characterAnimator)
  {

    var chosenAction = characterAnimator.GetType() == typeof(PlayerAnimator) ? playerChosenAction : enemyChosenAction;

    switch (characterAnimator.NextAction)
    {
      case Action.None:
        characterAnimator.Idle();
        break;
      case Action.Attack:
        characterAnimator.Attack();
        break;
      case Action.Special:
        characterAnimator.Special();
        break;
      case Action.Defend:
        characterAnimator.Defend();
        break;
      case Action.Flinch:
        characterAnimator.Flinch();
        break;
      case Action.MoveForward:
        characterAnimator.MoveForward();
        break;

      case Action.MoveBackward:
        characterAnimator.MoveBackward();
        break;

      case Action.Evade:
        characterAnimator.Evade();
        break;

      case Action.Counter:
        characterAnimator.Counter();
        break;

      case Action.DashAttack:
        characterAnimator.DashAttack();
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
        if (chosenAction == Action.Defend)
        {
          characterAnimator.Defend();
        }
        else
        {
          characterAnimator.Idle();
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

  public void SetNonDecisiveFightSequences(int currentAction = 0)
  {
    if (playerChosenAction == Action.Special && enemyChosenAction == Action.Defend)
    {
      currentSequence = PlayerCounteredSequence;
      return;
    }

    if (enemyChosenAction == Action.Special && playerChosenAction == Action.Defend)
    {
      currentSequence = EnemyCounteredSequence;
      return;
    }

    if (playerChosenAction == Action.Attack && enemyChosenAction == Action.Defend)
    {
      currentSequence = EnemyDefendSequence;
      return;
    }

    if (enemyChosenAction == Action.Attack && playerChosenAction == Action.Defend)
    {
      currentSequence = PlayerDefendSequence;
      return;
    }

    if (enemyChosenAction == Action.Defend && playerChosenAction == Action.Defend)
    {
      currentSequence = DefendVsDefendSequence;
      return;
    }

    if (enemyChosenAction == Action.Special && playerChosenAction == Action.Special)
    {
      currentSequence = SpecialVsSpecialSequence;
      return;
    }

    currentSequence = AttackVsAttackSequence;
  }

  public void SetPlayerWinSequence()
  {

  }

  public void SetEnemyWinSequence()
  {

  }
}
