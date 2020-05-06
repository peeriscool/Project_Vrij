using UnityEngine;
using System.Collections;

public class buttonScript : MonoBehaviour
{
    public int returnValue;

    public void ClickMe()
    {
        Debug.Log(returnValue);
    }
}
