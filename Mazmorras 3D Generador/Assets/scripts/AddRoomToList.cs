using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AddRoomToList : MonoBehaviour
{
    //este script es el encargado de mostrar la lista en el inspector 

    //private RoomTemplates templates;

    public RoomTemplates templates;




    void Start()
    {
        templates=GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);


        templates.roomCountText.text = "Número de Habitaciones: " + templates.rooms.Count;
       




    }

   




}
