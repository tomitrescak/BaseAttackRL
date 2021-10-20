using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed = 2;
    public GameObject explosion;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
             Destroy(gameObject);
        }
        //Debug.Log("Collided");
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) { 
            Destroy(gameObject);
        }
        Debug.Log("Collided");
    }
}
