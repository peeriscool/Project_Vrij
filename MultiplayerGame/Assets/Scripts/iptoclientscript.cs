using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class iptoclientscript : MonoBehaviour
{
    public Text inputfield;
    // Start is called before the first frame update
    void onclickapprove()
    {
        try
        {
            Client.instance.ip = inputfield.text;
        }
        catch (System.Exception)
        {
            Debug.Log("thats no Ip you silly");
            throw;
        }
       
    }

    // Update is called once per frame
    public void controlIp(GameObject sender)
    {
        string ip = inputfield.text;
        string[] ipStringArr = ip.Split('.');
        int[] ipIntArr = new int[ipStringArr.Length];
        if (ipIntArr.Length != 4)
        {
            //error
        }
        else
        {
            for (int i = 0; i < ipStringArr.Length; i++)
            {
                ipIntArr[i] = int.Parse(ipStringArr[i]);
                if (ipIntArr[i] < 0 || ipIntArr[i] > 255)
                {
                    //error
                }
            }
            Debug.Log("aproved");
            sender.GetComponent<Image>().color = Color.green;
            onclickapprove();
            sender.GetComponent<Button>().interactable = false;
        }
    }
}
