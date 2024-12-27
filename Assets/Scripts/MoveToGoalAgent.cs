using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;



public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform goalTransform;
    [SerializeField] private Transform agentPosition;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private float penaltyRate = -0.009f;
    private float timeOnStartGround;
    [SerializeField] private Collider zone1Collider; // Farthest zone
    [SerializeField] private Collider zone2Collider; // Middle zone
    [SerializeField] private Collider zone3Collider; // Closest zone

    [SerializeField] private float zone1RewardRate = 0.01f; // Reward for Zone 1
    [SerializeField] private float zone2RewardRate = 0.02f; // Reward for Zone 2
    [SerializeField] private float zone3RewardRate = 0.03f; // Reward for Zone 3


    private Rigidbody rb;


    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }


    public override void OnEpisodeBegin()
    {
        // Reset agent and goal positions
        transform.localPosition = agentPosition.localPosition;
        goalTransform.localPosition = goalTransform.localPosition;
        timeOnStartGround = 0f;


    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        rb.MovePosition(transform.position + transform.forward * moveForward * speed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * speed, 0f, Space.Self);

        float distanceToGoal = Vector3.Distance(transform.localPosition, goalTransform.localPosition);
        AddReward(-distanceToGoal * 0.001f);

        if (moveForward == 0 && moveRotate == 0)
        {
            AddReward(-0.01f); // Penalize idleness
        }
        if (zone3Collider.bounds.Contains(transform.position))
        {
            AddReward(zone3RewardRate * Time.deltaTime);
        }
        else if (zone2Collider.bounds.Contains(transform.position))
        {
            AddReward(zone2RewardRate * Time.deltaTime);
        }
        else if (zone1Collider.bounds.Contains(transform.position))
        {
            AddReward(zone1RewardRate * Time.deltaTime);
        }

        /*
        Vector3 velocity = new Vector3(moveX,0f, moveY);
        velocity = velocity.normalized * Time.deltaTime * speed;

        transform.localPosition += velocity;    
        */

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            AddReward(15f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.tag == "Wall")
        {
            AddReward(-5f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(goalTransform.localPosition);
        sensor.AddObservation(Vector3.Distance(transform.localPosition, goalTransform.localPosition));

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}


