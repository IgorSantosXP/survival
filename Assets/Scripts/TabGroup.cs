using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<Tab> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public Tab selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(Tab tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<Tab>();
        }

        tabButtons.Add(tabButton);
    }

    public void OnTabEnter(Tab tabButton)
    {
        ResetTabs();
        if (selectedTab == null || tabButton != selectedTab) 
        {
            tabButton.background.color = tabHover;
        }
    }

    public void OnTabExit(Tab tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(Tab tabButton)
    {
        selectedTab = tabButton;
        ResetTabs();
        tabButton.background.color = tabActive;
        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (Tab tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab) { continue; }
            tabButton.background.color = tabIdle;
        }
    }
}
