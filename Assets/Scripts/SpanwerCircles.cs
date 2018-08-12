using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwerCircles : MonoBehaviour
{
    public GameObject PrefabCircle;
    public Sprite[] PersonSprites;
    public RectTransform Bar;

    public Transform SpawnPointsContainer;

    private List<GameObject> circlesList = new List<GameObject>();

    private CircleManager circleManager;

    private void Start()
    {
        circleManager = GameObject.FindObjectOfType<CircleManager>();
    }

    public void SpawnCircle(float posX,float posY,Transform SpawnPoint)
    {
        if (!circleManager.isGameActive)
            return;

        GameObject circle = Instantiate(PrefabCircle,new Vector3(posX,posY,0f),Quaternion.identity,SpawnPoint);
        circle.GetComponent<SpriteRenderer>().sprite = PersonSprites[Random.Range(0, PersonSprites.Length)];

        circlesList.Add(circle);

        circleManager.NumberOfCircles++;

        UpdateBar();
    }

    public void UpdateBar()
    {
        Bar.sizeDelta = new Vector2((100f * circleManager.NumberOfCircles) / circleManager.LimitOfCircles, 69f);

        if (Bar.sizeDelta.x >= 100f)
            circleManager.LoseGame();
    }

    IEnumerator SpawningEvent(float SpawnDelay)
    {
        yield return new WaitForSeconds(SpawnDelay);

        float posX = float.NaN,posY = float.NaN;

        foreach(Transform SpawnPoint in SpawnPointsContainer)
        {
            if (SpawnPoint.childCount > 0)
                continue;

            posX = SpawnPoint.transform.position.x;
            posY = SpawnPoint.transform.position.y;
            SpawnCircle(posX, posY,SpawnPoint);

            StartCoroutine("SpawningEvent", SpawnDelay);
            break;
        }
        if (posX == float.NaN && posY == float.NaN)
        {
            Debug.Log("Full World");
        }
    }
}
