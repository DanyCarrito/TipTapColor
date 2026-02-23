using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicHideColor : MonoBehaviour
{
    public float minTimeChange;
    public float maxTimeChange;
    public float timeHidden;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HIDE");
        float timeRandom = Random.Range(minTimeChange, maxTimeChange);
        StartCoroutine(StartHideColorAfterSeconds(5f));
    }

    public void HideTargetColor()
    {
        GameManager.Instance.targetColorUI.SetActive(false);
    }

    public void ShowTargetColor()
    {
        GameManager.Instance.targetColorUI.SetActive(true);
    }

    IEnumerator StartHideColorAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        HideTargetColor();

        yield return new WaitForSeconds(timeHidden);

        ShowTargetColor();
    }
}
