using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class LearningAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    public float moveSpeed = 4f;
    private Vector3 position;
    public MeshRenderer floorRenderer;
    public Material loseMaterial;
    public Material winMaterial;


    private void Start()
    {
        this.position = transform.position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuoutActions = actionsOut.ContinuousActions;
        continuoutActions[0] = Input.GetAxisRaw("Vertical");
        continuoutActions[1] = Input.GetAxisRaw("Horizontal");
    }

    public void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Goal")) {
            SetReward(2f);
            floorRenderer.material = winMaterial;

            Debug.Log("Goal Reached ...");
        } 
        else if (other.CompareTag("Wall"))
        {
            floorRenderer.material = loseMaterial;
            SetReward(-1f);
            Debug.Log("Wall Reached ...");
        }
        EndEpisode();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 1),0,Random.Range(-4, 1));
        targetTransform.localPosition = new Vector3(Random.Range(-3, 1), 0, Random.Range(2.2f, 4));
    }
}
