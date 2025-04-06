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

    public List<GameObject> teethList = new List<GameObject>();
    
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
        yield return new WaitForSeconds(5f);
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
        foreach(var tooth in teethList)
        {
            tooth.SetActive(false);
        }
        act2.SetActive(false);
    }
    
    private IEnumerator Act3()
    {
        act3.SetActive(true);
        Act3Manager _act3Manager = act3.GetComponent<Act3Manager>();
        if (_act3Manager)
        {
            yield return StartCoroutine(_act3Manager.PlayAct3Sequence());
        }
        act3.SetActive(false);
    }
    
    private IEnumerator Act4()
    {
        act4.SetActive(true);
        Act4Manager _act4Manager = act4.GetComponent<Act4Manager>();
        if (_act4Manager)
        {
            yield return StartCoroutine(_act4Manager.StartAct4());
        }
        //Start Despwaning sycence
        act4.SetActive(false);
    }
    
    
}
