using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject ParticleDissapearing;
    public int CoinsWonOnDestroy = 5;
    public int TimeOnAreaToIncreasePeople = 3;

    public AudioClip Appearing;

    private void Start()
    {
        StartCoroutine("IncreaseAreaPeople");
        GetComponent<AudioSource>().clip = Appearing;
        GetComponent<AudioSource>().Play();
    }

    IEnumerator IncreaseAreaPeople()
    {
        yield return new WaitForSeconds(1f);

        transform.parent.transform.parent.transform.parent.GetComponent<Area>().People += TimeOnAreaToIncreasePeople;
        if (transform.parent.transform.parent.transform.parent.GetComponent<Area>().People > 100)
            transform.parent.transform.parent.transform.parent.GetComponent<Area>().People = 100;
        transform.parent.transform.parent.transform.parent.GetComponent<Area>().UpdateStats();

        StartCoroutine("IncreaseAreaPeople");
    }

    private void OnMouseDown()
    {
        StopCoroutine("IncreaseAreaPeople");

        transform.parent.transform.parent.transform.parent.GetComponent<Area>().People -= 5;

        if (transform.parent.transform.parent.transform.parent.GetComponent<Area>().People < 0)
            transform.parent.transform.parent.transform.parent.GetComponent<Area>().People = 0;

        transform.parent.transform.parent.transform.parent.GetComponent<Area>().UpdateStats();

        GameObject.FindObjectOfType<GameManager>().PlayerCoins += CoinsWonOnDestroy;
        GameObject.FindObjectOfType<GameManager>().UpdateCoinText();
        GameObject.FindObjectOfType<SoundManager>().PlayButtonClickSound();


        float posX = transform.position.x;
        float posY = transform.position.y;
        GameObject i = Instantiate(ParticleDissapearing, new Vector3(posX, posY, 0f), Quaternion.identity);

        Destroy(i, ParticleDissapearing.GetComponent<ParticleSystem>().main.duration);

        Destroy(gameObject);
    }
}
