using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using TMPro;

public class SoldierAgent : Agent
{
    private static Vector3 NullPosition = new Vector3(-1000, -1000, -1000);

    public JeepSpawn[] spawns;

    private Rigidbody2D rb;
    private ShootCannon cannon;
    private float angle = 0;


    [SerializeField] private float rotationSpeed = 1;

    [SerializeField] public TextMeshPro score;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.cannon = GetComponent<ShootCannon>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // add all spawns
        foreach (var spawn in spawns) {
            sensor.AddObservation(spawn.Jeep ? spawn.Jeep.transform.position : NullPosition);
        }
        sensor.AddObservation(transform.rotation.eulerAngles);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.ContinuousActions[0]);

        float currentAngle = -1 * actions.ContinuousActions[0] * rotationSpeed;
        angle = Mathf.Clamp(currentAngle + angle, -90, 90);
        rb.MoveRotation(angle);

        // transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        var shoot = actions.DiscreteActions[0];
        if (shoot == 1 && this.cannon.CanShoot)
        {
            this.cannon.Shoot();
        }
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        // prohibit shooting cannot during the period when it cannot be shot
        actionMask.SetActionEnabled(0, 1, this.cannon.CanShoot);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //Debug.Log("Collecting Heuristics");

        var continuoutActions = actionsOut.ContinuousActions;
        continuoutActions[0] = Input.GetAxisRaw("Horizontal");

        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }


    public override void OnEpisodeBegin()
    {
        // destroy jeeps
        foreach (var b in spawns)
        {
            if (b.Jeep)
            {
                Destroy(b.Jeep);
            }
            b.enabled = false;
            b.enabled = true;
        }
        this.angle = 0;
        transform.rotation = Quaternion.identity;
    }
}
