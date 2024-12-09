using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]



public class NavMeshPathTracer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private NavMeshAgent agent;
    public float pathYOffset = -0.1f;
    public bool isPathVisible = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing from the GameObject.");
            return;
        }

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0;
    }


    public void Update()
    {
        if (isPathVisible && agent.hasPath)
        {
            if (agent.path.corners.Length > 0)
            {
                DrawPath();
            }
            else
            {
                Debug.LogWarning("Agent has no path corners.");
                lineRenderer.positionCount = 0; 
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    void DrawPath()
    {
        NavMeshPath path = agent.path;
        lineRenderer.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            
            Vector3 position = path.corners[i];
            position.y += pathYOffset;
            lineRenderer.SetPosition(i, position);
        }
    }
    public void ActivatePath()
    {
        isPathVisible = true;
        lineRenderer.positionCount = 0; // Reset the path
        if (agent.hasPath)
        {
            DrawPath();
        }
    }

    // Method to deactivate the path display
    public void DeactivatePath()
    {
        isPathVisible = false;
        lineRenderer.positionCount = 0; // Clear the path instantly
    }

}
