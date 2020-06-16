using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sendbuttonscript : MonoBehaviour
{
    string inputfieldtext;
    public InputField inputfield;
    public Text allscenesloaded;
    public Text countdownObject;
    public Text ChannelNameText;
    bool Textsend = false;
    Button failsafe;
    private void Update()
    {
        //if all episode names are shown, display allscenesloaded.text en go to prep scene after a short countdown
        // StartCoroutine(Countdown(3));
        if (UserDataAcrossScenes.Episodename != null && Textsend == true)
        {
            GoToScene();
        }
        if (Input.GetKeyDown(KeyCode.CapsLock)) //secret bug pssszzzzz!
        {
            failsafe.interactable = true;
        }
    }
    private void Start()
    {
        GetGamecode();
        //change "InsertChannelName" in ChannelNameText to the correct channel name from 
        // UserDataAcrossScenes.PlayerSceneValue = 0; //tijdelijk moet verplicht ge-set zijn
        // string Channelnaam = UserDataAcrossScenes.Channelnames[UserDataAcrossScenes.PlayerSceneValue]; //vervang 0 met een value van de correcte scene
        //string newtext = ChannelNameText.text.Insert(36, Channelnaam);
        // ChannelNameText.text = newtext;
    }
    private void GetGamecode()
    {
        Debug.Log("getting gamecode");
        int[] gamecode = UserDataAcrossScenes.gamecode;
        string scenename = UserDataAcrossScenes.getchannelnamewithgamecode(Client.instance.myid);
        string newtext = ChannelNameText.text.Insert(36, scenename);
        ChannelNameText.text = newtext;
    }
    public void SendButtonIsclicked(Button Sendbutton)
    {
        failsafe = Sendbutton;
        inputfieldtext = inputfield.text;
        if (inputfieldtext != null)
        {
            Textsend = true;
            //episode naam sturen naar server en geven aan de correcte user
            ClientSend.SendEpisodeName(inputfieldtext);
           
            Sendbutton.interactable = false;

        }
    }
    public void GoToScene()
    {
        string mijnscene = UserDataAcrossScenes.findyourscene(Client.instance.myid);
        GameMenuScript loadmyscene = new GameMenuScript();
        loadmyscene.LoadGameScene(mijnscene);
    }
    public IEnumerator Countdown(float Time)
    {
        while (Time > 0)
        {
            countdownObject.text = countdownObject.text + Time.ToString();
            yield return new WaitForSeconds(1f);
            Time--;
        }
        countdownObject.text = "Let's Go!";
    }
}
