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
    int buttonCount;
    Quaternion rot = new Quaternion(0,0,0,0);
    public Vector3 margin;
    void Awake()
    {
        spawnloction = buttonlocation.transform.position; 
        if (UserDataAcrossScenes.gamecode != null)
        {
            buttonCount = UserDataAcrossScenes.gamecode.Length;
        }
        else
        { buttonCount = 1; }
        spawnvotebuttons();
    }
    public void spawnvotebuttons()
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
    public void displayonlyvotebuttons(List<int>votenumbers)
    {
        List<string> votedfor = new List<string>();
        for (int i = 0; i < votenumbers.Count; i++)
        {
            votedfor.Add(UserDataAcrossScenes.getchannelnamewithgamecode(votenumbers[i]));
        }


        //string placeholder;
        //foreach (string name in votedfor)
        //{
        //    if (name)
        //    {

        //    }

        //    placeholder = name;
        //}
        for (int i = 0; i < buttonCount; i++) //todo get amount of buttons dynamicly from the server
        {
            string name = "button " + i.ToString();
            GameObject InstantiatedButton =
            SetParent(Instantiate(buttonprefab, spawnloction, rot));
            InstantiatedButton.name = name;
            spawnloction = spawnloction + margin; //change location of next button
            InstantiatedButton.GetComponentInChildren<Text>().text = "hoeveelste plaats"; //change button name to episode names
            InstantiatedButton.GetComponent<Button>().interactable = false;
        }
    }

    public GameObject SetParent(GameObject obj) //assign button to canvas
    {
        obj.transform.SetParent(canvas.transform, false);
        return obj;
    }
}