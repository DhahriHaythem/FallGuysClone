using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
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
    private void Start() {
        
    }
  private void Update() 
  {
    if (loaded && !loading)
        activateScene();
        loaded=false;  
  }
IEnumerator downloadBarre()
{
    loading=true; // busy loading...  dont try to load another scene
    GameObject.Find("BarText").GetComponent<Text>().text = "LOADING..." ;

    while(i<=100)
    {
    yield return new WaitForSeconds(0.1f);
    GameObject.Find("RedFillingBar").transform.position += fillingStep;
    GameObject.Find("LoadingBar").transform.position += fillingStep;
  
    i++;
    }
    i=0;
    loading=false; 
   // GameObject.Find("StartGo").GetComponentInChildren<Text>().text = "Go!!!" ;
    GameObject.Find("BarText").GetComponent<Text>().text = "GET READY TO RUN!!!" ;
    GameObject.Find("Start").GetComponent<Image>().color= Color.green;
    GameObject.Find("RedFillingBar").GetComponent<Image>().color= Green;
    loaded=true; // completion du loading
}

IEnumerator quittingBarre()
{
    loading=true; // busy loading...  dont try to load another scene
    GameObject.Find("BarText").GetComponent<Text>().text = "QUITTING..." ;

    while(i<=100)
    {
    yield return new WaitForSeconds(0.1f);
    GameObject.Find("RedFillingBar").transform.position += fillingStep;
    GameObject.Find("LoadingBar").transform.position += fillingStep;
    i++;
    }
    i=0;
    loading=false; 
    GameObject.Find("BarText").GetComponent<Text>().text = "YOU'RE ABOUT TO LEAVE..." ;
    GameObject.Find("RedFillingBar").GetComponent<Image>().color= Red;
    loaded=true; // completion du loading
}
IEnumerator LoadGameScene()
{
    AsyncOperation asyncLoadScene= SceneManager.LoadSceneAsync("LevelSelector"); // remplacer par le nom de la game scene
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
IEnumerator LoadEndScene()
{
    AsyncOperation asyncLoadScene= SceneManager.LoadSceneAsync("QuitGame");// remplacer par le nom de la endScene
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
public void buttonStartGo()
{
    if (!loaded&&!loading) //dont try to load the same scene while its loading
    {
        startCoroutine();
        GameObject.Find("Start").GetComponent<Image>().color= Color.red;
    }
    else if (loaded && !loading)
        activateScene();
}  

public void buttonQuit()
{
    //SceneManager.LoadScene("QuitGame");

    if (!loaded&&!loading) //dont try to load the same scene while its loading
    {
        endCoroutine();
        GameObject.Find("Quit").GetComponent<Image>().color= Color.red;
    }
    else if (loaded && !loading)
        activateScene();
}  

public void activateScene()
{ 
    fade=true;
}
public void startCoroutine()
    {
        StartCoroutine(LoadGameScene()); 
        StartCoroutine(downloadBarre()); 
    }
public void endCoroutine()
    {
        StartCoroutine(LoadEndScene()); 
        StartCoroutine(quittingBarre());  
    }
}