using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Countdowntonextact : MonoBehaviour
{

    float currCountdownValue;
    public Text Countdowndisplay;
    public int Countdownlength;
    public ActSceneScript actlogic;
    public void StartCountdown()
    {
        StartCoroutine(scenelenght(Countdownlength));
    }
    public void StartCountdown(int lenght)
    {
        StartCoroutine(scenelenght(lenght));
    }
     IEnumerator scenelenght(float countdownValue)
    {

        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //       Debug.Log("Countdown: " + currCountdownValue);
            Countdowndisplay.text = currCountdownValue.ToString();
            yield return new WaitForSeconds(1);
            currCountdownValue--;
        }
        Countdowndisplay.text = currCountdownValue.ToString();
        // additive load 
        PlaybackLogic.LoadScenelogic();
    }

}
