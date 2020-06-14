using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayWinner : MonoBehaviour
{
    public Text Dispalyvotes;
    bool recieved = false;
    // Start is called before the first frame update
    void Start()
    {
        Dispalyvotes.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(UserDataAcrossScenes.Votes != null && recieved == false)
        {
            foreach (var item in UserDataAcrossScenes.Votes)
            {
                Dispalyvotes.text = Dispalyvotes.text + item.ToString() + ",";
            }
            recieved = true;
            GameObject.Find("Buttonscriptholder").GetComponent<VotingButtons>().displayonlyvotebuttons(UserDataAcrossScenes.Votes);
            // Dispalyvotes.text = string.Join(",", UserDataAcrossScenes.Votes.ToString()); //should display everyvalue seperatly
        }

     
    }
}
