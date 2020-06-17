using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdownandactivateothercanvas : MonoBehaviour
{
    public float currCountdownValue;
    public Canvas owner;
    public Canvas enable;
    public float tijd;

    public AudioSource winnerBaby;
    public AudioSource winnerSong;
    // Start is called before the first frame update
   void Start()
    {
        StartCoroutine(StartCountdown(tijd,owner, enable));
    }

    public IEnumerator StartCountdown(float countdownValue,Canvas Owner, Canvas Enable)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log(currCountdownValue);
            yield return new WaitForSeconds(1);
            currCountdownValue--;
        }
        Debug.Log("TimerDone");
        Owner.gameObject.SetActive(false);
        Enable.gameObject.SetActive(true);
        winnerBaby.Play();
        winnerSong.Play();
    }
}
