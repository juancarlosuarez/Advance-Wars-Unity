// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class MenuUnitsSelectorRefactor : MonoBehaviour, IChainedMenu
// {
//     
//     [SerializeField] private ListGameObjectsReference prefabsUnits;
//     
//     private List<ElementsUnitSelectorMenu> _allUnitsShowedAndHidden = new List<ElementsUnitSelectorMenu>();
//     private List<ElementsUnitSelectorMenu> _allUnitsCurrentShowed = new List<ElementsUnitSelectorMenu>();
//     private Dictionary<int, List<ElementsUnitSelectorMenu>> _elementsUnitsSplitInEachPlayer = new Dictionary<int, List<ElementsUnitSelectorMenu>>();  
//         
//     private List<Vector2> _positionElementShowed = new List<Vector2>();
//     [SerializeField] private ElementsUnitSelectorMenu prefab;
//
//     
//     [SerializeField] private SOListUnitsPrefabs dictionaryUnits;
//     private bool _isAlreadySpawnedMenu = false;
//  
//     private int _selectPositionList = 2;
//     private int _finalPositionList = 4;
//     private int _localPosition;
//     private int _initialPosition = 0;
//     public int InitialPositionList { get; set; }
//     public int SelectPositionList { get; set; }
//     public int FinalPositionList { get; set; }
//     public void HighLight(int countData, int countUI)
//     {
//     }
//
//     public void LowLight(int countData, int countUI)
//     {
//     }
//
//     public void Trigger()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public void MoveElementsInTheSelector()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public void DisplayElements()
//     {
//     }
//     public void CloseMenu()
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public int GetCountListElement()
//     {
//         throw new System.NotImplementedException();
//     }
//
//
//     private void InstantiateAllElements()
//     {
//         var dictionaryUnit = dictionaryUnits.GetDictionaryOfUnits();
//
//         for (int numberPlayer = 0; numberPlayer < 4; numberPlayer++)
//         {
//             var listLocal = new List<ElementsUnitSelectorMenu>();
//             for (int idUnit = 0; idUnit < GetCountListElement(); idUnit++)
//             {
//                 var listUnit = dictionaryUnit[(NameUnit)idUnit];
//                 var unit = listUnit[numberPlayer];
//
//                 var element = InstantiateElement(unit);
//                 listLocal.Add(element);
//                 
//                 if (idUnit != GetCountListElement()) continue;
//                 
//                 _elementsUnitsSplitInEachPlayer.Add(numberPlayer, listLocal);
//
//                 if (numberPlayer == 2) _allUnitsShowedAndHidden = _elementsUnitsSplitInEachPlayer[2];
//             }
//         }
//     }
//
//     private ElementsUnitSelectorMenu InstantiateElement(AbstractBaseUnit unit)
//     {
//         var unitDisplayed = Instantiate(prefab, transform.position, Quaternion.identity);
//         unitDisplayed.InitElementMenuUnit(unit);
//         unitDisplayed.transform.SetParent(transform);
//         unitDisplayed.gameObject.SetActive(false);
//
//         return unitDisplayed;
//     }
// }
