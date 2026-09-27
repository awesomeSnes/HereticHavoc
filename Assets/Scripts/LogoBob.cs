using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogoBob : MonoBehaviour
{
    public GameObject up;
    public GameObject down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LogoDelay());
    }

    // Update is called once per frame
    private IEnumerator LogoDelay()
    {
        while (true)
        {
           
            up.SetActive(false);
            down.SetActive(true);
            yield return new WaitForSeconds(1);
       
            up.SetActive(true);
            down.SetActive(false);
            yield return new WaitForSeconds(1);
        }
        
    }
}
