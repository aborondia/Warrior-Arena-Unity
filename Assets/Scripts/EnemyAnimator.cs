using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : CharacterAnimator
{
  [SerializeField] private string Name;
  private Sprite[] _sprites;

  void Start()
  {
    animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _characterStateHandler = GetComponent<CharacterStateHandler>();
    characterMover = transform.parent.GetComponent<CharacterMover>();
    _sprites = Resources.LoadAll<Sprite>(Name);
    UnPauseAnimation();
    NextAction = "Attack";
    UpdateAnimClipTimes();
  }

  void LateUpdate()
  {
    int index = int.Parse(_spriteRenderer.sprite.name.Replace("Adela_", ""));
    _spriteRenderer.sprite = _sprites[index];
  }
}
