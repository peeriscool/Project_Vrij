using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    /*
     To DO:
     - assign scenes to players dynamicly
     - scan for GameStartBool
     */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void EndLobby(bool start)
    {
        if(start == true)
        {
            //SceneManager.DontDestroyOnLoad();

            SceneManager.LoadScene("TvScene1", LoadSceneMode.Single); //or additive load?
            
        }
    }
}
