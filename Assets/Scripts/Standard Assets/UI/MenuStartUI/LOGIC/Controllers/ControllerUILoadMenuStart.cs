using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ControllerUILoadMenuStart : ControllerUIMenuStatic
{
    private DataUIMenuStart _currentDataUI;
        [SerializeField] private List<MapData> _mapDataList;

        private List<Vector2> _positionArrowHighLight;
        private Vector2 _positionMenu;
        private bool _firstRound;

        public override void HighLight(int count, int countUI)
        {
            _currentDataUI.arrowHighLight.transform.position = _positionArrowHighLight[countUI];
            if (!_firstRound) SoundManager._sharedInstance.PlayEffectSound(EffectNames.MoveBetweenMenus);
            else _firstRound = false;
        }
    
        public override void LowLight(int count, int countUI)
        {
            
        }

        public override void Trigger(int count)
        {
            //new GenerateMap(_mapDataList[count], count).BuildMap();
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.SelectOption);
            FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
            FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.EditMenuStart, 0, false);
            // var dataOptionStartJustText = _slotsUIData.GetData()[count];
            // dataOptionStartJustText.text = "Slot " + _mapDataList[count].mapName;
        }
    
        public override void DisplayMenu()
        {
            _firstRound = true;
            _currentDataUI = FactoryBuildersEachMenuGamePlayUI.sharedInstance.GetDataMenu(nameFrameGeneric);
            CalculatePositionArrow();
            FactoryBuildersEachMenuGamePlayUI.sharedInstance.ActiveUIMenu(nameFrameGeneric, _positionMenu);
            
        }
        
        public override void CloseMenuByTriggerButton()
        {
            _currentDataUI.arrowHighLight.SetActive(false);
            FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
        }
        public void CalculatePositionArrow()
        {
            var camera = GameObject.Find("Main Camera");
            var positionCamera = camera.transform.position;
            _positionMenu = new Vector2(positionCamera.x - 4, positionCamera.y);
            _currentDataUI.arrowHighLight.SetActive(true);
    
            _positionArrowHighLight = _currentDataUI.arrowPositions.Select(pos => 
                new Vector2(pos.x + _positionMenu.x, pos.y + _positionMenu.y)).ToList();
        }
        public override int GetCountList() => 3;
        public override bool CanSelectOption(int count) => true;
        public override void CloseMenuByPressB()
        {
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.Exit);
            _currentDataUI.arrowHighLight.SetActive(false);
            FactoryControllersMenuStartUI.sharedInstance.CloseCurrentMenu();
            FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.FileMenuStart, 0, false);
        }
}
