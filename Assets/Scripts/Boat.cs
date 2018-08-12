using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public GameObject ParticleDissapearing;
    public Transform Destination;
    public float Speed = 2f;
    public int AmountOfPeopleCarried = 10;
    public int CoinsWonOnDestroy = 10;

    private bool hasReachedDestination = false;
    private bool isDead = false;

    private void Update()
    {
        if (hasReachedDestination || !GameObject.FindObjectOfType<CircleManager>().isGameActive || isDead)
            return;

        int dirX = 0,dirY = 0;

        if (transform.position.x < Destination.position.x)
            dirX = 1;
        else if (transform.position.x > Destination.position.x)
            dirX = -1;

        if (transform.position.y < Destination.position.y)
            dirY = 1;
        else if (transform.position.y > Destination.position.y)
            dirY = -1;

        transform.position += new Vector3(dirX * Speed * Time.deltaTime,dirY * Speed * Time.deltaTime,0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Boat>() != null)
        {
            Physics2D.IgnoreCollision(collision,GetComponent<Collider2D>());
        }
        if (collision.transform == Destination)
        {
            hasReachedDestination = true;
            Destination.parent.GetComponent<Area>().People += AmountOfPeopleCarried;
            if (Destination.parent.GetComponent<Area>().People > 100)
                Destination.parent.GetComponent<Area>().People = 100;
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        GameObject i = Instantiate(ParticleDissapearing, new Vector3(posX, posY, 0f), Quaternion.identity);

        Destroy(i, ParticleDissapearing.GetComponent<ParticleSystem>().main.duration);

        GameObject.FindObjectOfType<GameManager>().PlayerCoins += CoinsWonOnDestroy;
        GameObject.FindObjectOfType<GameManager>().UpdateCoinText();

        Destroy(gameObject);
    }
}
