using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryScreen;
    [SerializeField] private Tooltip tooltip;

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
                tooltip.HideTooltip();
            }
            
        }
    }

    private void DisableScreens()
    {
        InventoryScreen.SetActive(false);
    }
}
