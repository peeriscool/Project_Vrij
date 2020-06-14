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
    public Toggle Ready; 
    bool toggle = true;
    Color defaultbuttoncolor;
    public Image ReadyButtonImage;

    bool connected = false;
    public Text playercounttext;
    Text newplayercounttext;
    private void Awake()
    {
      

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            //instance already exists
            Destroy(this);
        }
         defaultbuttoncolor = ReadyButtonImage.color;
        
    }
    public void ConnectedToServer()
    {
        if(usernameField.text == null || usernameField.text == "Username...")
        {
            usernameField.text = "NoName";
        }
        startMenu.SetActive(false);
        usernameField.interactable = false;

        Client.instance.ConnectedToServer();
        
        //Client.instance.RefreshPlayerList(); //initial load of online plyers on the server
    }
    public void recievednewplayer()
    {
        Names.text = "";
        ClientSend.GetListOfPlayers();
    }
    public void ConnectedToServer(bool ready)
    {
        if (usernameField.text == null || usernameField.text == "Username...")
        {
            usernameField.text = "NoName";
        }
        startMenu.SetActive(false);
        usernameField.interactable = false;

        Client.instance.ConnectedToServer();

        //Client.instance.RefreshPlayerList(); //initial load of online plyers on the server
    }
    // #region 
    //peer method
    public void RefreshPlayerList() //request server for names
    {
        Names.text = "";
        ClientSend.GetListOfPlayers();
    }
    public void Fillplayerlist(string name,int playercount) //is called from the server (client handle)
    {
        Names.text += name;
        newplayercounttext = playercounttext;
        newplayercounttext.text = playercount + playercounttext.text.Substring(1); //Retrieves a substring from this instance. The substring starts at a specified character position and continues to the end of the string.
    }
    //public void ReadyToggle()
    //{
    //    if (toggle == true)
    //    {
    //        Ready.isOn = true;
    //        toggle = false;
    //        ClientSend.PlayerReady(true);
    //    }
    //   else if (toggle == false)
    //    {
    //        Ready.isOn = false;
    //        toggle = true;
    //        Debug.Log("not ready ");
    //        ClientSend.PlayerReady(false);
    //    }
       
    //}
    public void ReadyButton()
    {

        if (connected == false)
        {
            ConnectedToServer(true);
            connected = true;
        }

        if (toggle == true)
        {  
            toggle = false;
            ReadyButtonImage.color = Color.green;
           StartCoroutine(ExampleCoroutine(true));
          
            Debug.Log("I'm ready!");
        }
        else if (toggle == false)
        {
            toggle = true;
            Debug.Log("not ready, stop  it plz ");


            ReadyButtonImage.color = defaultbuttoncolor;
            StartCoroutine(ExampleCoroutine(false));
           // ClientSend.PlayerReady(false);
        }
       
    }

    //Most epic back button made by Jesse Brolsma yeah yeah yeah B)
    public void BackButton()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    IEnumerator ExampleCoroutine(bool status)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        ClientSend.PlayerReady(status);
    }
    }
