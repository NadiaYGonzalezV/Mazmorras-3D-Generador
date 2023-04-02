using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //Aqui van a estar todos los prefabs de las salas para saber donde se encuentran las salidas

    public GameObject[] bootomRooms; //array por que podemos tener mas de  una habitacion con salidas abajo
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    //aqui podemos tener referencias a objetos de vida,mana, etc
    public GameObject boss;
    public GameObject enemys;

    private void Start()
    {
        Invoke("SpawnEnemys", 3f); // "f" segundos a esperar 
    }

    void SpawnEnemys()
    {
        //boss en la ultima sala
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity); //menos uno por que las listas empiezan desde cero

        //instanciar en cada salda enemigso 

        for (int i = 0; i < rooms.Count-1; i++) //-1 es para que no aparezca en la ultima sala 
        {
            Instantiate(enemys, rooms[i].transform.position, Quaternion.identity);
        }
    }


}
