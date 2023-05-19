using System.Collections;
using System.Collections.Generic;
using Extensibles;
using UnityEngine;

public class InterfaceUIGamePlay : MonoBehaviour
{
    [SerializeField] SetSpritesCanvas spriteIcon;
    [SerializeField] SetSpritesCanvas spriteMenu;
    [SerializeField] NumberSet goldAmmount;

    [SerializeField] private GameObject _desactivable;

    private void OnDisable()
    {
        Reset();
    }
    private void OnEnable()
    {
        PlayerController.ResetEventsNoMonobehavior += Reset;

        //TurnsManager.UpdateInterfacesByChangeTurn += UpdateInterface;

        CommandUpdateInterfaceGamePlay.UpdateInterfaceGamePlay += UpdateInterface;
    }
    private void Reset()
    {

        PlayerController.ResetEventsNoMonobehavior -= Reset;

        //TurnsManager.UpdateInterfacesByChangeTurn -= UpdateInterface;
        
        CommandUpdateInterfaceGamePlay.UpdateInterfaceGamePlay -= UpdateInterface;
    }

    private void UpdateInterface()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentStats = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(currentPlayer);
        
        ChangeNumberGold(currentStats);
        ChangeSprite(currentStats);
        ChangeCoverSprite(currentStats);
    }

    private void DissapearMenu()
    {
        
        if (_desactivable.activeInHierarchy)
        {
            _desactivable.SetActive(false);
            PlayerController.sharedInstance.ChangeControlToEditMap();
            return;
        }
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        _desactivable.SetActive(true);
    }
    private void ChangeNumberGold(DataStats currentPlayerStats) => goldAmmount.UpdateData(currentPlayerStats.GoldAmount());
    private void ChangeSprite(DataStats currentPlayerStats) => spriteIcon.UpdateData(currentPlayerStats.GetSpritePlayer());

    private void ChangeCoverSprite(DataStats currentPlayerStats) => spriteMenu.UpdateData(currentPlayerStats.GetCoverPlayer());
}
