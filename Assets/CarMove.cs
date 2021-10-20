using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] public float Speed = 1;
    [SerializeField] private GameObject explosion;
    
    public GameObject target;

    Transform player;
    Rigidbody2D rb;

    public JeepSpawn spawn;

    // Start is called before the first frame update
    void Start()
    {
        player = target.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void LookAt()
    {
        //Vector3 diff = player.position - transform.position;
        //diff.Normalize();

        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        transform.up = player.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAt();

        // transform.LookAt(player, Vector3.right);
        //Vector2 LookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        //transform.LookAt(LookAtPoint);

        // transform.LookAt(player, Vec);
        // 
        //Vector2 LookAtPoint = new Vector2(player.transform.position.z, player.transform.position.y);
        //transform.LookAt(new Vector3(0, LookAtPoint.y, LookAtPoint.x));

        rb.velocity = transform.up * Time.deltaTime * Speed;
        // transform.Translate(transform.forward * 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // we shot the jeep
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Debug.Log("Bullet trigger!");

            var instance = Instantiate(explosion, transform.position, Quaternion.identity);

            spawn.Player.AddReward(this.Speed);
            spawn.enabled = true;
            spawn.Jeep = null;

            Destroy(instance, 1);
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.5f);

        }
        // jeep crashed into base
        else if (collision.gameObject.CompareTag("Player"))
        {
            spawn.Player.SetReward(-100);
            spawn.Player.EndEpisode();
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Bullet"))
        //    {
        //        Debug.Log("Bullet collison!");
        //        spawn.enabled = true;
        //        Destroy(gameObject, 0.5f);
        //    }
        //}
    }
}
