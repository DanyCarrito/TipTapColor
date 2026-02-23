using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class DynamicChangeColor : MonoBehaviour
{
    public float minTimeChange;
    public float maxTimeChange;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CHANGE");
        float timeRandom = Random.Range(minTimeChange, maxTimeChange);
        StartCoroutine(StartChangeColorAfterSeconds(timeRandom));
    }

    public void ChangeTargetColor()
    {
        GameManager.Instance.SetRandomTargetColor();

    }

    IEnumerator StartChangeColorAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeTargetColor();
    }

}
