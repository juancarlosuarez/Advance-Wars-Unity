using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PoolHighLightTiles
{
    //private Dictionary<HighLightTypes, >
    private static PoolHighLightTiles _sharedInstance;

    private List<HighLightTilesData> _highLightTilesDatas;
    private Dictionary<HighLightTypes, Queue<GameObject>> _dictionary;

    private List<GameObject> _elementsOutsidePool = new List<GameObject>();
    private PoolHighLightTiles()
    {
        _highLightTilesDatas = Resources.Load<SOHighLightTilesData>("").reference;
        AssignValuesToThePool();    
    }

    private void AssignValuesToThePool()
    {
        foreach (var c in _highLightTilesDatas)
        {
            Queue<GameObject> allElementsSpawned = new Queue<GameObject>();
            for (int i = 0; i < c.spawnCount; i++)
            {
                var highLightTile = Object.Instantiate(c.prefab);
                highLightTile.SetActive(false);
                allElementsSpawned.Enqueue(highLightTile);
            }
            _dictionary.Add(c.highLightTypes, allElementsSpawned);
        }
    }
    public void PutBackAllTheElementsOutToInsideThePool()
    {
        foreach (var c in _elementsOutsidePool) c.SetActive(false);
        _elementsOutsidePool.Clear();
    }
    public void SpawnFromThePool(HighLightTypes highLightTypes, List<Vector2> positions)
    {
        var elementsSelected = _dictionary[highLightTypes];

        foreach (var c in positions)
        {
            var elementHighLightTile = elementsSelected.Dequeue();
            elementHighLightTile.SetActive(true);
            elementHighLightTile.transform.position = c;
            
            _elementsOutsidePool.Add(elementHighLightTile);
        }
    }
    public static PoolHighLightTiles GetInstance()
    {
        if (_sharedInstance is null) _sharedInstance = new PoolHighLightTiles();
        return _sharedInstance;
    }
    
}
[CreateAssetMenu(menuName = "ScriptableObject/ContainerStruct/HighLightTilesData")]
public class SOHighLightTilesData : ScriptableObject
{
    public List<HighLightTilesData> reference;
} 
public enum HighLightTypes
{
    PathFindingCurrentPlayer, PathFindingEnemy, ObjectiveAttack, RangeActionCurrentPlayer, RangeActionEnemy
}
[System.Serializable]
public struct HighLightTilesData 
{
    public GameObject prefab;
    public int spawnCount;
    public HighLightTypes highLightTypes;
}
