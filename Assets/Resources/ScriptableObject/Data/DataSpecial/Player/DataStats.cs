using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Manager/StatsManager/Data")]
public class DataStats : ScriptableObject
{
    [SerializeField] private int citiesAmount;
    [SerializeField] private int goldAmount;

    [SerializeField] private List<Soldier> allUnits;
    [SerializeField] private List<Build> allBarrack;

    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite coverPlayer;


    public int CitiesAmount() => citiesAmount;
    public int GoldAmount() => goldAmount;

    public List<Soldier> GetAllSoldiers() => allUnits;
    public List<Build> GetAllBarracks() => allBarrack;

    public Sprite GetSpritePlayer() => playerSprite;
    public Sprite GetCoverPlayer() => coverPlayer;
    
    public void SetGoldAmount(int amount)
    {
        int goldAmountTest = 0;
        
        goldAmountTest = goldAmount + amount;

        if (goldAmountTest >= 0 && goldAmountTest <= 99999) goldAmount = goldAmountTest;

        if (goldAmountTest < 0) goldAmount = 0;
        if (goldAmountTest > 99999) goldAmount = 99999;
    }
    public void SetCityAmount(bool iWannaAddCity)
    {
        
        if (iWannaAddCity) citiesAmount++;
        else
        {
            int preCityAmount = citiesAmount;
            preCityAmount--;

            if (preCityAmount > 0) citiesAmount = preCityAmount;
        }
    }
    public void CalculateGoldByEachTurn()
    {
        int amountCitiesActual = citiesAmount;

        int addMoney = amountCitiesActual * 1000;
        SetGoldAmount(addMoney);
    }
    public void RemoveBarrack(BarrackUnits barrack)
    {
        allBarrack.Remove(barrack);
    }

    public void AddBarrack(Build barrack)
    {
        
        allBarrack.Add(barrack);
        //Debo anadir la id del menu tambien para que este todo correcto
    }

    public void RemoveSoldier(Soldier soldier)
    {
        allUnits.Remove(soldier);
    }

    public void AddSoldier(Soldier soldier)
    {
        allUnits.Add(soldier);
    }

    public void ResetStats()
    {
        citiesAmount = 0;
        goldAmount = 0;

        allUnits = new List<Soldier>();
        allBarrack = new List<Build>();
    }
}
