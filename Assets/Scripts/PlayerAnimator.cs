using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
  void Start()
  {
    animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _characterStateHandler = GetComponent<CharacterStateHandler>();
    characterMover = transform.parent.GetComponent<CharacterMover>();
    UpdateAnimClipTimes();
  }
}
