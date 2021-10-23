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
    characterMover = transform.parent.GetComponent<CharacterMover>();
    enemyActionHandler = GetComponent<EnemyActionHandler>();
    _sprites = Resources.LoadAll<Sprite>(Name);
    NextAction = ActionHandler.Action.Attack;
    UpdateAnimClipTimes();
  }

  void LateUpdate()
  {
    int index;
    int.TryParse(_spriteRenderer.sprite.name.Replace("Adela_", ""), out index);
    _spriteRenderer.sprite = _sprites[index];
  }

  public void GetNextAction(){
    //more
  }
}
