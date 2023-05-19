using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerIfUnitIsAllyOrEnemy
{
    private ScriptableObjectPlayers currentPlayer;
    public CheckerIfUnitIsAllyOrEnemy()
    {
        currentPlayer = Resources.Load<ScriptableObjectPlayers>("ScriptableObject/Data/DataSpecial/Player/CurrentPlayer");
    }
    public bool System(AbstractBaseUnit unitToCheck)
    {
        if (unitToCheck.playerThatCanControlThisUnit == currentPlayer.reference)
        {
            Debug.Log("Pertenezco al jugador correspondiente");
            return true;
        }
        Debug.Log("Soy un enemigo");
        return false;
    }
}
//BORRRAR ESTA MIERDA 