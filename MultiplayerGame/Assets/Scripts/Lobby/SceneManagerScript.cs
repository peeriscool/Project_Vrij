using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    public static void EndLobby(bool start)
    {
        if(start == true)
        {
            //SceneManager.DontDestroyOnLoad();
            //DontDestroyOnLoad(GameObject.Find("ClientManager"));
            GameObject.Find("Menu").SetActive(false);
            SceneManager.LoadScene("TvScene1", LoadSceneMode.Additive); //or additive load?
            
        }
    }
}
