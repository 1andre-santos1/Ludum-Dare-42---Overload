using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInfoManager : MonoBehaviour
{
    public GameObject[] StatsPanels;
    public GameObject[] StatsOutlines;

    public GameObject GetStatPanelActive()
    {
        foreach(GameObject panel in StatsPanels)
        {
            if (panel.activeInHierarchy)
                return panel;
        }
        return null;
    }

    public void CloseActiveStatPanel()
    {
        GameObject g = GetStatPanelActive();

        if(g != null)
        {
            g.SetActive(false);
            int index = GetIndexOf(g, StatsPanels);
            StatsOutlines[index].SetActive(false);
        }
    }
    public int GetIndexOf(GameObject obj,GameObject[] list)
    {
        for (int i = 0; i < list.Length; i++)
            if (list[i] == obj)
                return i;
        return -1;
    }
}
