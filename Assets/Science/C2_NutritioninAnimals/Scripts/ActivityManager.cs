using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject act1;
    public GameObject act2;
    public GameObject act3;
    public GameObject act4;
    
    void Start()
    {
        act1.SetActive(false);
        act2.SetActive(false);
        act3.SetActive(false);
        act4.SetActive(false);
        StartCoroutine(StartActivity());
    }
    
    private IEnumerator StartActivity()
    {
        yield return Act1();
        yield return Act2();
        yield return Act3();
        yield return Act4();
    }

    private IEnumerator Act1()
    {
        act1.SetActive(true);
        Act1Manager _act1manager = act1.GetComponent<Act1Manager>();
        if (_act1manager)
        {
            yield return StartCoroutine(_act1manager.PlayAct1Sequence());
        }
        //Start Despwaning sycence 
        act1.SetActive(false);
    }
    
    private IEnumerator Act2()
    {
        act2.SetActive(true);
        Act2Manager _act2Manager = act2.GetComponent<Act2Manager>();
        if (_act2Manager)
        {
            yield return StartCoroutine(_act2Manager.StartAct2());
        }
        //Start Despwaning sycence 
        act2.SetActive(false);
    }
    
    private IEnumerator Act3()
    {
        Debug.Log("Act3");
        yield return new WaitForSeconds(5);
    }
    
    private IEnumerator Act4()
    {
        Debug.Log("Act4");
        yield return new WaitForSeconds(5);
    }
    
    
}
