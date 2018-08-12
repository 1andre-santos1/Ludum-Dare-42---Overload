using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject PrefabCloud;
    public int LimitOfClouds = 3;
    public int DelayCloudSpawning = 5;

    private void Start()
    {
        StartCoroutine("SpawnCloudEvent");
    }
    public int HowManyClouds()
    {
        return FindObjectsOfType<Cloud>().Length;
    }

    IEnumerator SpawnCloudEvent()
    {
        yield return new WaitForSeconds(DelayCloudSpawning);
        if(HowManyClouds() < LimitOfClouds)
        {
            float posX = transform.position.x;
            float posY = transform.position.y + Random.Range(-2f,2f);

            GameObject instance = Instantiate(PrefabCloud, new Vector3(posX, posY, 0f), Quaternion.identity, transform);
        }
        StartCoroutine("SpawnCloudEvent");
    }
}
