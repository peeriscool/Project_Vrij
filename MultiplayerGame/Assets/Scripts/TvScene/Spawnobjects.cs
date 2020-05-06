using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnobjects : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject Activeprefab;
    public Camera MainCamera;


    public int numberOfButtons = 4;
    public Transform buttonPanel;
    public Transform buttonPrefab;

    void Start()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            Transform buttonInstance = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as Transform;
            buttonInstance.SetParent(buttonPanel);
            buttonInstance.GetComponent<buttonScript>().returnValue = i;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 Touchpos = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 130f));
            Instantiate(Activeprefab, Touchpos, Quaternion.identity);
        }
    }
    void prefabselector()
    {

    }
}
