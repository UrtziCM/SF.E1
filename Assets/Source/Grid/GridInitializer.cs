using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject cell;
    [SerializeField]
    int size;
    int SqrSize => size * size;

    [SerializeField]
    CellState initialState;


    GridManager gridManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = GetComponent<GridManager>();

        float initializationStartTime = Time.realtimeSinceStartup;
        Cell[] cells = new Cell[SqrSize];
        for (int i = 0,x,y; i < SqrSize; i++)
        {
            x = i % (size);
            y = i / (size);
            GameObject currentGameobject = Instantiate(cell, new Vector3(x, 0, y), Quaternion.identity);
            cells[i] = currentGameobject.GetComponent<Cell>();
            cells[i].SetGridManager(gridManager);
            cells[i].pos = new Vector2(x, y);
            cells[i].state = initialState;
        }
        gridManager.Initialize(cells, size);
        Debug.Log("Time to initialize board: " + (Time.realtimeSinceStartup -  initializationStartTime) + "s");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
