using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour
{
    public float DelayCircleSpawning = 10f;

    public int Day = 1;
    public int ScoreIncrement = 100;

    public GameObject DaysText;

    public int NumberOfCircles = 0;
    public int LimitOfCircles = 10;
    public bool isGameActive = false;

    public float UpdateAreasDelay = 2f;

    public GameObject LosePanel;

    private LevelManager levelManager;
    private SpanwerCircles spawnerCircles;

    private void Start()
    {
        LosePanel.SetActive(false);
        Area[] areas = GameObject.FindObjectsOfType<Area>();

        foreach (Area area in areas)
            area.InitiateArea();
    }
    public void StartGame()
    {
        isGameActive = true;

        levelManager = GameObject.FindObjectOfType<LevelManager>();
        spawnerCircles = GameObject.FindObjectOfType<SpanwerCircles>();

        GameObject.FindObjectOfType<DaylightPanel>().GetComponent<Animator>().enabled = true;

        GameObject.FindObjectOfType<EarthInfoManager>().UpdateEarthInfo();
        GameObject.Find("PanelEarthInfo").GetComponent<Animator>().enabled = true;
        GameObject.Find("PanelEarthInfo").GetComponent<Animator>().Play("EarthInfo_Appearing");


        StartCoroutine("SpawnHumansOnAreas");
    }

    IEnumerator SpawnHumansOnAreas()
    {
        yield return new WaitForSeconds(DelayCircleSpawning);

        if(isGameActive)
        {
            Area[] areas = GameObject.FindObjectsOfType<Area>();
            foreach (Area area in areas)
                area.TryToSpawnHuman();
        }
        
        StartCoroutine("SpawnHumansOnAreas");
    }

    public void LoseGame()
    {
        isGameActive = false;
        StopAllCoroutines();

        LosePanel.SetActive(true);
        LosePanel.transform.GetChild(1).GetComponent<Text>().text = "Mother Earth survived " + Day + " days";
        if(GameObject.FindObjectOfType<RecordManager>().RecordDays < Day)
        {
            LosePanel.transform.GetChild(2).GetComponent<Text>().text = "New Record!";
            GameObject.FindObjectOfType<RecordManager>().RecordDays = Day;
        }
        else
        {
            LosePanel.transform.GetChild(2).GetComponent<Text>().text = "Record: "+ GameObject.FindObjectOfType<RecordManager>().RecordDays;
        }
    }

    public void UpdateDaysText()
    {
        DaysText.GetComponent<Text>().text = "Day " + Day;

        UpdateAreasResources();
        GameObject.FindObjectOfType<EarthInfoManager>().UpdateEarthInfo();

        GameObject.Find("PanelEarthInfo").GetComponent<Animator>().Play("EarthInfo_Appearing",-1,0);
    }

    public void UpdateAreasResources()
    {
        Area[] areas = GameObject.FindObjectsOfType<Area>();
        foreach (Area area in areas)
            area.UpdateResources();
    }

    public void PauseGame()
    {
        GameObject.FindObjectOfType<DaylightPanel>().GetComponent<Animator>().speed = 0f;
        isGameActive = false;
    }
    public void ResumeGame()
    {
        GameObject.FindObjectOfType<DaylightPanel>().GetComponent<Animator>().speed = 1f;
        isGameActive = true;
    }
    public void StopCoroutines()
    {
        StopAllCoroutines();
    }
}
