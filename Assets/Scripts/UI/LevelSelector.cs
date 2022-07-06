using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
     static public bool fade=false;
    static public bool go = false;
    private Vector3 fillingStep= new Vector3 (10.0f,0.0f,0.0f);
    private bool loaded=false;
    private bool loading=false;
    private int i=0;
    private Color Green= new Color(0f,255f,0f,255f);
    private Color Red= new Color(255f,0f,0f,255f);
    private Image bStartGo;
    private string[] levelList= new string[4];
    [SerializeField] private int levelNumber;

    private void Awake()
    {
        //string[] levelList={"Level1","Level2","Level3","Level4"};
    }
  private void Update() 
  {
    if (loaded && !loading)
        activateScene();
        loaded=false;  
  }

  IEnumerator LoadLevelScene(int value)
{
    AsyncOperation asyncLoadScene= SceneManager.LoadSceneAsync("Level"+ value.ToString()); // remplacer par le nom de la game scene
    asyncLoadScene.allowSceneActivation=false;
    
    while (!asyncLoadScene.isDone)
    {
        float progress=  asyncLoadScene.progress*100;
       
        if (asyncLoadScene.progress >=0.90f)
        {
            if (go)
                asyncLoadScene.allowSceneActivation=true;
        }
        yield return null;// next frame
    }
}

public void selectLevelButtons()
{
    if (!loaded&&!loading) //dont try to load the same scene while its loading
    {
        
       // levelNumber = int.Parse(GameObject.Find("Grid").GetComponentInChildren<TextMeshPro>().text);
        Debug.Log(levelNumber);
        startCoroutine(levelNumber);
    }
    else if (loaded && !loading)
        activateScene();
}

public void activateScene()
{ 
    fade=true;
}
public void startCoroutine(int value)
    {
        StartCoroutine(LoadLevelScene(value)); 
        //StartCoroutine(downloadBarre()); 
    }
public void endCoroutine()
    {
        //StartCoroutine(LoadEndScene()); 
        //StartCoroutine(quittingBarre());  
    }
}
