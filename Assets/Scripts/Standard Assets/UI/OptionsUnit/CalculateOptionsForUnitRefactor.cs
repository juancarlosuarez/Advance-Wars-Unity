using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateOptionsForUnitRefactor
{
    private List<IOptionsUnitConditions> _conditionsForEachAction;
    private SOListInt _listActionID;
    public CalculateOptionsForUnitRefactor()
    {
        _listActionID = Resources.Load<SOListInt>("ScriptableObject/Data/ListIntData/OptionsUnit/ListActionsID");
    }
    public void CalculateOptions()
    {
        //Aqui busca todos las clases que contengan la interfaz "IOptionsUnitConditions"
        var typeIOptionUnit = typeof(IOptionsUnitConditions);
        var allTypesOptionsUnits = AppDomain.CurrentDomain.GetAssemblies().SelectMany(pe => pe.GetTypes())
            .Where(t => typeIOptionUnit.IsAssignableFrom(t) && t.IsClass);

        _conditionsForEachAction = new List<IOptionsUnitConditions>();

        //Aqui se recorren los tipos y se crea la instancia, pero no de la clase concreta, si no de la abstraccion
        foreach (var eachOption in allTypesOptionsUnits)
        {
            _conditionsForEachAction.Add(Activator.CreateInstance(eachOption) as IOptionsUnitConditions);
        }
        //Ya luego se comprueba todas las opciones y se seleccionan las que devuelva true y obtenemos la opcion asociada,
        //lo cual nos servira para que la UI sepa que datos coger
        _listActionID.reference = _conditionsForEachAction.Where(c => c.DoesOptionMeetCondition())
            .Select(c => (int)c.GetOptionAssociated()).ToList();
        _listActionID.reference.OrderByDescending(c => c);
    }
}
    public enum OptionsUnit
    {
        Attack = 0, Move = 1, Cancel = 2, Wait = 8, Load = 4, Drop = 5, Take = 3
    }
    public interface IOptionsUnitConditions
    {
        public OptionsUnit GetOptionAssociated();
        public bool DoesOptionMeetCondition();
    }
