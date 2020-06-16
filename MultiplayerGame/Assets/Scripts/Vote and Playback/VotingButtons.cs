using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
public class VotingButtons : MonoBehaviour
{
    public GameObject buttonprefab;
    public GameObject winner;
    public Canvas canvas;
    public GameObject buttonlocation;
    Vector3 spawnloction;
    int buttonCount;
    Quaternion rot = new Quaternion(0,0,0,0);
    public Vector3 margin;
    public Quaternion objectModification;

    public Text nameofkanidates;
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
            InstantiatedButton.GetComponentInChildren<Text>().text = UserDataAcrossScenes.getchannelnamewithgamecode(i+1); //change button name to episode names
        }
    }
    public void displayonlyvotebuttons(List<int>votenumbers)
    {
        //RepresentVotingSystem voteing = this.gameObject.AddComponent<RepresentVotingSystem>();
        RepresentVotingSystem voteing = new RepresentVotingSystem();
        voteing.votearraytovisual(votenumbers, winner, objectModification);
        //ToDO: display names of scenes 

        int amountofplayers = UserDataAcrossScenes.gamecode.Length;
        int x = 0;
        for (int i = 0; i < amountofplayers; i++) //todo get amount of buttons dynamicly from the server
        {
            Text Nameofkanidate = Instantiate(nameofkanidates, new Vector3(x, 0, 0), rot) as Text;
            Nameofkanidate.transform.SetParent(GameObject.Find("WinningCanvas").GetComponent<Canvas>().transform, false);
            Nameofkanidate.text = UserDataAcrossScenes.getchannelnamewithgamecode(i+1);
            Nameofkanidate.transform.position = Nameofkanidate.transform.position + new Vector3(i * 120, 200, -1);
            x = x + 40;
        }
    }

    public GameObject SetParent(GameObject obj) //assign button to canvas
    {
        obj.transform.SetParent(canvas.transform, false);
        return obj;
    }



   List <string> RemoveDuplicatesIterative(List<string> items)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < items.Count; i++)
        {
            // Assume not duplicate.
            bool duplicate = false;
            for (int z = 0; z < i; z++)
            {
                if (items[z] == items[i])
                {
                    // This is a duplicate.

                    duplicate = true;
                    break;
                }
            }
            // If not duplicate, add to result.
            if (!duplicate)
            {
                result.Add(items[i]);
            }
        }
        return result;
    }

}