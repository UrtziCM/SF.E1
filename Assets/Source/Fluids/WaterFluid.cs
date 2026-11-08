using UnityEngine;

[CreateAssetMenu(fileName = "WaterFluid", menuName = "Fluid/Water")]
public class WaterFluid : CellState
{
    public override void CellUpdate(GridManager gridManager, Vector2 pos)
    {
        if (fluidLevel > 1)
        {
            foreach (Cell neighbour in gridManager.GetVonNewmannNeighbours(pos))
            {
                if (neighbour == null)
                    continue;

                if (neighbour.state.fluidID == 2)
                {
                    gridManager.QueueChange(neighbour.pos, gridManager.Presets[3], 10);
                    continue;
                }

                if (neighbour.state.fluidID == 0)
                {

                    WaterFluid newState = CreateInstance<WaterFluid>();
                    newState.fluidID = fluidID;
                    newState.material = material;
                    newState.fluidLevel = fluidLevel - 1;
                    gridManager.QueueChange(neighbour.pos, newState, fluidLevel);
                }
            }
        }
    }

}
