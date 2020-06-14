using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VotingButtons : MonoBehaviour
{
   public GameObject buttonprefab;
    public Canvas canvas;
    public GameObject buttonlocation;
    Vector3 spawnloction;
    public int buttonCount;
   Quaternion rot = new Quaternion(0,0,0,0);
    public Vector3 margin;
    void Awake()
    {
        spawnloction = buttonlocation.transform.position;
        spawnvotebuttons();
    }
    void spawnvotebuttons()
    {
        for (int i = 0; i < buttonCount; i++) //todo get amount of buttons dynamicly from the server
        {
            string name = "button " + i.ToString();
            GameObject InstantiatedButton =
            SetParent(Instantiate(buttonprefab, spawnloction, rot));
            InstantiatedButton.name = name;
            spawnloction = spawnloction + margin; //change location of next button
            InstantiatedButton.GetComponentInChildren<Text>().text = "Kooking Channel"; //change button name to episode names
        }
    }

    public GameObject SetParent(GameObject obj) //assign button to canvas
    {
        obj.transform.SetParent(canvas.transform, false);
        return obj;
    }
}