using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeepSpawn : MonoBehaviour
{
    [SerializeField] private GameObject jeep;

    [SerializeField] private float jeepSpeed = 1;

    [SerializeField] private Vector2 spawnTime;

    [SerializeField]public SoldierAgent Player;


    public GameObject Jeep;

    float enableTime;
    float timeElapsed;

    // Start is called before the first frame update

    private void OnEnable()
    {
        enableTime = Random.Range(spawnTime.x, spawnTime.y);
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > enableTime)
        {
            var jeepGo = Instantiate(jeep, transform.position, Quaternion.identity);
            var move = jeepGo.GetComponent<CarMove>();
            move.spawn = this;
            move.Speed = this.jeepSpeed;
            move.target = Player.gameObject;
            this.Jeep = jeepGo;

            jeepGo.name = gameObject.name + "Instance";
            jeepGo.tag = "Enemy";

            this.enabled = false;
        }
    }

    void OnDrawGizmos()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;

        ////Draw the suspension
        //Gizmos.DrawLine(
        //    Vector3.zero,
        //    Vector3.up
        //);

        //draw force application point
        Gizmos.DrawSphere(transform.position, 0.3f);

        Gizmos.color = Color.white;
#endif
    }


}
