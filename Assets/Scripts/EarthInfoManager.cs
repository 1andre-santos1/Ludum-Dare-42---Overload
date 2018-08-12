using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthInfoManager : MonoBehaviour
{
    public Text StatFood;
    public Text StatWater;
    public Text StatUrban;
    public Text StatNature;
    public Text StatOverall;

    private Area[] areas;

    public void UpdateEarthInfo()
    {
        areas = GameObject.FindObjectsOfType<Area>();

        int overallFood = 0;
        int overallWater = 0;
        int overallUrban = 0;
        int overallNature = 0;
        int overallState = 0;
        int overallPeople = 0;

        foreach(Area area in areas)
        {
            overallFood += area.Food;
            overallWater += area.Water;
            overallUrban += area.HumanDevelopment;
            overallNature += area.NatureDevelopment;
            overallPeople += area.People;
        }

        overallFood /= areas.Length;
        overallWater /= areas.Length;
        overallUrban /= areas.Length;
        overallNature /= areas.Length;
        overallState = (overallFood + overallWater) / 2;
        overallPeople /= areas.Length;

        if (overallFood <= 0 || overallWater <= 0 || overallPeople <= 0)
            GameObject.FindObjectOfType<CircleManager>().LoseGame();

        UpdateText(overallFood,StatFood,false);
        UpdateText(overallWater, StatWater,false);
        UpdateText(overallUrban, StatUrban,true);
        UpdateText(overallNature, StatNature,false);
        UpdateText(overallState, StatOverall,false);

    }

    public void UpdateText(int percentage, Text text, bool inverse)
    {
        if (percentage > 80)
        {
            text.text = "Very High";
            text.color = (inverse) ? new Color(255f, 0f, 0f) : new Color(0f, 255f, 0f);
        }
        else if (percentage > 60)
        {
            text.text = "High";
            text.color = (inverse) ? new Color(255f, 100f, 0f) : new Color(100f, 255f, 0f);
        }
        else if (percentage > 40)
        {
            text.text = "Average";
            text.color = new Color(255f, 255f, 0f);
        }
        else if (percentage > 20)
        {
            text.text = "Low";
            text.color = (!inverse) ? new Color(255f, 100f, 0f) : new Color(100f, 255f, 0f);
        }
        else if (percentage > 0)
        {
            text.text = "Very Low";
            text.color = (!inverse) ? new Color(255f, 0f, 0f) : new Color(0f, 255f, 0f);
        }
        else
        {
            text.text = "Empty";
            text.color = (!inverse) ? new Color(255f, 0f, 0f) : new Color(0f, 255f, 0f);
        }
    }
}
