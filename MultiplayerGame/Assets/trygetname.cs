using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trygetname : MonoBehaviour
{
    public Text naamholder;
    public Text copyfrom;
    public void Start()
    {
        naamholder.text = copyfrom.text;
        //   naamholder.Text = GameObject.Find("VerySpecifiekNaam").GetComponent<Text>().text
    }

}
