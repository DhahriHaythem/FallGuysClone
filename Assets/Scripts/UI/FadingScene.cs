using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadingScene : MonoBehaviour
{
    private RawImage rImage;
    GameObject Canvas;

    private float startTime;
    bool activateFade = false;
    bool setTime = false;

    GameObject newGame;
    GameObject quitGame;

    void DelayedLerp()
    {
        if(!setTime)
        startTime = Time.time;
        setTime=true;

        activateFade=true;

    }
    // Start is called before the first frame update
    void Start()
    {
        rImage = gameObject.GetComponent<RawImage>();
        newGame = GameObject.Find("NewGame");
        quitGame = GameObject.Find("QuitGame");
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DelayedLerp", 0);
        if(activateFade)
        rImage.color = Color.Lerp(Color.black, Color.clear, (Time.time-startTime)/10);
        if (rImage.color==new Color(0,0,0,0))
        {
            gameObject.SetActive(false);
        
            newGame.SetActive(true);
            quitGame.SetActive(true);
        }
    }
}
