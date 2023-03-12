using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [HideInInspector] public bool isDisabled = true;
    // Start is called before the first frame update
    void Start()
    {
        isDisabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisabled && Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isDisabled = false;
        }
        else if(!isDisabled && Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isDisabled = true;
        }
    }
}
