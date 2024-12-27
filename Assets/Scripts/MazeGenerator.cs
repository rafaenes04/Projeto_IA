using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine.UI;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    public NavMeshPathTracer pathTracer;
    public Button button;



    private void Start()
    {
        StartMaze();
        if (button != null)
        {
            button.onClick.AddListener(OnPathButtonClicked); // Assign the button click listener
        }
        else
        {
            Debug.LogError("Button is not assigned in the Inspector!");
        }

        // Ensure that the NavMeshPathTracer component is properly assigned
      
        if (pathTracer == null)
        {
            Debug.LogError("NavMeshPathTracer component is not attached to this GameObject!");
        }
    }

    public void OnPathButtonClicked()
    {
        // Activate the path immediately on button press
        if (pathTracer != null)
        {
            pathTracer.ActivatePath();
           
        }

        // Optionally, deactivate the path after 2 seconds
        StartCoroutine(DeactivatePathAfterDelay(2f)); // Deactivate path after 2 seconds
    }

    private IEnumerator DeactivatePathAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 2 seconds
        if (pathTracer != null)
        {
            pathTracer.DeactivatePath();  // Hide the path
        }
    }
    public void StartMaze()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
                _mazeGrid[x, z].transform.localPosition = new Vector3(x, 0, z);

            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
        CreateEntranceAndExit();



        GetComponent<NavMeshSurface>().BuildNavMesh();

    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        currentCell.IsPathCell = true;
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }
   
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }
   

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
    private void CreateEntranceAndExit()
    {

        int entranceX = Random.Range(0, _mazeWidth);
        _mazeGrid[entranceX, 0].ClearBackWall();


        int exitX = Random.Range(0, _mazeWidth);
        _mazeGrid[exitX, _mazeDepth - 1].ClearFrontWall();
    }
    public void ResetMaze()
    {
        if (_mazeGrid != null)
        {
            for (int x = 0; x < _mazeWidth; x++)
            {
                for (int z = 0; z < _mazeDepth; z++)
                {
                    if (_mazeGrid[x, z] != null)
                    {
                        Destroy(_mazeGrid[x, z].gameObject);
                    }
                }
            }
        }

        _mazeGrid = null;

        StartMaze();

        StartCoroutine(RebuildNavMeshAndPath());
    }
    private IEnumerator RebuildNavMeshAndPath()
    {
        yield return null;

        var navMeshSurface = GetComponent<NavMeshSurface>();
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }

        var agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // Wait until the NavMeshAgent has a valid path.
            yield return new WaitUntil(() => !agent.pathPending);

            // Trigger path recalculation.
            var agentNavigation = GetComponent<AgentNavigation>();
            if (agentNavigation != null)
            {
                Vector3 currentDestination = agentNavigation.DesiredDestination;
                agentNavigation.SetDestination(currentDestination);
            }

            var pathTracer = GetComponent<NavMeshPathTracer>();
            if (pathTracer != null)
            {
                pathTracer.Update();
            }
        }
    }


}
