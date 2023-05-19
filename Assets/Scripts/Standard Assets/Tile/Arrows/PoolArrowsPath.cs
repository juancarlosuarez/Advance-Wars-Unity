using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolArrowsPath
{
    private ArrowsDataPoolSO dataArrowPool;

    private Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>> _dictionary = new Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>>();
    private Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>> _dictionaryElementsOutsidePool = new Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>>();


    public PoolArrowsPath()
    {
        dataArrowPool = WorldScriptableObjects.GetInstance().poolDataPathArrows;
        AssignValuesToThePool();
    }
    private void AssignValuesToThePool()
    {
        Debug.Log(dataArrowPool.listDataArrow.Length);
        foreach (var c in dataArrowPool.listDataArrow)
        {
            Queue<GameObject> allElementsSpawned = new Queue<GameObject>();
            for (int i = 0; i < c.spawnCount; i++)
            {
                var arrowData = Object.Instantiate(c.prefab);
                arrowData.SetActive(false);
                allElementsSpawned.Enqueue(arrowData);
            }
            _dictionary.Add(c.currentArrowType, allElementsSpawned);
        }
    }
    public void PutOutsideElementsBackToThePool()
    {
        if (_dictionaryElementsOutsidePool.Count == 0) return;
        //Se cogen los elementos por llave, se accede a su lista y luego con esa llav se meten al diccionario principal
        foreach (var keys in _dictionaryElementsOutsidePool.Keys)
        {
            var poolWhereIWillPutMyElementsOutside = _dictionary[keys];
            foreach (var value in _dictionaryElementsOutsidePool[keys])
            {
                value.SetActive(false);
                poolWhereIWillPutMyElementsOutside.Enqueue(value);
            }
        }
    }
    public void SpawnFromThePool(ArrowsTranslator.ArrowDirection arrowType, Vector2 position)
    {
        //Esto hace las llamadas correctamente, quizas el problema esta en las colas
        var elementSelected = GetElement(arrowType);
        
        elementSelected.SetActive(true);
        elementSelected.transform.position = position;
        
        PutElementToTheOutsideDictionary(elementSelected, arrowType);
    }

    private GameObject GetElement(ArrowsTranslator.ArrowDirection arrowType)
    {
        var queueSelected = _dictionary[arrowType];
        var elementItsNotActive = false;
        var elementSelect = queueSelected.Dequeue();
        while (!elementItsNotActive)
        {
            if (elementSelect.activeSelf == false) elementItsNotActive = true;
            else
            {
                elementSelect = queueSelected.Dequeue();
            }
        }

        return elementSelect;
    }
    private void PutElementToTheOutsideDictionary(GameObject valueDictionary, ArrowsTranslator.ArrowDirection keyDictionary)
    {
        //Se comprueba si esa llave esta siendo utilizada en el caso que se almacena en la cola correspondiente sino se
        //crea otra nueva y se almacena en el diccionario de los elementos de fuera
        if (_dictionaryElementsOutsidePool.ContainsKey(keyDictionary))
        {
            var queueWhereIWannaPutMyElement = _dictionaryElementsOutsidePool[keyDictionary];
            queueWhereIWannaPutMyElement.Enqueue(valueDictionary);
        }
        else
        {
            var newQueueType = new Queue<GameObject>();
            newQueueType.Enqueue(valueDictionary);
            _dictionaryElementsOutsidePool.Add(keyDictionary, newQueueType);
        }
    }
    public void ResetValues()
    {
        _dictionary = new Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>>();
        _dictionaryElementsOutsidePool = new Dictionary<ArrowsTranslator.ArrowDirection, Queue<GameObject>>();
        
        AssignValuesToThePool();
    }
}
[System.Serializable]
public struct ArrowsPathData
{
    public ArrowsTranslator.ArrowDirection currentArrowType;
    public int spawnCount;
    public GameObject prefab;
}

