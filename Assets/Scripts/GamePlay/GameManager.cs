using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    
    //[SerializeField] private Button playButton;

    public static GameManager instance;
    private Data data;

    private bool gameOver = false;


    public Action OnCoinChange;
    public Action OnLifeChange;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if(!PlayerPrefs.HasKey("coinCount"))
            {
                Reset();
            }
            load();
        }
        else 
        {
            Destroy(gameObject);
        }

        //playButton.onClick.AddListener(LoadLevel);
    }

    /*public void LoadLevel()
    {
        SceneManager.LoadScene("Level"+ data.level);
    }*/

    public void Reset()
    {
        data.coinCount = 0;
        data.lifeCount = 3;
        data.level = 1;
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coinCount", data.coinCount);
        PlayerPrefs.SetInt("lifeCount", data.lifeCount);
        PlayerPrefs.SetInt("level", data.level);
        PlayerPrefs.Save();
    }

    public void load()
    {
        data.coinCount = PlayerPrefs.GetInt("coinCount");
        data.lifeCount = PlayerPrefs.GetInt("lifeCount");
        data.level = PlayerPrefs.GetInt("level");
    }

    public void AddCoin()
    {
        data.coinCount += 1;
        Save();
        OnCoinChange();
    }

    public void addLife()
    {
        data.lifeCount += 1;
        Save();
        OnLifeChange();
    }
    public void addLevel()
    {
        data.level += 1;
        Save();
    }


    public int GetCoin()
    {
        return data.coinCount;
    }

    public int GetLife()
    {
        return data.lifeCount;
    }

    public int GetLevel()
    {
        return data.level;
    }

    public void GameOver()
    {
        gameOver = true;
        data.lifeCount -= 1;
        OnLifeChange();
        Save();
        StartCoroutine(loadScene());
    }

    public IEnumerator DestroyObject(AudioSource a)
    {
        while(a.isPlaying)
        {
            yield return null;
        }
        Destroy(a.gameObject);
    }

    IEnumerator loadScene()
    {
        int count=0;
        while(count<3)
        {
            count++;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Eliminated");
    }

    
}
