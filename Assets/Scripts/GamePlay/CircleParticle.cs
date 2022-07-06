using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleParticle : MonoBehaviour
{
    private int count = 0;
    public void loadLevel(int i)
    {
        count++;
        if(count>5)
        {
            SceneManager.LoadScene("Qualified");
        }
        
    }
}
