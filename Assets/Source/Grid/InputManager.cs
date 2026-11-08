using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputAction update;
    [SerializeField]
    GridManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        update = InputSystem.actions.FindAction("Update");
    }

    // Update is called once per frame
    void Update()
    {
        if (update.WasPerformedThisFrame())
        {
            gm.Change();
        }
    }
}
