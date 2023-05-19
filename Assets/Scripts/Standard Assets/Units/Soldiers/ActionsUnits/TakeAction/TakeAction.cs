using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAction : ActionUnit
{
    private TileReference selectedTile;
    private ScriptableObjectPlayers currentPlayer;
    public override void Init()
    {
        selectedTile = Resources.Load<TileReference>("ScriptableObject/Data/DataSpecial/TilesData/SelectedTile");
        currentPlayer = Resources.Load<ScriptableObjectPlayers>("ScriptableObject/Data/DataSpecial/Player/CurrentPlayer");
    }
    public override void Trigger()
    {
        StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);
        
        var build = selectedTile.reference.occupiedBuild;

        if (build.playerThatCanControlThisUnit == FactionUnit.Neutral) 
        {
            TakeNeutralBuild();
            return;
        }
        if (build.playerThatCanControlThisUnit != currentPlayer.reference) print("Intento tomar ciudad enemiga");
    }
    private void TakeNeutralBuild()
    {
        new TakeNeutralBuild(selectedTile.reference.occupiedBuild, currentPlayer.reference);    }

}
    public class TakeNeutralBuild
    {
        private UnitReference currentUnitBeingControl;

        public TakeNeutralBuild(Build build, FactionUnit currentPlayer)
        {
            currentUnitBeingControl = Resources.Load<UnitReference>("ScriptableObject/Data/UnitData/CurrentUnitBeingControl");

        CanITakeTheBuild(build, currentPlayer);
        }
        private void CanITakeTheBuild(Build build, FactionUnit currentPlayer)
        {
            int lifeActualOfBuild = build.currentLifeUI - currentUnitBeingControl.reference.currentLifeUI;
            if (lifeActualOfBuild <= 0)
            {
                Debug.Log("tomo build");
                build.currentLifeUI = 20;
                new ChangePlayerWhoControlUnit(build, build.occupiedTile);
            //UnitSelectorSystem.sharedInstance.DeselectUnit();
            return;
            }
            build.currentLifeUI = build.currentLifeUI - currentUnitBeingControl.reference.currentLifeUI;

        }
    }
