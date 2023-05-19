using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
public class SpawnerUnit
{
    private Tile tilePosition;
    private AbstractBaseUnit unitPrefab;
    public SpawnerUnit(Tile tileWhereUnitWillBeSpawn, AbstractBaseUnit unitThatWillBeSpawned)
    {
        tilePosition = tileWhereUnitWillBeSpawn;
        unitPrefab = unitThatWillBeSpawned;

        InstantiateUnit();
    }
    public void InstantiateUnit()
    {
        if (tilePosition.occupiedSoldier && unitPrefab.typeUnit != TypesUnit.Build) return;
        
        var unit =  Object.Instantiate(unitPrefab, tilePosition.transform.position, Quaternion.identity);

        if (unitPrefab.typeUnit == TypesUnit.Soldier) tilePosition.setUnitComponent.SetUnitToTheGrid((Soldier)unit, tilePosition, false);
        if (unitPrefab.typeUnit == TypesUnit.Build) tilePosition.setUnitComponent.SetBuildToTheGrid((Build)unit, tilePosition);
    }
}

public class SpawnerUnitRefactor : ISpawnElementInTheGrid
{
    private UnitDataPrefab _dataPrefabUnit;

    private Soldier _soldierThatWillBeSpawneable;
    private TileRefactor _tileWhereMyUnitWillBe;

    private SOListUnitsPrefabs _unitsPrefabs;
    private int _lifeUnit;

    public SpawnerUnitRefactor(int lifeUnit)
    {
        _unitsPrefabs = Resources.Load<SOListUnitsPrefabs>("ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
        _lifeUnit = lifeUnit;
    }
    public void PutElement<T>(T elementThatITryToPutInTheGrid, TileRefactor tileWhereIWillPutIt) where T : ITsSpawnableInGrid
    {
        _dataPrefabUnit = elementThatITryToPutInTheGrid as UnitDataPrefab;
        _tileWhereMyUnitWillBe = tileWhereIWillPutIt;

        GetSoldier();
        if (ElementAccomplishRequirement() is false) return;
        SpawnSoldier();
    }

    private void GetSoldier()
    {
        var dictionary = _unitsPrefabs.GetDictionaryOfUnits();
        var typeUnit = dictionary[_dataPrefabUnit.nameUnit];
        _soldierThatWillBeSpawneable = (Soldier)typeUnit[(int)_dataPrefabUnit.factionUnit];
        //_soldierThatWillBeSpawneable.gameObject.SetActive(true);
    }

    public bool ElementAccomplishRequirement()
    {
         if (!_soldierThatWillBeSpawneable.terrainUnitCanTransit.Contains(_tileWhereMyUnitWillBe.dataVariable.terrainTypes)) return false;
         if (_tileWhereMyUnitWillBe.occupiedSoldier != null) DestroyPreviousUnit();
        return true;
    }

    private void DestroyPreviousUnit()
    {
        GameObject unitThatWillBeDestroy = _tileWhereMyUnitWillBe.occupiedSoldier.gameObject;
        UnsetUnitToTheGridRefactor.CleanReferences(_tileWhereMyUnitWillBe);
        Object.Destroy(unitThatWillBeDestroy.gameObject);
    }
    private void SpawnSoldier()
    {
        var unit = Object.Instantiate(_soldierThatWillBeSpawneable, _tileWhereMyUnitWillBe.positionTileInGrid,
            quaternion.identity);
        
        var setUnisInGrid = new SetUnitToTheGridRefactor();
        setUnisInGrid.SetUnit(_tileWhereMyUnitWillBe, unit);
        CommandQueue.GetInstance.AddCommand(new CommandDisableUnit(unit), false);

        if (_lifeUnit != 100)
        {
            unit.currentLife = _lifeUnit;
            unit.currentLifeUI = GetLifeUI(_lifeUnit);
            unit.managerLifeUnitUI.SetLife(unit);
        }
        
        AddDataToStats(unit);
        //AddNewUnitToTheDataMap(unit);
    }
    private int GetLifeUI(int currentLife)
    {
        var life = currentLife / 10;
        if (life == 0) life = 1;
        return life;
    }
    private void AddDataToStats(Soldier unit)
    {
        WorldScriptableObjects.GetInstance()._currentMapDisplayData.allSoldierInGrid.Add(unit);

        if (unit.playerThatCanControlThisUnit == FactionUnit.Neutral) return;
        
        var statsSelected = WorldScriptableObjects.GetInstance().statsPlayersManager
            .GetStatsPlayer(unit.playerThatCanControlThisUnit);
        statsSelected.AddSoldier(unit);
        CommandQueue.GetInstance.AddCommand(new CommandUpdateR1L1Stats(), false);
        //listSelected.Add(unit);
    }
}
    
public class SpawnerBuilds : ISpawnElementInTheGrid
{
    private TileRefactor _tileWhereBuildWillBe;
    private SOListUnitsPrefabs _unitsPrefabs;
    private UnitDataPrefab _dataPrefabBuild;
    private Build _buildThatWillBeSpawned;
    public SpawnerBuilds()
    {
        _unitsPrefabs = Resources.Load<SOListUnitsPrefabs>("ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
    }
    public void PutElement<T>(T elementThatITryToPutInTheGrid, TileRefactor tileWhereIWillPutIt) where T : ITsSpawnableInGrid
    {
        _dataPrefabBuild = elementThatITryToPutInTheGrid as UnitDataPrefab;
        _tileWhereBuildWillBe = tileWhereIWillPutIt;

        GetBuild();
        if (ElementAccomplishRequirement() is false) return;

        InstantiateBuild(tileWhereIWillPutIt);
    }
    public bool ElementAccomplishRequirement()
    {
        if( _tileWhereBuildWillBe.dataVariable.terrainTypes != _buildThatWillBeSpawned.typeTileWhereMyBuildCanBe) return false;
        if (_tileWhereBuildWillBe.occupiedBuild != null) DestroyPreviousBuild(_tileWhereBuildWillBe.occupiedBuild);
        return true;
    }

    private void InstantiateBuild(TileRefactor tileWhereIWillPutIt)
    {
        var build = Object.Instantiate(_buildThatWillBeSpawned, tileWhereIWillPutIt.positionTileInGrid, quaternion.identity);
        var setBuildToTheGrid = new SetUnitToTheGridRefactor();
        setBuildToTheGrid.SetBuild(tileWhereIWillPutIt, build);
        AddToStats(build);
    }
    private void GetBuild()
    {
        var dictionary = _unitsPrefabs.GetDictionaryOfUnits();
        var typeUnit = dictionary[_dataPrefabBuild.nameUnit];
        _buildThatWillBeSpawned = (Build)typeUnit[(int)_dataPrefabBuild.factionUnit];
    }

    private void AddToStats(Build build)
    {
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var allBuilds = currentMapData.allBuildInGrid;
        
        if (!allBuilds.Contains(build)) allBuilds.Add(build);
        
        if (build.playerThatCanControlThisUnit == FactionUnit.Neutral) return;

        var currentPlayerFromThisBuild = build.playerThatCanControlThisUnit;
        var currentStats = WorldScriptableObjects.GetInstance().statsPlayersManager
            .GetStatsPlayer(currentPlayerFromThisBuild);
        
        if (build.nameUnit == NameUnit.City) currentStats.SetCityAmount(true);
        if (build.nameUnit == NameUnit.HQ ||
            build.nameUnit == NameUnit.Port ||
            build.nameUnit == NameUnit.Arprt)
        {
            currentStats.AddBarrack(_buildThatWillBeSpawned);
        }
        if (build.nameUnit == NameUnit.Base)
        {
            //I do all this, because i just wanna one MainCity for eachPlayer, first we got all the variables, second we
            //look if we had something already in the tile, if yes, we delete the previous one and change the data, if no
            //just add the data.
            var idTileMainCityFromPlayer = currentMapData.idTileMainCityEachPlayer[_dataPrefabBuild.factionUnit];
            var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
            var currentMainCityPlayer = allTiles[idTileMainCityFromPlayer].occupiedBuild;
            
            if (currentMainCityPlayer) 
            {
                if (currentMainCityPlayer.nameUnit != NameUnit.Base) return;
                var previousMainCity = allTiles[idTileMainCityFromPlayer].occupiedBuild;
                DestroyPreviousBuild(previousMainCity);
                currentMapData.idTileMainCityEachPlayer[_dataPrefabBuild.factionUnit] = _tileWhereBuildWillBe.spawnID;
            }
            else
            {
                currentMapData.idTileMainCityEachPlayer[_dataPrefabBuild.factionUnit] = _tileWhereBuildWillBe.spawnID;
                currentStats.SetCityAmount(true);
            }
        }
    }
    private void DestroyPreviousBuild(Build buildToDestroy)
    {
        UnsetUnitToTheGridRefactor.UnSetBuild(buildToDestroy);
        Object.Destroy(buildToDestroy.gameObject);
    }
}