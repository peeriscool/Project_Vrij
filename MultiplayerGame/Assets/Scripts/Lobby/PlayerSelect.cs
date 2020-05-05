using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSelect : MonoBehaviour
{
   public Image PlayerImage;
    public Sprite option0;
    public Sprite option1;
    public Sprite option2;
    int selector = 1;
    void Selection()
    {
        switch (selector)
        {
            case 1:
                PlayerImage.sprite = option0;
                break;
            case 2:
                PlayerImage.sprite = option1;
                break;
            case 3:
                PlayerImage.sprite = option2;
                break;
        }
    }

   public void buttonplus()
    {
        if (selector == 3)
        {
            return;
        }
        Debug.Log("+");
        selector += 1;
        Selection();
    }
   public void buttonminus()
    {
        if (selector == 1)
        {
            return;
        }
        selector -= 1;
        Selection();
    }
}
