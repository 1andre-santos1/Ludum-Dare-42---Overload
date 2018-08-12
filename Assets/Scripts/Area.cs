using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Area : MonoBehaviour
{
    public string Name = "Unknown";

    //all in 0-100 rating
    public int Food = 50;
    public int Water = 50;
    public int People = 0;
    public int HumanDevelopment = 0;
    public int NatureDevelopment = 50;

    public int BirthRate = 50;
    public int DeathRate = 50;

    public int ProbabilityOfImmigrantsSentAway = 0;
    public GameObject BoatPrefab;

    public float FoodDecreasePerPerson = 0.2f;
    public float FoodIncreasePerPerson = 0.1f;

    public float WaterDecreasePerPerson = 0.2f;
    public float WaterIncreasePerPerson = 0.1f;

    public float RainEffect = 0.4f;
    public GameObject RainParticle;

    public float EarthquakeEffect = 0.4f;

    public int NatureIncreaseAmount = 5;
    public int UrbanIncreaseAmount = 5;
    public int NatureDecreaseAmount = 10;
    public int UrbanDecreaseAmount = 10;

    public float EffectOfUrbanOnResources = 0.1f;
    public float EffectOfNatureOnResources = 0.3f;

    private float NatureIncreaseProbability;
    private float UrbanIncreaseProbability;

    public GameObject PrefabHuman;
    public Sprite[] SpriteHuman;

    public GameObject PrefabBuilding;
    public Sprite[] SpriteBuilding;

    public GameObject PrefabTree;
    public Sprite[] SpriteTree;

    public GameObject StatPanel;
    public Text StatFood;
    public Text StatWater;
    public Text StatNature;
    public Text StatUrban;
    public Text StatPeople;

    private GameObject outline;
    public Transform BuildingsContainer;
    public Transform HumansContainer;
    public Transform TreesContainer;

    private void Start()
    {
        outline = transform.GetChild(0).gameObject;


        StatPanel.SetActive(false);
        outline.SetActive(false);
    }
    public void MakeItRain()
    {
        Transform RainSpawn = transform.GetChild(5);

        float posX = RainSpawn.position.x;
        float posY = RainSpawn.position.y;

        GameObject instance = Instantiate(RainParticle, new Vector3(posX, posY, 0f), Quaternion.identity, RainSpawn);
        Destroy(instance, 5f);

        if(Food > 0)
            Food += Mathf.FloorToInt(Food * RainEffect);
        else
            Food += Mathf.FloorToInt(10 * RainEffect);
        if (Food > 100)
            Food = 100;

        if(Water > 0)
            Water += Mathf.FloorToInt(Water * RainEffect * 2);
        else
            Water += Mathf.FloorToInt(10 * RainEffect * 2);

        if (Water > 100)
            Water = 100;

        if(NatureDevelopment > 0)
            NatureDevelopment += Mathf.FloorToInt(NatureDevelopment * RainEffect);
        else
            NatureDevelopment += Mathf.FloorToInt(10 * RainEffect);
        if (NatureDevelopment > 100)
            NatureDevelopment = 100;
    }

    public void ShakeItOff()
    {
        GameObject.FindObjectOfType<MainCamera>().GetComponent<Animator>().Play("Earthquake",-1,0);

        HumanDevelopment -= Mathf.FloorToInt(HumanDevelopment * EarthquakeEffect);
        if (HumanDevelopment < 0)
            HumanDevelopment = 0;

        People -= Mathf.FloorToInt(People * EarthquakeEffect);
        if (People < 0)
            People = 0;
    }

    public void UpdateStats()
    {
        UpdateText(Food, StatFood,false);
        UpdateText(Water, StatWater,false);
        UpdateText(NatureDevelopment, StatNature,false);
        UpdateText(HumanDevelopment, StatUrban,true);
        UpdateText(People, StatPeople,true);
    }
    public void UpdateText(int percentage, Text text,bool inverse)
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
        else if(percentage > 0)
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
    public void UpdateResources()
    {
        UrbanIncreaseProbability = People / 100f;
        NatureIncreaseProbability = 1 - UrbanIncreaseProbability;

        float rnd = Random.Range(0f, 1f);
        if (rnd < UrbanIncreaseProbability)
        {
            HumanDevelopment += UrbanIncreaseAmount;
            if (HumanDevelopment > 100)
                HumanDevelopment = 100;

            NatureDevelopment -= NatureDecreaseAmount;
            if (NatureDevelopment < 0)
                NatureDevelopment = 0;
        }
        else
        {
            NatureDevelopment += NatureIncreaseAmount;
            if (NatureDevelopment > 100)
                NatureDevelopment = 100;

            HumanDevelopment -= UrbanDecreaseAmount;
            if (HumanDevelopment < 0)
                HumanDevelopment = 0;
        }

        //update buildings
        int numBuildings = (HumanDevelopment > 80) ? 5 : (HumanDevelopment > 60) ? 4 : (HumanDevelopment > 40) ? 3 : (HumanDevelopment > 20) ? 2 : (HumanDevelopment > 0) ? 1 : 0;
        int count = 0;
        foreach(Transform building in BuildingsContainer)
        {
            bool buildingAlreadyExists = building.childCount > 0;
            if (buildingAlreadyExists && count < numBuildings)
            {
                count++;
                continue;
            }
            else if(buildingAlreadyExists && count >= numBuildings)
            {
                count++;
                Destroy(building.GetChild(0).gameObject);
                continue;
            }
            else if(!buildingAlreadyExists && count < numBuildings)
            {
                count++;
                float posX = building.transform.position.x;
                float posY = building.transform.position.y;

                GameObject instance = Instantiate(PrefabBuilding, new Vector3(posX, posY, 0f), Quaternion.identity, building);
                instance.GetComponent<SpriteRenderer>().sprite = SpriteBuilding[Random.Range(0, SpriteBuilding.Length)];
            }
        }

        //update trees
        int numTrees = (NatureDevelopment > 80) ? 5 : (NatureDevelopment > 60) ? 4 : (NatureDevelopment > 40) ? 3 : (NatureDevelopment > 20) ? 2 : (NatureDevelopment > 0) ? 1 : 0;
        count = 0;
        foreach (Transform tree in TreesContainer)
        {
            bool treeAlreadyExists = tree.childCount > 0;
            if (treeAlreadyExists && count < numTrees)
            {
                count++;
                continue;
            }
            else if (treeAlreadyExists && count >= numTrees)
            {
                count++;
                Destroy(tree.GetChild(0).gameObject);
                continue;
            }
            else if (!treeAlreadyExists && count < numTrees)
            {
                count++;
                float posX = tree.transform.position.x;
                float posY = tree.transform.position.y;

                GameObject instance = Instantiate(PrefabTree, new Vector3(posX, posY, 0f), Quaternion.identity, tree);
                instance.GetComponent<SpriteRenderer>().sprite = SpriteBuilding[Random.Range(0, SpriteBuilding.Length)];
            }
        }


        //how much was produced and consumed per people
        int foodIncrease = Mathf.FloorToInt(FoodIncreasePerPerson * People);
        int foodDecrease = Mathf.FloorToInt(FoodDecreasePerPerson * People);

        Food += foodIncrease;
        Food -= foodDecrease;

        //how much the surroundings have an impact on natural resources
        foodIncrease = Mathf.CeilToInt(EffectOfNatureOnResources * NatureDevelopment * Food) / 100;
        foodDecrease = Mathf.CeilToInt(EffectOfUrbanOnResources * HumanDevelopment * Food) / 100;
        Food -= foodDecrease;
        Food += foodIncrease;

        if (Food > 100)
            Food = 100;
        else if (Food < 0)
            Food = 0;

        int waterIncrease = Mathf.FloorToInt(WaterIncreasePerPerson * People);
        int waterDecrease = Mathf.FloorToInt(WaterDecreasePerPerson * People);

        Water += waterIncrease;
        Water -= waterDecrease;

        waterIncrease = Mathf.CeilToInt(EffectOfNatureOnResources * NatureDevelopment * Water) / 100;
        waterDecrease = Mathf.CeilToInt(EffectOfUrbanOnResources * HumanDevelopment * Water) / 100;
        Water -= waterDecrease;
        Water += waterIncrease;

        if (Water > 100)
            Water = 100;
        else if (Water < 0)
            Water = 0;

        ProbabilityOfImmigrantsSentAway = 100 - Mathf.FloorToInt((Water + Food) / 2);

        rnd = Random.Range(0, 101);
        if (rnd < ProbabilityOfImmigrantsSentAway)
            SendImmigrantsAway();

        UpdateStats();
    }

    public void SendImmigrantsAway()
    {
        if (People <= 0)
            return;

        Transform Destination = null;
        Area[] areas = GameObject.FindObjectsOfType<Area>();
        foreach(Area area in areas)
        {
            if (area == this)
                continue;

            int amountOfResources = (area.Water + area.Food) / 2;
            if(amountOfResources > ((Water + Food) / 2))
            {
                Destination = area.transform.GetChild(4);
                break;
            }
        }

        if (Destination == null)
            return;

        Transform Port = transform.GetChild(4);
        float posX = Port.transform.position.x;
        float posY = Port.transform.position.y;

        GameObject instance = Instantiate(BoatPrefab, new Vector3(posX, posY, 0f), Quaternion.identity, Port);
        instance.GetComponent<Boat>().Destination = Destination;
        People -= instance.GetComponent<Boat>().AmountOfPeopleCarried;
    }
    private void OnMouseDown()
    {
        UpdateStats();

        FindObjectOfType<AreaInfoManager>().CloseActiveStatPanel();
        FindObjectOfType<SoundManager>().PlayButtonClickSound();

        StatPanel.SetActive(true);
        outline.SetActive(true);
    }
    public void InitiateArea()
    {
        InstantiateObjectsOnArea(BuildingsContainer, Mathf.CeilToInt((HumanDevelopment * 5f) / 81f), PrefabBuilding, SpriteBuilding);
        InstantiateObjectsOnArea(TreesContainer, Mathf.CeilToInt((NatureDevelopment * 5f) / 81f), PrefabTree, SpriteTree);

        UpdateStats();
    }
    public void TryToSpawnHuman()
    {
        int rnd = Random.Range(0, 101);
        if (rnd <= People)
        {
            InstantiateObjectsOnArea(HumansContainer, 1, PrefabHuman, SpriteHuman);
        }
    }
    private void InstantiateObjectsOnArea(Transform Container, int NumberOfSpawns, GameObject PrefabToSpawn, Sprite[] ObjectSprites)
    {
        int count = 0;

        foreach (Transform SpawnPoint in Container)
        {
            if (SpawnPoint.childCount > 0)
                continue;

            if (count >= NumberOfSpawns)
                break;

            float posX = SpawnPoint.transform.position.x;
            float posY = SpawnPoint.transform.position.y;

            GameObject instance = Instantiate(PrefabToSpawn, new Vector3(posX, posY, 0f), Quaternion.identity, SpawnPoint);
            instance.GetComponent<SpriteRenderer>().sprite = ObjectSprites[Random.Range(0, ObjectSprites.Length)];

            count++;
        }
    }
}
