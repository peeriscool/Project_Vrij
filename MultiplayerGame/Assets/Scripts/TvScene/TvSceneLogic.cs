using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TvSceneLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public Text countdownObject;
    public int CountdownTime;
    public string Countdowntext;
    void Start()
    {
        StartCoroutine(Countdown(CountdownTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Countdown(float Time)
    {
        while (Time > 0)
        {
            countdownObject.text = Countdowntext + Time.ToString();
            yield return new WaitForSeconds(1f);
            Time--;
        }
        countdownObject.text = "Time's up!";
        SceneManager.LoadScene("PlaybackScene", LoadSceneMode.Single);
    }
}
