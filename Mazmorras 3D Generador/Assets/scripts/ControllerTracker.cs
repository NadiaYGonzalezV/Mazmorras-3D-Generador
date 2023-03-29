using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControllerTracker : MonoBehaviour
{
    [SerializeField] int maxX = 4;
    [SerializeField] int maxY = 4;
    [SerializeField] int emptyCells = 0;

    [SerializeField] float period = 0.5f;

    MenTurtle turtle; 
    MemMaze maze;
    TileMap16 tilemap;

    Stack<int> stack;
    int lastStackCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        stack = new Stack<int>();
        tilemap = GameObject.Find("TileMap16").GetComponent<TileMap16>();

        maze = new MemMaze();
        maze.iteratorDelegate = (x, y, value) =>
        {
            tilemap.AddTile(x, y, value & 0x0F, (value >> 4) & 0x0F);
        };

        turtle = new MenTurtle();
        turtle.SetClamp(maxX, maxY);
        turtle.forwardDelegate = (pos, lastPos, turn, invTurn) =>
        {
            maze.Add(pos, lastPos, turn, invTurn);
        };
        StartCoroutine("Tracker");
    }

    IEnumerator Tracker()
    {
        while (true)
        {
            Track();
            yield return new WaitForSeconds(period);
        }
    }

    void Restart()
    {
        stack.Clear();
        maze.Clear();
        maze.Fill(maxX, maxY);
        maze.AddColor(turtle.Pos, 1);
    }

    void UpdateMazeView()
    {
        tilemap.ClearMesh();
        maze.IterateRect();
        tilemap.UpdateMesh();
    }

    void Track()
    {
        Restart();
        while (maze.GetEmptyCount() > emptyCells)
        {
            int neighbor = maze.GetNeighborsAt(turtle.Pos);
            if (neighbor < 15)
            {
                GoToNeighbor(neighbor);
            }
            if (neighbor == 15 && stack.Count == lastStackCount){
                lastStackCount = 0;
                maze.AddColor(turtle.Pos, 3);
            }
            if (neighbor == 15 && stack.Count > 0)
            {
                turtle.TurnTo(stack.Pop());
                turtle.Backwadr();
            }

        }

        maze.AddColor(turtle.Pos, 2);
        //GoalPath
        UpdateMazeView();
    }

    void GoToNeighbor(int neighbor)
    {
        //hallas vecionos disponibles y escoger uno
        int[] ns = { 0, 0, 0, 0 };
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
          if(((neighbor >> i) & 0x03) == 0)
            {
                ns[count] = i;
                count++;
            }
        }
        int turn = ns[Random.Range(0,count)];
        stack.Push(turn);
        lastStackCount = stack.Count;
        turtle.TurnTo(turn);
        turtle.Forward();
    }

    void GoalPath()
    {
        while (stack.Count > 1)
        {
            turtle.TurnTo(stack.Pop());
            turtle.Backwadr();
            maze.AddColor(turtle.Pos, 4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
