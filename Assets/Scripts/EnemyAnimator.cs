using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : CharacterAnimator
{
  [SerializeField] public string Name;
  private Sprite[] _sprites;
  private EnemyActionHandler enemyActionHandler;

  void Start()
  {
    animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _characterStateHandler = GetComponent<CharacterStateHandler>();
    characterMover = transform.parent.GetComponent<CharacterMover>();
    enemyActionHandler = GetComponent<EnemyActionHandler>();
    _sprites = Resources.LoadAll<Sprite>(Name);
    NextAction = "None";
    UnPauseAnimation();
    UpdateAnimClipTimes();
  }

  void LateUpdate()
  {
    int index;
    int.TryParse(_spriteRenderer.sprite.name.Replace("Adela_", ""), out index);
    _spriteRenderer.sprite = _sprites[index];
  }
}
