using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandDrop : ICommand
{
    private TransportSoldier _transport;
    private TileRefactor _tileWhereGoUnit;

    public CommandDrop(Soldier transport, TileRefactor tileWhereGoUnit)
    {
        _transport = transport as TransportSoldier;
        _tileWhereGoUnit = tileWhereGoUnit;
    }
    public void Execute()
    {
        _transport.DropUnit(_tileWhereGoUnit);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
