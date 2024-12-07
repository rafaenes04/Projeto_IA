using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]


public class NavMeshPathTracer : MonoBehaviour
{
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;
    public float pathYOffset = -0.1f; 
    private bool showPath = false; // Flag to toggle path visibility

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }

    public void TogglePathVisibility()
    {
        showPath = !showPath; // Toggle the visibility flag
        lineRenderer.enabled = showPath; // Enable or disable the line renderer
    }

    void Update()
    {
        if (showPath && agent.hasPath)
        {
            DrawPath();
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

}
