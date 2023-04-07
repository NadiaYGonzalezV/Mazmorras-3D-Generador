using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazEnemigo : MonoBehaviour
{
    private RoomTemplates templates;
    public Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    void Update()
    {
        List<string> roomNames = new List<string>();
        foreach (GameObject room in templates.rooms)
        {
            roomNames.Add(room.name);
        }
        myText.text = "Rooms: " + string.Join(", ", roomNames.ToArray());
    }
}
