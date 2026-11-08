using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public struct CellChange
{
    public Vector2 pos;
    public CellState nextState;
    public int priority;
}

public class GridManager : MonoBehaviour
{
    public List<CellState> Presets;
    int itCount = 0;
    float iterationTimeStart = 0f;

    private Cell[] cells;
    private List<CellChange> changes = new();
    private int size;
    public void Initialize(Cell[] cells, int size)
    {
        this.cells = cells;
        this.size = size; // Podria ser sqrt de cells.size pero es mejor así
        foreach (Cell cell in cells)
        {
            if (UnityEngine.Random.value > .75)
                cell.SetState(Presets[UnityEngine.Random.Range(1,3)]);

        }
        iterationTimeStart = Time.realtimeSinceStartup;
    }

    public Cell GetCellAt(int x, int y)
    {
        if (x < 0 || x > size)
            return null;
        if (y < 0 || y > size)
            return null;


        int index = y * size + x;
        if (index >= size * size)
            return null;
        return cells[index];
    }

    public Cell GetCellAt(Vector2 pos)
    {
        return GetCellAt((int)pos.x, (int)pos.y);
    }

    public void SetCellAt(int x, int y, CellState newState)
    {
        SetCellAt(new Vector2(x, y), newState);
    }

    public void SetCellAt(Vector2 pos, CellState newState)
    {
        int index = ((int)pos.y) * size + ((int)pos.x);
        cells[index].SetState(newState);
    }

    public Cell[] GetVonNewmannNeighbours(Vector2 pos)
    {
        Cell[] neighbours = new Cell[4];
        neighbours[0] = GetCellAt(pos + Vector2.left);
        neighbours[1] = GetCellAt(pos + Vector2.up);
        neighbours[2] = GetCellAt(pos + Vector2.right);
        neighbours[3] = GetCellAt(pos + Vector2.down);
        return neighbours;
    }

    public void QueueChange(Vector2 pos, CellState nextState, int priority)
    {
        changes.Add(new CellChange() { pos = pos, nextState = nextState, priority = priority });
    }

    private void OrderChanges()
    {
        changes.Sort((cc1, cc2) => { return cc1.priority.CompareTo(cc2.priority); });
    }

    public void Change() // Aqui los cambios vienen ya calculados, se aplican en orden de prioridad donde los más pequeños van primero.
    {
        OrderChanges();
        foreach (CellChange change in changes)
        {
            ApplyChange(change);
        }
        changes.Clear();
        foreach ( Cell cell in cells)
        {
            cell.CellUpdate();
        }
    }



    private void ApplyChange(CellChange change)
    {
        SetCellAt(change.pos, change.nextState);
    }

    private void Update()
    {
        Change();
        itCount++;
        if (itCount == 16)
            Debug.Log("Time to do 16 iterations: " + (Time.realtimeSinceStartup - iterationTimeStart) + "s");
    }
}
