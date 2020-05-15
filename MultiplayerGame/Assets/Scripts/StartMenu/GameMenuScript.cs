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
}
