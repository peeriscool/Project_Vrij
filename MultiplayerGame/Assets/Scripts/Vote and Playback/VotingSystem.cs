using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VotingSystem : MonoBehaviour
{
    // Start is called before the first frame update
    static List<int> votes = new List<int>();
    static int index = 0;
    int votecount = 0;
   public void Submit()
    {
        int vote1 = votes[0];
        int vote2 = votes[1];
        ClientSend.SendVotesToServer(vote1, vote2);
        //ToDo: disable submid button
    }

    public void onbuttonclick(Button Sender)
    {
        string onlynumber = Sender.name.Substring(Sender.name.Length - 1);
       // Debug.Log(onlynumber);
        int buttonnumber = int.Parse(onlynumber);
        Voting(buttonnumber,Sender);

    }
    private void Update()
    {
        if(votes.Count == 2)
        {
            GameObject.Find("Submit").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("Submit").GetComponent<Button>().interactable = false;
        }
    }

    void Voting(int scenenumber,Button sender)
    {
        int count = 0;
        bool add = true;

        if (votes.Count == 0)
        {
            //first vote
            votes.Add(scenenumber);
            sender.GetComponent<Image>().color = Color.green;
            votecount += 1;
            return;
        }
        if (votes.Count == 1)
        {
            //second vote
            if(votes[0] == scenenumber) //not twice the same
            { return; }
            votes.Add(scenenumber);
            sender.GetComponent<Image>().color = Color.yellow;
            votecount += 1;
            return;
        }

        foreach (int item in votes) //control if vote is already made on the scene
        {
            
            if (item == scenenumber)
            {
                //remove vote from list
                votes.RemoveAt(count);
                add = false;
                index =-1;
                //To DO: remove medalion
                sender.GetComponent<Image>().color = Color.red;
                break;
            }
            count =+1;
        }

        if((int)votes.Count > 0) //you still have votes left
        {
            if (add) //you didnt already add the vote
            {
                if(votecount <2)
                {
                   
                    votes[index] = scenenumber;
                    index = +1;
                    sender.GetComponent<Image>().color = Color.green;
                    //GameObject.Find("Submit").GetComponent<Button>().interactable = true;
                }
               
            }
        }

       
    }

    public void reciveAllvotes(List<int> votes)
    {
        GameObject.Find("VotingCanvas").SetActive(false);
        SceneManager.LoadScene("WinningVideoScene",LoadSceneMode.Additive);
        //To Do: display votes
        foreach (int item in votes)
        {
            Debug.Log(item);
        }
        UserDataAcrossScenes.Votes = votes;
    }
}
