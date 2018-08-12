using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightPanel : MonoBehaviour
{
    public void FinishDay()
    {
        GameObject.FindObjectOfType<CircleManager>().Day++;
        GameObject.FindObjectOfType<CircleManager>().UpdateDaysText();
    }
}
