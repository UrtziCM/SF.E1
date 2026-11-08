using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[CreateAssetMenu(fileName = "Fluid", menuName = "Fluid")]
public class CellState : ScriptableObject
{
    public int fluidID;
    public int fluidLevel;
    public Material material;

    public static CellState Create(int fluidID, int fluidLevel )
    {
        CellState cellState = ScriptableObject.CreateInstance<CellState>();
        cellState.fluidID = fluidID;
        cellState.fluidLevel = fluidLevel;
        return cellState;
    }

    public CellState Clone()
    {
        CellState cellState = ScriptableObject.CreateInstance<CellState>();
        cellState.fluidID = fluidID;
        cellState.fluidLevel = fluidLevel;
        cellState.material = material;

        return cellState;
    }

    public virtual void CellUpdate(GridManager gridManager, Vector2 pos) { }

}
