using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        transform.position += new Vector3(speed*Time.deltaTime,0f,0f);
        if (transform.position.x > 5.5f)
            Destroy(gameObject);
    }


}
