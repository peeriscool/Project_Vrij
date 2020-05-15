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
    public float distancefromcamera;
  
    void Start() //spawns list of buttons
    {//ToDo: get sprite Image from list and assign to buttons

        numberOfButtons = prefabs.Count;
      //  Debug.Log(numberOfButtons);
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
            //  Vector3 Touchpos = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distancefromcamera));
            //   Instantiate(Activeprefab, Touchpos, Quaternion.identity);
            raycastcall();
        }
    }
    void raycastcall()
    {
   
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
               
                    Instantiate(Activeprefab, hit.point, Quaternion.identity);
            }
 
    }
    public void prefabselector(int buttonvalue)
    {
        Activeprefab = prefabs[buttonvalue];
    }
}
/*
 *    public GameObject prefab;
    GameObject board;
   
    // Update is called once per frame
    void Update () {
        Vector3 localOffset = new Vector3(0, 0, 2);
        Vector3 worldOffset = transform.rotation * localOffset;
        Vector3 spawnPosition = transform.position + worldOffset;
       
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject board = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
        }
       
        RaycastHit hit;
        if (Input.GetButton("Fire2"))
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position + new Vector3(0, 0, 1), fwd, out hit, 100))
            {
                board.transform.position = hit.point;
            }
        }
    }
 

*/