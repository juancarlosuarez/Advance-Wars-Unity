using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HeadGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject _desactivable;

    [SerializeField] private InterfazTileSelected _interfazTileSelected;
    [SerializeField] private InterfazUnitSelected _interfazUnitSelected;
    [SerializeField] private InterfazBuildSelected _interfazBuildSelected;
    [SerializeField] private InterfazCalculateDamage _interfazCalculateDamage;
    
    private bool _isUnitInterfazAppear;
    public bool right;
    public void OpenInterfaceGamePlay()
    {
        _desactivable.SetActive(true);
        //_interfazTileSelected.ShowMenu();
        //_interfazUnitSelected.UpdateData();
    }

    public void CloseInterfaceGamePlay()
    {
        _desactivable.SetActive(false);
    }
    public void UpdateData()
    {
        _interfazTileSelected.UpdateData();
        _isUnitInterfazAppear = _interfazUnitSelected.UpdateData(WorldScriptableObjects.GetInstance().tileSelected.reference);
        _interfazBuildSelected.UpdateData();
        
        CalculatePositionBuildMenu();
    }
    
    private void UpdateDataCalculateDamage(int damage, int count)
    {
        var tileSelected = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference[count];
        
        _isUnitInterfazAppear = _interfazUnitSelected.UpdateData(tileSelected);
        _interfazBuildSelected.UpdateData();
        _interfazCalculateDamage.UpdateData(damage);
        
    }

    private void StopShowCalculateDamageInterfaz()
    {
        _interfazCalculateDamage.StopShow();
    }
    private void CalculatePositionBuildMenu()
    {
        var positionXUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.x;
        var positionYUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.y;
        var sizeCoverUnitImage = _interfazUnitSelected.coverImage.rectTransform.sizeDelta.x;
        if (_isUnitInterfazAppear)
        {
            _interfazBuildSelected.transform.position = GetPosition();
        }
        else
        {
            _interfazBuildSelected.transform.position = new Vector2(positionXUnitInterfaz - (sizeCoverUnitImage / 3 ), positionYUnitInterfaz);
        }
    }

    private Vector2 GetPosition()
    {
        var positionXUnitInterfaz = 0f;
        var positionYUnitInterfaz = 0f;
        if (right)
        {
            var sizeCoverUnitImage = _interfazUnitSelected.coverImage.rectTransform.sizeDelta.x;
            positionXUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.x - (sizeCoverUnitImage / 1.5f);
            positionYUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.y;
        }
        else
        {
            positionXUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.x;
            positionYUnitInterfaz = _interfazUnitSelected.gameObject.transform.position.y;
        }

        return new Vector2(positionXUnitInterfaz, positionYUnitInterfaz);
    }
    private void OnDisable()
    {
        ControllerObjectiveAttack.SendCurrentDamageSelect -= UpdateDataCalculateDamage;
        ControllerObjectiveAttack.StopCombat -= StopShowCalculateDamageInterfaz;
    }

    private void OnEnable()
    {
        ControllerObjectiveAttack.SendCurrentDamageSelect += UpdateDataCalculateDamage;
        ControllerObjectiveAttack.StopCombat += StopShowCalculateDamageInterfaz;
    }
}
