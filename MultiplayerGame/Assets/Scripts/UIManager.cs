using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public Text Names;
    public Button Refresher;
    public Toggle Ready;
    bool toggle = true;
    private void Awake()
    {
        Refresher.interactable = false;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            //instance already exists
            Destroy(this);
        }
    }

    public void ConnectedToServer()
    {
        if(usernameField.text == null || usernameField.text == "Username...")
        {
            usernameField.text = "NoName";
        }
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Refresher.interactable = true;
        Client.instance.ConnectedToServer();
        //Client.instance.RefreshPlayerList(); //initial load of online plyers on the server
    }
    // #region 
    //peer method
    public void RefreshPlayerList() //refresh button
    {
        Names.text = "";
        ClientSend.GetListOfPlayers();
    }
    public void Fillplayerlist(string name) //refresh button
    {
        Names.text += name;
    }
    public void ReadyToggle()
    {
        if (toggle == true)
        {
            Ready.isOn = true;
            toggle = false;
            ClientSend.PlayerReady(true);
        }
       else if (toggle == false)
        {
            Ready.isOn = false;
            toggle = true;
        }
       
    }

}
