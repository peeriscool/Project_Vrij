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

        int diffrentlocationX = 0;
        int diffrentlocationy = 0;
        int ycollum1 = 0;
        int ycollum2 = 0;
        int ycollum3 = 0;
        int scalemultiplier = 3;
        
        foreach (int vote in votenumbers)
        {
            if(vote == 0)
            {
                if (diffrentlocationX == 0) //check for row
                {
                    ycollum1 =+ scalemultiplier;
                    diffrentlocationy = ycollum1; //place cube ontop of other cube
                }
                diffrentlocationX = 0;
                //place cube
            }
            if (vote == 1)
            {
                if (diffrentlocationX == 2)
                {
                    ycollum2 =+ scalemultiplier;
                    diffrentlocationy = ycollum2;
                }
                diffrentlocationX = 2;

            }
            if (vote == 2)
            {
                if(diffrentlocationX == 4)
                {
                    ycollum3 =+ scalemultiplier;
                    diffrentlocationy = ycollum3;
                }
                diffrentlocationX = 4;
            }
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //   cube.transform.position = this.gameObject.transform.position;
            cube.transform.localScale *= 1; 
            cube.transform.position = new Vector3(diffrentlocationX, diffrentlocationy, 0);
            var cubeRenderer = cube.GetComponent<Renderer>();
            Color32 cubecolor = new Color32(0, (byte)(60 + diffrentlocationy * 40), 0, 100);
            cubeRenderer.material.SetColor("_Color", cubecolor);

            
        }
        int biggest = Math.Max(Math.Max(ycollum1, ycollum2), ycollum3);
        GameObject win = Instantiate(winner);
        if (biggest == ycollum1)
        {
            win.transform.position = new Vector3(0, 0, -3);
        }
        if (biggest == ycollum2)
        {
            win.transform.position = new Vector3(2, 0, -3);
        }
        if (biggest == ycollum3)
        {
            win.transform.position = new Vector3(4, 0, -3);
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