using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicsManager : MonoBehaviour
{
    public List<GameObject> dynamics;
    public GameObject currentDynamic;

    void Start()
    {
        
    }

    public void ActivateRandomDynamic()
    {
        if (currentDynamic != null)
        {
            currentDynamic.SetActive(false);
        }

        int randomDynamic = Random.Range(0, dynamics.Count);

        currentDynamic = dynamics[randomDynamic];
        currentDynamic.SetActive(true);
    }

    public void DesactivateDynamic()
    {
        if (currentDynamic != null)
        {
            currentDynamic.SetActive(false);
            currentDynamic = null;
        }
    }
}
