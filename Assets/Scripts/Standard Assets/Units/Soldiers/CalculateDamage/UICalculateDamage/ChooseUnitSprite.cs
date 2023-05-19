using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseUnitSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeSprite(Soldier unit)
    {
        spriteRenderer.sprite = unit.spriteUI;
    }
}
