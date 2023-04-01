using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRandom : MonoBehaviour
{
    [SerializeField] int maxStep = 3;
    [SerializeField] int maxX = 4;
    [SerializeField] int maxY = 4;
    [SerializeField] int emptyCells = 0;

    [SerializeField] float period = 0.5f;

    MenTurtle turtle; //modelo
    MemMaze maze;//modelo
    TileMap16 tileMap; //vista

    void Start()

    {
        Application.targetFrameRate = 30;
        tileMap = GameObject.Find("TileMap16").GetComponent<TileMap16>();
        turtle = new MenTurtle();
        turtle.SetClamp(maxX, maxY);

        turtle.forwardDelegate = (pos, lastPos, turn, invTurn) =>
        {
            maze.Add(pos, lastPos, turn, invTurn);
        };

        maze = new MemMaze();
        maze.iteratorDelegate = (x, y, value) =>
        {
            tileMap.AddTile(x, y, value & 0x0F, (value >> 4) & 0x0F);
        };

        StartCoroutine("WalkCoroutine");
    }

    IEnumerator WalkCoroutine()
    {
        while (true)
        {
            Walk();
            yield return new WaitForSeconds(period);
        }
    }
    void Restart()
    {
        maze.Clear();
        maze.Fill(maxX, maxY);
        maze.AddColor(turtle.Pos, 1);
    }

    void UpdateTilemap()
    {
        tileMap.ClearMesh();
        maze.IterateRect();
        tileMap.UpdateMesh();
    }

    void Walk()
    {
        Restart();
        while(maze.GetEmptyCount() > emptyCells)
        { //puede repetir giro
            turtle.TurnTo(Random.Range(0, 4));

            //no repetir giro
            //turtle.AddTurn(random.Range(1,4));
            //Girar siempre;
            //turtle.AddTurn(1 + 2 * Random.Range(1,3));

            turtle.Forward(Random.Range(1, maxStep + 1));
        }//while
        maze.AddColor(turtle.Pos, 2);
        UpdateTilemap();
    } 


}
