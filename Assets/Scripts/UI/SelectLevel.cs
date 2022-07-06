using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SelectLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private TextMeshProUGUI levelText;
   
    [SerializeField] private Button[] lvlButtons;
    private List<int> Buttons = new List<int>();
    //public static int levelAt;
    
    private void Awake() 
    {
       
    }
    void Start()
    {
        //levelAt=3;//PlayerPrefs.GetInt("levelAt",3);
        for (int i=0;i<lvlButtons.Length;++i)
        {
            if(i+3>Inventory.levelAt)
                lvlButtons[i].interactable=false;
            
        }
        levelNumber+=1;
        levelText.text=levelNumber.ToString(); 
    }

    // Update is called once per frame
    public void OpenScene()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        /*Debug.Log(Inventory.levelAt);
        if(Inventory.levelAt>4)
        {
            Inventory.levelAt=4;
            GoBackToStart();
        }
        else
            SceneManager.LoadScene("Level2");*/
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("level"));
    }
    public void GoBackToStart()
    {
        SceneManager.LoadScene("StartGame");
    }

}
