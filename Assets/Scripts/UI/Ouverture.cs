using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Ouverture : MonoBehaviour
{
    AsyncOperation asyncLoadScene;
    CanvasGroup canGroup;
    [SerializeField] private float speed=0.2f;
    [SerializeField] private PlayerInput uiInput;
    private InputAction loadAction;
    //public  float a;


    private void Awake()
    {
        canGroup=GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        loadAction = uiInput.actions["load"];
    }
    void Start()
    {
        Inventory.levelAt=3;
        StartCoroutine(LoadIntroScene());
    }
    private void Update() 
    {
        if(loadAction.ReadValue<float>()!=0f)
        {
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator LoadIntroScene()
    {
        asyncLoadScene=SceneManager.LoadSceneAsync("StartGame");
        asyncLoadScene.allowSceneActivation=false;
        yield return null;
    }
    IEnumerator FadeOut()
    {
        canGroup.alpha = 1;
        while(canGroup.alpha >0)
        {
            canGroup.alpha -= Time.deltaTime *speed;
            yield return null;
        }
        asyncLoadScene.allowSceneActivation=true;
    }
}