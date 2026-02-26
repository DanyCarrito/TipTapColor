using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject panelNextLevel;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelMainMenu;

    public DynamicsManager dynamicsManager;

    // Start is called before the first frame update
    public void GetNextLevelPanel()
    {
        //Time.timeScale = 0f;
       GameManager.Instance.targetColorUI.SetActive(false);
       ClearBalls();
       panelNextLevel.SetActive(true);
    }

    public void StartGame()
    {
        panelMainMenu.SetActive(false);
        GameManager.Instance.StartSpawner();
    }

    public void GetNextLevel()
    {
        panelNextLevel.SetActive(false);
        panelGameOver.SetActive(false);
        GameManager.Instance.Respawner();
        GameManager.Instance.RestartValues();
        //GameManager.Instance.targetColorUI.SetActive(true);
        dynamicsManager.ActivateRandomDynamic();
    }

    public void GetGameOver()
    {
        GameManager.Instance.targetColorUI.SetActive(false);
        ClearBalls();
        panelGameOver.SetActive(true);
    }

    public void ClearBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }

}
