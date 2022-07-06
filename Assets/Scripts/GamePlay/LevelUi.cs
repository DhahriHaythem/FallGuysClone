using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUi : MonoBehaviour
{
    [SerializeField] private Text lifetxt;
    [SerializeField] private Text cointxt;
    [SerializeField] private Text countertxt;
    [SerializeField] private RawImage fade;

    private int counter = 4;
    private int elapsedTime = 1;

    private void OnEnable()
    {
        GameManager.instance.OnLifeChange += ChangeLifeTxt;
        GameManager.instance.OnCoinChange += ChangeCoinTxt;
    }
    private void OnDisable()
    {
        GameManager.instance.OnLifeChange -= ChangeLifeTxt;
        GameManager.instance.OnCoinChange -= ChangeCoinTxt;
    }

    private void Awake()
    {
        lifetxt.text = GameManager.instance.GetLife().ToString();
        cointxt.text = GameManager.instance.GetCoin().ToString();
    }

    private void Start()
    {
        //StartCoroutine(CounterCoroutine());
    }

    void ChangeLifeTxt()
    {
        lifetxt.text = GameManager.instance.GetLife().ToString();
    }

    void ChangeCoinTxt()
    {
        cointxt.text = GameManager.instance.GetCoin().ToString();
    }

    IEnumerator CounterCoroutine()
    {
        while(counter>0)
        {
            counter--;
            countertxt.text = counter.ToString();
            yield return new WaitForSeconds(1);
        }
        fade.gameObject.SetActive(false);
        countertxt.gameObject.SetActive(false);
    }
}
