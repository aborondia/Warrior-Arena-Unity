using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHandler : MonoBehaviour
{
  [SerializeField] public int Health = 50;
  public bool IsInjured
  {
    get
    {
      return Health < (Health / 2);
    }
  }
  public bool IsDead
  {
    get
    {
      return Health <= 0;
    }
  }
}
