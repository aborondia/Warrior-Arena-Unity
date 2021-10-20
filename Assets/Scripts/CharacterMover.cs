using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
  [SerializeField] public Vector3 basePosition = new Vector3(-5.12f, -0.994f, 0);
  [SerializeField] private Vector3 attackPosition = new Vector3(5.78f, -0.994f, 0);
  [SerializeField] private GameObject enemyMover;
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
  public void SetDestination(bool moveForward, float time)
  {
    t = 0;
    startPosition = transform.position;
    timeToReachTarget = time;
    if (moveForward)
    {
      target = enemyMover.transform.position - movePositionAdjustment;
    }
    else
    {
      target = basePosition;
    }

  }
}
