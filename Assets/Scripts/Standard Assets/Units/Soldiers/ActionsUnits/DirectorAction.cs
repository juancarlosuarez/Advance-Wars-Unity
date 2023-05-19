using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorAction
{
    
        private ActionUnit currentActionSelected;
        private ActionReference moveAction;
        private BoolReference unitWillMove;
        
        public DirectorAction(ActionUnit _currentActionSelected)
        {
            currentActionSelected = _currentActionSelected;
            moveAction = Resources.Load<ActionReference>("ScriptableObject/Data/DataSpecial/ListCurrentAcionUnit/MoveAction");
            unitWillMove = Resources.Load<BoolReference>("ScriptableObject/Data/BoolData/WillUnitMove");
        
        
            System();
        }
        private void System()
        {
            bool unitWillMoveToCompleteHisAction = currentActionSelected.thisActionMaybeRequiresMovement && unitWillMove.boolReference;
            unitWillMove.boolReference = unitWillMoveToCompleteHisAction;
        
            if (unitWillMoveToCompleteHisAction) moveAction.reference.Trigger();
            else
            {
                currentActionSelected.Init();
                currentActionSelected.Trigger();
            }
        
        
        }
    
}
