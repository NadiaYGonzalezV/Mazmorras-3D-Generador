using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public delegate void VoidFunc3Int(int x, int y, int value);

public class MemMaze
{
    Dictionary<(int, int), int> maze;

    int maxX = 0;
    int maxY = 0;

    public Dictionary<(int,int), int> Maze { get => maze; }

    public VoidFunc3Int iteratorDelegate;

    public MemMaze()
    {
        maze = new Dictionary<(int, int), int>();
        iteratorDelegate = (x, y, v) => { };
    }

    public void Clear()
    {
        maze.Clear();
    }

    public int GetValueAt(int x, int y)
    {
        return maze[(x, y)];
    }
    public int GetValueAt(float x, float y)
    {
        return maze[((int)x, (int)y)];
    }

    public int GetValueAt(Vector3 v)
    {
        return maze[((int)v.x, (int)v.y)];
    }

    public int SetValueAt(int x, int y, int value)
    {
        return maze[(x, y)] = value;
    }

    public int SetValueAt(float x, float y, int value)
    {
        return maze[((int)x, (int)y)] = value;
    }

    public int SetValueAt(Vector3 v, int value)
    {
        return maze[((int)v.x, (int)v.y)] = value;
    }

    public int CombineValueAt(int x, int y, int value)
    {
        return maze[(x, y)] |= value;
    }

    public int CombineValueAt(float x, float y, int value)
    {
        return maze[((int)x, (int)y)] |= value;
    }

    public int CombineValueAt(Vector3 v, int value)
    {
        return maze[((int)v.x, (int)v.y)] |= value;
    }

    public void Fill(int maxX, int maxY, int value = 0)
    {
        this.maxX = maxX;
        this.maxY = maxY;
        for (int y = 0; y <maxX; y ++)
        {
            for (int x = 0; x < maxX; x ++)
            {
                maze[(x, y)] = value;
            }
        }

        for (int y = -1; y < maxY +1; y ++)
        {
            maze[(-1, y)] = 15;
            maze[(maxX, y)] = 15;
        }

        for (int x = -1; x <maxX +1; x ++)
        {
            maze[(x, maxY)] = 15;
            maze[(x, -1)] = 15; //boton
        }
    }
   public void Add(
       Vector3 pos,Vector3 lastPos,
       int turn, int invTurn)
    {
        CombineValueAt(pos, 1 << invTurn);
        CombineValueAt(lastPos, 1 << invTurn);
    }
    public void AddColor (Vector3 pos,int colorID)
    {
        int value = GetValueAt(pos) & 0x0F;
        SetValueAt(pos,(colorID << 4) | value);
    }
    public void Interate()
    {
        foreach( KeyValuePair<(int,int),int> m in maze)
        {
            iteratorDelegate(m.Key.Item1, m.Key.Item2, m.Value);
        }
    }
    public void IterateRect()
    {
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX +1; x++)
            {
                iteratorDelegate(x, y, maze[(x, y)]);
            }
        }
    }
    public int GetEmptyCount()
    {
        int count = 0;

        foreach ( KeyValuePair<(int,int),int> m in maze)
        {
            if ((m.Value & 0x0F) == 0) { count++; }
        }
        return count;
    }
    public bool IsFull()
    {
        return GetEmptyCount() == 0;
    }

    public int GetNeighborsAt (int x,int y)
    {
        int result = 0;
        result |= maze[(x, y + 1)].bit(); //top
        result |= maze[(x + 1, y)].bit() << 1; //right
        result |= maze[(x, y - 1)].bit() << 2; //bottom
        result |= maze[(x-1,y )].bit() << 3; //left

        return result;
    }

    public int GetNeighborsAt (float x, float y)
    {
        return GetNeighborsAt ((int) x, (int) y);
    }
    public int GetNeighborsAt(Vector3 v)
    {
        return GetNeighborsAt((int) v.x, (int) v.y);
    }
} //clase