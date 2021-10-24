using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
  Vector3 basePlayerPosition = new Vector3(-5.12f, -0.994f, 0);
  Vector3 playerEvadePosition = new Vector3(-7.12f, -0.994f, 0);
  Vector3 baseEnemyPosition = new Vector3(6.44f, -0.994f, 0);
  Vector3 enemyEvadePosition = new Vector3(8.44f, -0.994f, 0);
  [SerializeField] private GameObject enemyCharacterMover;
  [SerializeField] private Vector3 movePositionAdjustment;
  float t;
  Vector3 startPosition;
  Vector3 target;
  float timeToReachTarget;
  void Start()
  {
    startPosition = target = transform.position;
  }
  void Update()
  {
    t += Time.deltaTime / timeToReachTarget;
    transform.position = Vector3.Lerp(startPosition, target, t);
  }
  public void SetDestination(bool isPlayerCharacter, bool moveForward, float time, bool countering = false)
  {
    Vector3 basePosition = isPlayerCharacter ? basePlayerPosition : baseEnemyPosition;
    Vector3 counterPosition = isPlayerCharacter ? playerEvadePosition : enemyEvadePosition;

    t = 0;
    startPosition = transform.position;
    timeToReachTarget = time;
    
    if (moveForward)
    {
      target = countering ? basePosition : enemyCharacterMover.transform.position - movePositionAdjustment;
    }
    else
    {
      target = countering ? counterPosition : basePosition;
    }
  }
}
