using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Run()); 
    }

   public IEnumerator Run() 
    { 
        Debug.Log("Quitting..");
        yield return new WaitForSeconds(10.0f);
        Quitting();
    } 
    public void Quitting()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); 
    }
}
