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
        transform.up = player.position - transform.position;
        rb.velocity = transform.up * 0.016f * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // we shot the jeep
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Debug.Log("Bullet trigger!");

            var instance = Instantiate(explosion, transform.position, Quaternion.identity);

            spawn.Player.score.text = (int.Parse(spawn.Player.score.text) + 1).ToString();
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
            spawn.Player.score.text = "0";
            spawn.Player.SetReward(-100);
            spawn.Player.EndEpisode();
        }
    }
}
