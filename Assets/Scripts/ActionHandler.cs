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
    ToBeDetermined,
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
  private Action chosenAction
  {
    get
    {
      return playerAnimator.ChosenAction;
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



  public Dictionary<string, List<Action>> AttackVsAttackSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.Attack,
Action.None,
Action.Flinch,
Action.None,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.None,
Action.Flinch,
Action.None,
Action.Attack,
Action.None,
Action.None,
Action.None,
}
  };
  public Dictionary<string, List<Action>> SpecialVsAttackSequence = new Dictionary<string, List<Action>>
  {
    ["Player"] = new List<Action>{
Action.MoveForward,
Action.Special,
Action.None,
Action.Flinch,
Action.None,
Action.MoveBackward,
Action.None,
},
    ["Enemy"] = new List<Action>{
Action.None,
Action.Flinch,
Action.None,
Action.Attack,
Action.None,
Action.None,
Action.None,
}
  };

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
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
    switch (playerAnimator.ChosenAction)
    {
      case Action.Attack:
        switch (enemyAnimator.NextAction)
        {
          case Action.Attack:
            AttackVsAttack();
            break;
          case Action.Special:
            AttackVsAttack();
            break;
          case Action.Defend: break;
        }
        break;

      case Action.Special:
        switch (enemyAnimator.NextAction)
        {
          case Action.Attack:
            AttackVsAttack();
            break;
          case Action.Special:
            AttackVsAttack();
            break;
          case Action.Defend: break;
        }
        break;

      case Action.Defend:
        switch (enemyAnimator.NextAction)
        {
          case Action.Attack:
            break;
          case Action.Special: break;
          case Action.Defend: break;
        }
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

    switch (playerAnimator.NextAction)
    {
      case Action.Attack:
        playerAnimator.Attack();
        break;
      case Action.Special:
        playerAnimator.Special();
        break;
      case Action.Defend:
        playerAnimator.Defend();
        break;
      case Action.MoveForward:
        playerAnimator.MoveForward();
        break;
      case Action.MoveBackward:
        playerAnimator.MoveBackward();
        break;
      case Action.Flinch:
        playerAnimator.Flinch();
        break;
      case Action.None:
        playerAnimator.Idle();
        break;
    }

    switch (enemyAnimator.NextAction)
    {
      case Action.Attack:
        enemyAnimator.Attack();
        break;
      case Action.Special:
        enemyAnimator.Special();
        break;
      case Action.Defend:
        enemyAnimator.Defend();
        break;
      case Action.MoveForward:
        enemyAnimator.MoveForward();
        break;
      case Action.MoveBackward:
        enemyAnimator.MoveBackward();
        break;
      case Action.Flinch:
        enemyAnimator.Flinch();
        break;
      case Action.None:
        enemyAnimator.Idle();
        break;
    }
  }

  public void AttackVsAttack(int currentAction = 0)
  {
    // var playerActionToSet = AttackVsAttackSequence["Player"].Select(action => action == Action.ToBeDetermined);
    //deal damage
    //Add check for death
    currentSequence = AttackVsAttackSequence;
  }
}
