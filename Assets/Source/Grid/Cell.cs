using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public CellState state;
    GridManager gridManager;
    public Vector2 pos;
    private Renderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }
    public void SetGridManager(GridManager gm)
    {
        gridManager = gm;
    }

    public void SetState(CellState newState)
    {
        state = newState;
        RenderState();
    }

    private void RenderState()
    {
        meshRenderer.material = state.material;
        transform.position = new Vector3(pos.x, state.fluidLevel*.1f, pos.y); // esto es para que pueda distinguir el fluidLevel de cada cosa, deja un efecto gracioso pero poco mas
    }
    public void CellUpdate()
    {
        state.CellUpdate(gridManager, pos);
        
    }


}
