using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class UnitManager : MonoBehaviour
{
    //DElette this
    public static UnitManager sharedInstance;
    private List<ScriptableUnit> _units;
    private List<ScriptableBuilds> _builds;

    public Soldier selectedHero;

    public Soldier selectedEnemy;
    private TileReference tileSelected;

    

    void Awake()
    {
        sharedInstance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        _builds = Resources.LoadAll<ScriptableBuilds>("Builds").ToList();
    }

    public void SpawnHeroes()
    {
        
        //tileStartSelect.setUnitComponent.SetUnitToTheGrid(Selector.sharedInstance, tileStartSelect, true);

        for (int i = 0; i < GridManager.sharedInstance.player1list.Count; i++)
        {
            var randomPrefab = GetRandomUnit<Soldier>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.sharedInstance.GetHeroSpawnTile(i);

            randomSpawnTile.setUnitComponent.SetUnitToTheGrid(spawnedHero, randomSpawnTile, true);
        }
        
        GameManager.sharedInstance.ChangeState(StateSpawnGamePlayElements.SpawnEnemies);
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < GridManager.sharedInstance.player2List.Count; i++)
        {
            var randomPrefab = GetRandomUnit<Soldier>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.sharedInstance.GetPlayer2Tile(i);
            
            randomSpawnTile.setUnitComponent.SetUnitToTheGrid(spawnedEnemy, randomSpawnTile, true);
        }
    }
    public void SpawnBuild()
    {
        SpawnBarracks();
        SpawnCityNeutral();
        SpawnCityPlayer1();
    }
    public void SpawnBarracks()
    {
        for (int i = 0; i < GridManager.sharedInstance.enemyBarrack.Count; i++)
        {
            //var buildPrefab = GetBuild<BaseBuilds>(TypeOfBuild.Military);
            var dictionaryOfUnits = Resources.Load<SOListUnitsPrefabs>("ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
            var unitDictionaryArray = dictionaryOfUnits.StartDictionary()[NameUnit.Base];

            
            var spawnedBuild = Instantiate(unitDictionaryArray[0]);

            Tile tileWhereBarrackIs = GridManager.sharedInstance.GetBarrackAlly(i);
            tileWhereBarrackIs.setUnitComponent.SetBuildToTheGrid((Build)spawnedBuild, tileWhereBarrackIs);
        }
    }
    public void SpawnCityNeutral()
    {
        for (int i = 0; i < GridManager.sharedInstance.cityNeutral.Count; i++)
        {
            var dictionaryOfUnits =
                Resources.Load<SOListUnitsPrefabs>(
                    "ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
            var unitDictionaryArray = dictionaryOfUnits.GetDictionaryOfUnits()[NameUnit.City];

            var spawnedCity = Instantiate(unitDictionaryArray[0]);

            Tile tileWhereIWillPutThisCity = GridManager.sharedInstance.GetCityNeutral(i);
            tileWhereIWillPutThisCity.setUnitComponent.SetBuildToTheGrid((Build)spawnedCity, tileWhereIWillPutThisCity);
        }
    }
    public void SpawnCityPlayer1()
    {
        List<Build> listCities = new List<Build>();
        for (int i = 0; i < GridManager.sharedInstance.cityNeutral.Count; i++)
        {
            var dictionaryOfUnits =
                Resources.Load<SOListUnitsPrefabs>(
                    "ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
            var unitDictionaryArray = dictionaryOfUnits.StartDictionary()[NameUnit.City];

            var spawnedCity = Instantiate(unitDictionaryArray[1]);
            
            Tile tileWhereIWillPutThisCity = GridManager.sharedInstance.GetCityPlayer1(i);
            tileWhereIWillPutThisCity.setUnitComponent.SetBuildToTheGrid((Build)spawnedCity, tileWhereIWillPutThisCity);
            
            listCities.Add((Build)spawnedCity);
        }
        //PlayerStats.GetInstance().GetStatFromPlayer(FactionUnit.Player1).SetListBuilds(listCities);
    }
    private T GetBuild<T>(TypeOfBuild typeOfBuildAssign) where T : Build
    {
        
        return (T)_builds.Where(u => u.typeOfBuild == typeOfBuildAssign).OrderBy(o => 0).First().buildPrefab;
    }
    private T GetRandomUnit<T>(Faction factionUnit) where T : Soldier
    {
        return (T)_units.Where(u => u.Faction == factionUnit).OrderBy(o => Random.value).First().prefab;
    }
}
