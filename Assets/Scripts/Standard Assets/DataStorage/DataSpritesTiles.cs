using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/DataStorage/SpritesTiles")]
public class DataSpritesTiles : ScriptableObject
{
    [SerializeField] private List<Sprite> spritesTiles;

    public Sprite GetSpriteTile(int idTexture) => spritesTiles[idTexture];
}
