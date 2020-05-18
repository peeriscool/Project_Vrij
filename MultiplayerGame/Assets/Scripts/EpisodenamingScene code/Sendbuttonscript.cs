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
    private void Update()
    {
        //if all episode names are shown, display allscenesloaded.text en go to prep scene after a short countdown
        // StartCoroutine(Countdown(3));

 
            

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
    public void SendButtonIsclicked()
    {
        inputfieldtext = inputfield.text;
        if (inputfieldtext != null)
        {
            // GameMenuScript LoadGamescene = new GameMenuScript();
            // LoadGamescene.LoadGameScene(inputfieldtext, "TvScene1"); // "TvScene1" should be replaced with the channelnaam of the player (NOT THE ONE YOU MAKE THE EPISODE NAME FOR)
           string mijnscene = UserDataAcrossScenes.findyourscene(Client.instance.myid);
            GameMenuScript loadmyscene = new GameMenuScript();
            loadmyscene.LoadGameScene(mijnscene);
        }
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
