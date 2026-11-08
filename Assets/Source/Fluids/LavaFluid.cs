using UnityEngine;

[CreateAssetMenu(fileName = "LavaFluid", menuName = "Fluid/Lava")]
public class LavaFluid : CellState
{
    public override void CellUpdate(GridManager gridManager, Vector2 pos)
    {
        if (fluidLevel > 1)
        {
            foreach (Cell neighbour in gridManager.GetVonNewmannNeighbours(pos))
            {
                if (neighbour == null)
                    continue;

                if (neighbour.state.fluidID == 0)
                {
                    LavaFluid newState = CreateInstance<LavaFluid>();
                    newState.fluidID = fluidID;
                    newState.material = material;
                    newState.fluidLevel = fluidLevel - 1;
                    gridManager.QueueChange(neighbour.pos, newState, fluidLevel);
                }
            }
        }
    }
}
