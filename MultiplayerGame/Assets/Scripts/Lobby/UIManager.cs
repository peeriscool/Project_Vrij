using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    private void Update()
    {
        //devmode cheat button
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("EpisodeNamingScene", LoadSceneMode.Single);
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
        //StartCoroutine(Countdown(1));
        // IEnumerator Countdown(float Time)
        //{
        //    while (Time > 0)
        //    {
        //        yield return new WaitForSeconds(1f);
        //        Time--;
        //    }
        //    ClientSend.PlayerReady(false);
        //}
       
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
            Debug.Log("not ready ");
            ClientSend.PlayerReady(false);
        }
       
    }

}
