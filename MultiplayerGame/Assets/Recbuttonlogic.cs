using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Recbuttonlogic : MonoBehaviour
{
    public Image Recbutton;
    // Start is called before the first frame update
    public Text CountdownText;
    string memberme;
    float timer = 10f;
    float currCountdownValue;

    public AudioSource readySet;

    public void Recbuttonenable()
    {
       
        memberme = CountdownText.text;
        StartCoroutine(StartCountdown(timer));
       
    }
    public void changecolor(Image changethis)
    {
        changethis.color = Color.white;
    }

    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            CountdownText.text = memberme + currCountdownValue.ToString();
            if (currCountdownValue == 6)
            {
                readySet.Play();
            }
            currCountdownValue--;
        }
        Debug.Log("TimerDone");
        changecolor(Recbutton);
        CountdownText.text = "We are Live!";
        GameObject.Find("ScriptHolder_InGame").GetComponent<AudioRec>().startRecording();
    }
}
