using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int PlayerCoins = 50;

    public int CoinsIncreaseDelay = 3;


    public int RainCost = 50;
    public int EarthquakeCost = 100;

    public Text[] RainCostText;
    public Text[] EarthquakeCostText;

    public Text CoinText;

    

    void Start()
    {
        PlayerCoins = 0;
        UpdateCoinText();
        StopCoroutine("IncreaseCoins");

        StartCoroutine("IncreaseCoins");

        foreach (Text t in RainCostText)
            t.text = "" + RainCost;
        foreach (Text t in EarthquakeCostText)
            t.text = "" + EarthquakeCost;
    }

    IEnumerator IncreaseCoins()
    {
        yield return new WaitForSeconds(CoinsIncreaseDelay);

        if(GameObject.FindObjectOfType<CircleManager>().isGameActive)
        {
            PlayerCoins++;
            UpdateCoinText();
        }

        StartCoroutine("IncreaseCoins");
    }

    public void TryToBuyRainAction(Area area)
    {
        if (RainCost > PlayerCoins)
            return;

        area.MakeItRain();
        PlayerCoins -= RainCost;
        UpdateCoinText();
    }

    public void TryToBuyEarthquakeAction(Area area)
    {
        if (EarthquakeCost > PlayerCoins)
            return;

        area.ShakeItOff();
        PlayerCoins -= EarthquakeCost;
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        CoinText.text = "" + PlayerCoins;
    }
}
