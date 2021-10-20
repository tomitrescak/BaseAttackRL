using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    [SerializeField]
    private float CadencyPerSecond = 1f;
    [SerializeField]
    private GameObject bullet;

    private Transform bulletSpawn;
    private Rigidbody2D rb;
    private float angle = 0;

    private float lastShot;

    public bool CanShoot { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        lastShot = -CadencyPerSecond;
        bulletSpawn = transform.Find("BulletSpawn");
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShot < 1 / CadencyPerSecond)
        {
            this.CanShoot = false;
        } else
        {
            this.CanShoot = true;
        }

        //if (Input.GetKey(KeyCode.Space) && this.CanShoot) {
        //    this.Shoot();
        //}

        //var rotate = -1 * Input.GetAxis("Horizontal");
        //angle = Mathf.Clamp(rotate + angle, -90, 90);
        //rb.MoveRotation(angle);
    }

    public void Shoot()
    {
        lastShot = Time.time;

        var b = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        b.tag = "Bullet";
        var rb = b.GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * 5;
        this.CanShoot = false;
    }
}
