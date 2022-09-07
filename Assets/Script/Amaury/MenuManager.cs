using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public KeyCode up, down, enter;


    public int buttonIndex;
    public int maxButtonIndex;

    public UnityEvent downEvent;
    public UnityEvent upEvent;

    public Text[] actionTexts;
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(down)) {
            if (buttonIndex < (maxButtonIndex - 1)) {
                buttonIndex++;
                downEvent?.Invoke();
            }
        }

        if (Input.GetKeyDown(up)) {
            if (buttonIndex > 0) {
                buttonIndex--;
                upEvent?.Invoke();
            }
        }

        if (Input.GetKeyDown(enter)) {
            switch (buttonIndex) {
                case 0:
                    GameManager.Instance.OnLaunchGame();
                    break;
                
                case 1:
                    GameManager.Instance.OnQuit();
                    break;
            }
        }
        
        
        
    }


    public void DownMenu()
    {
        actionTexts[buttonIndex].color = Color.yellow;
        if(buttonIndex - 1 >= 0)
            actionTexts[buttonIndex - 1].color = Color.white;
        
    }

    public void UpMenu()
    {
        actionTexts[buttonIndex].color = Color.yellow;
        if(buttonIndex + 1 < actionTexts.Length)
            actionTexts[buttonIndex + 1].color = Color.white;
    }
}
