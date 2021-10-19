using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
  private Animator animator;
    [SerializeField] private string Name;
      private SpriteRenderer _spriteRenderer;
  private Sprite[] _sprites;
  bool regularAttack;
  void Start()
  {
    animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _sprites = Resources.LoadAll<Sprite>(Name);
  }

  void LateUpdate()
  {
    int index = int.Parse(_spriteRenderer.sprite.name.Replace("Elicia_", ""));
    _spriteRenderer.sprite = _sprites[index];
  }

  public void Attack()
  {
    animator.SetBool("regularAttack", true);
    animator.SetTrigger("moveForward");
  }

  public void SpecialAttack(){
    animator.SetBool("regularAttack", false);
    animator.SetTrigger("moveForward");

  }
}
