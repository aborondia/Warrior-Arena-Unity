using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
  // [SerializeField] Animator enemyAnimator;
  private Animator animator;
  bool regularAttack;
  [SerializeField] private string Name;
  private SpriteRenderer _spriteRenderer;
  private Sprite[] _sprites;
  void Start()
  {
    animator = GetComponent<Animator>();
  }
  
  public void Attack()
  {
    animator.SetBool("regularAttack", true);
    animator.SetTrigger("moveForward");
  }

  public void SpecialAttack()
  {
    animator.SetBool("regularAttack", false);
    animator.SetTrigger("moveForward");
  }

}
