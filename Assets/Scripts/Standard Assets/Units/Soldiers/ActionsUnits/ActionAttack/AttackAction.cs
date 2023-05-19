using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//BORRAR ESTA MIERDA PLS
public class AttackAction : ActionUnit
{
    public static AttackAction sharedInstance;

    private PrepareValuesAttack prepareValueOfAttack;

    private Tile unitAttacker;
    private Tile unitDefenser;
    [Header("ScriptableObjects")]
    [SerializeField] BoolReference unitMustMove;
    [SerializeField] ListActionUnit currentActions;
    //[SerializeField] UIControllerData menuController;

    [FormerlySerializedAs("prefabMenuObjectiveAttack")] [SerializeField] ControllerObjectiveAttack prefabControllerObjectiveAttack;
    private void Awake()
    {
        sharedInstance = this;
        prepareValueOfAttack = GetComponent<PrepareValuesAttack>();
    }
    public override void Trigger()
    {
        ControllerObjectiveAttack controller =  Instantiate(prefabControllerObjectiveAttack);
    }
    public override void SharedVoid() 
    {
        MakeDamage();
    }
    private void MakeDamage()
    {
    }
    public void AttackSystem(Tile _unitAttacker, Tile _unitDefenser)
    {
        unitAttacker = _unitAttacker;
        unitDefenser = _unitDefenser;
        //Preparar los valores
        //Ejecutar el damage
        
    }
}
