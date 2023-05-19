using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPreDamageUI : MonoBehaviour
{
    //DEbor borrar esto
    public static SystemPreDamageUI sharedInstance;

    [SerializeField] CalculeDamageNumberUI attackerDamageUI;
    [SerializeField] CalculeDamageNumberUI defenserDamageUI;
    [SerializeField] ChooseUnitSprite attackerSpriteUI;
    [SerializeField] ChooseUnitSprite defenserSpriteUI;

    [SerializeField] GameObject UICalculatePreDamage;
    private void Awake()
    {
        sharedInstance = this;
    }
    public void OpenMenu(int attackerDamage, int defenserDamage, Soldier attacker, Soldier defenser)
    {
        UICalculatePreDamage.SetActive(true);

        Vector2 positionMenu = new Vector2(defenser.transform.position.x, defenser.transform.position.y + 1);
        ColocateMenuInRightPosition(positionMenu);

        attackerDamageUI.CalculateDamageUI(attackerDamage);
        defenserDamageUI.CalculateDamageUI(defenserDamage);

        attackerSpriteUI.ChangeSprite(attacker);
        defenserSpriteUI.ChangeSprite(defenser);
    }
    private void ColocateMenuInRightPosition(Vector2 positionMenu)
    {
        UICalculatePreDamage.transform.position = positionMenu;
    }
    public void CloseMenu()
    {
        UICalculatePreDamage.SetActive(false);
    }
}
