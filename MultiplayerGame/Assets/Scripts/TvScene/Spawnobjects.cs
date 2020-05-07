using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnobjects : MonoBehaviour
{
    public GameObject Activeprefab;
    public Camera MainCamera;
    public List<GameObject> prefabs;
    public int numberOfButtons;
    public Transform buttonPanel;
    public Transform buttonPrefab;
    public Vector3 ButtonLocation;
    void Start() //spawns list of buttons
    {//ToDo: get sprite Image from list and assign to buttons

        numberOfButtons = prefabs.Count;
        Debug.Log(numberOfButtons);
        for (int i = 0; i < numberOfButtons; i++)
        {
            Transform buttonInstance = Instantiate(buttonPrefab, ButtonLocation, Quaternion.identity) as Transform;
            buttonInstance.SetParent(buttonPanel);
            buttonInstance.GetComponent<buttonScript>().returnValue = i;
            ButtonLocation.x += 150f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //spawn objects 
        {
            Vector3 Touchpos = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 130f));
            Instantiate(Activeprefab, Touchpos, Quaternion.identity);
        }
    }
    public void prefabselector(int buttonvalue)
    {
        Activeprefab = prefabs[buttonvalue];
    }
}
