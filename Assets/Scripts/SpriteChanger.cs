using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
  [SerializeField] private string Name;
  [SerializeField] Sprite newSprite;
  private SpriteRenderer _spriteRenderer;
  private Sprite[] _sprites;

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _spriteRenderer.sprite = newSprite;
    _sprites = Resources.LoadAll<Sprite>("Elicia");
  }
  void Start()
  {

  }

  void Update()
  {
    //   int index = int.Parse(_spriteRenderer.sprite.name.Replace("Adela_", ""));
      _spriteRenderer.sprite = _sprites[0];
//     int actionNumber = int.Parse(_spriteRenderer.sprite.name.Replace(Name,""));
// Sprite newSprite = _sprites.

//     switch (actionNumber)
//     {
//       case 0: return;
//       case 1: return;
//       case 2: return;
//       case 3: return;
//     }
  }
}
