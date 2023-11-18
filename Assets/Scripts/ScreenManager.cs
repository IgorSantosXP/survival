using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryScreen;

    public bool isInventoryOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        DisableScreens();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isInventoryOpen)
            {
                InventoryScreen.SetActive(true);
                isInventoryOpen = true;
            } else
            {
                InventoryScreen.SetActive(false);
                isInventoryOpen = false;
            }
            
        }
    }

    private void DisableScreens()
    {
        InventoryScreen.SetActive(false);
    }
}
