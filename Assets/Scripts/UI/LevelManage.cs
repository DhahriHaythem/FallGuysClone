using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManage : MonoBehaviour
{
    void Update()
    {
       if(Input.GetButton("Jump")== true)
        {
            if(Inventory.levelAt<5)
                Inventory.levelAt+=1;
            else
                Inventory.levelAt=4; 
            SceneManager.LoadScene("Qualified");
        } 
        else if (Input.GetButton("Fire1")==true)
        {
             SceneManager.LoadScene("Eliminated");
        }
    }
}
