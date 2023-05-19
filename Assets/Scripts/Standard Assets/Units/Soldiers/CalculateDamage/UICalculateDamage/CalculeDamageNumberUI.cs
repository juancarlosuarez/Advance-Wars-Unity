using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculeDamageNumberUI : MonoBehaviour
{
    [SerializeField] List<Sprite> number0to9;
    private List<int> eachNumberIndividually;

    private Sprite numberRight;
    private Sprite numberLeft;

    [SerializeField] SpriteRenderer right;
    [SerializeField] SpriteRenderer left;

    public void CalculateDamageUI(int damage)
    {
        List<int> splitNumber = SplitDamage(damage);

        SetSpriteForEachNumber(splitNumber);

        ShowSprites();

    }
    private List<int> SplitDamage(int damage)
    {
        eachNumberIndividually = new List<int>();
        while (damage > 0)
        {
            eachNumberIndividually.Add(damage % 10);
            damage = damage / 10;
        }
        return eachNumberIndividually;
    }
    private void SetSpriteForEachNumber(List<int> splitNumbers)
    {
        numberRight = ConverterIntToSprite(splitNumbers[0]) ;
        if (splitNumbers.Count != 0)
        {
            numberLeft = ConverterIntToSprite(splitNumbers[1]) ;
        }
    }
    private Sprite ConverterIntToSprite(int splitNumber)
    {
        switch (splitNumber)
        {
            case 0: return number0to9[0];
            case 1: return number0to9[1];
            case 2: return number0to9[2];
            case 3: return number0to9[3];
            case 4: return number0to9[4];
            case 5: return number0to9[5];
            case 6: return number0to9[6];
            case 7: return number0to9[7];
            case 8: return number0to9[8];
            case 9: return number0to9[9];
        }
        print(splitNumber) ;
        return null;
    }
    private void ShowSprites()
    {
        right.sprite = numberRight;
        if (eachNumberIndividually.Count != 0) left.sprite = numberLeft;
    }
}
