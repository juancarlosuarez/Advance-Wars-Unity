using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandChangeTurn : ICommand
{
    public static event Action ChangeTurnUI;
    public void Execute()
    {
        ChangePlayer();
        ChangeMusic();
        var player = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentNewTileForCamera = WorldScriptableObjects.GetInstance()._currentMapDisplayData
            .idTileMainCityEachPlayer[player];
         var allTiles = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference;
         WorldScriptableObjects.GetInstance().tileSelected.reference = allTiles[currentNewTileForCamera];
        
        CommandQueue.GetInstance.AddCommand(new CommandUpdateInterfacesChangeTurn(), false);
        CommandQueue.GetInstance.AddCommand(new CommandPutAvailableAllSoldiers(player), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveSelectorImmediatly(currentNewTileForCamera), false);
        CommandQueue.GetInstance.AddCommand(new CommandMoveCameraInmediatly(currentNewTileForCamera), false);
        CommandQueue.GetInstance.AddCommand(new CommandUpdateR1L1Stats(), false);
        CommandQueue.GetInstance.AddCommand(new CommandAddLifeToUnitInBuilds(), false);
        
        ChangeTurnUI?.Invoke();
        
        //Update Interface
        
        //Notificar a la clase que se encarga de cambiar el HUID para cada jugador
        //Notificar a la clase que se encarga de gestionar la niebla de guerra hahaha xd
        //Cambiar la posicion de la camara para la base del jugador
        /*
         *Evento Cambio de turno
         *Interface Request Update
         *Stats Update
         * Interface Update Data
         * 
         */
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
    private void ChangePlayer()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer;
        if ((int)currentPlayer.reference == 2)
        {
            currentPlayer.reference = FactionUnit.Player1;
            return;
        }
        currentPlayer.reference++;
    }
    private void ChangeMusic()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        switch (currentPlayer)
        {
            case FactionUnit.Player1: SoundManager._sharedInstance.PlayMusic(MusicNames.Player1Song);
                break;
            case FactionUnit.Player2: SoundManager._sharedInstance.PlayMusic(MusicNames.Player2Song);
                break;
        }
    }
}
