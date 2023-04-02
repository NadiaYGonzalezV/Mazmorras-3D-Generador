using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openSide; //saber el sitio donde se encuentra la puerta o el sitio abierto

    //1 necesitamos una puerta abajo
    //2 necesitamos una puerta arriba
    //3 necesitamos una puerta izquierda
    //4 necesitamos puerta derecha

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>(); //tag rooms
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
       if (spawned == false)
        {
            if (openSide == 1)
            {
                //Necesitamos puerta abajo
                rand = Random.Range(0, templates.bootomRooms.Length);
                Instantiate(templates.bootomRooms[rand], transform.position, templates.bootomRooms[rand].transform.rotation);

            }
            else if (openSide == 2)
            {
                //necesitamos puerta arriba
                rand = Random.Range(0, templates.TopRooms.Length);
                Instantiate(templates.TopRooms[rand], transform.position, templates.TopRooms[rand].transform.rotation);
            }
            else if (openSide == 3)
            {
                //neceistamos puerta izquierda
                rand = Random.Range(0, templates.LeftRooms.Length);
                Instantiate(templates.LeftRooms[rand], transform.position, templates.LeftRooms[rand].transform.rotation);
            }
            else if (openSide == 4)
            {
                //puerta derecha
                rand = Random.Range(0, templates.RightRooms.Length);
                Instantiate(templates.RightRooms[rand], transform.position, templates.RightRooms[rand].transform.rotation);
            }
            spawned= true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
