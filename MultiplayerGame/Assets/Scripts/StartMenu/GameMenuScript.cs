using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    { SceneManager.LoadScene("Lobby", LoadSceneMode.Single); }
    public void ExitButton()
    { Application.Quit(); }
    public void InstructionButton()
    { 
        // idk something like play a instruction video or somethin
    }
    public void loadVoteScene()
    { SceneManager.LoadScene("Vote", LoadSceneMode.Additive); }
    public void LoadWinningVideoScene()
    { SceneManager.LoadScene("WinningVideoScene", LoadSceneMode.Single); }
    public void LoadMenuScene()
    { SceneManager.LoadScene("StartMenu", LoadSceneMode.Single); }
    public void Discconect()
    { Client.instance.Disconnect(); }

   
    public void LoadGameScene(string PlayerScene) 
    {SceneManager.LoadScene(PlayerScene, LoadSceneMode.Single); }

    //overload method als ik de episode naam al zou weten Sendbuttonscript.SendButtonIsclicked()
    public void LoadGameScene(string episodename, string PlayerScene)
    { UserDataAcrossScenes.Episodename = episodename; SceneManager.LoadScene(PlayerScene, LoadSceneMode.Single); }

    public void EndLobby(bool start)
    {
        if (start == true)
        {
            //SceneManager.DontDestroyOnLoad();
            DontDestroyOnLoad(GameObject.Find("ClientManager"));
            //GameObject.Find("Menu").SetActive(false);
            SceneManager.LoadScene("EpisodeNamingScene", LoadSceneMode.Single); //or additive load?
        }
    }
}
