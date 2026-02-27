using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float score;
    [SerializeField] private float minScore;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeY;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] int countSprites = 4;
    [SerializeField] int columns = 4;
    [SerializeField] float spacing = 1.2f;
    [SerializeField] Vector2 gridOrigin = new Vector2(-3, 2);

    public GameObject targetColorUI;
    public TMP_Text targetColorText;
    [SerializeField] private TMP_Text scoreText;

    public int remainingTargetColor = 0;

    public PanelManager panelManager;
    public Timer timerScp;

    public BallType currentTarget;
    public BallType[] availableTypes;
    public GameObject[] prefabs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (scoreText != null)
        {
            int displayScore = Mathf.Max(0, Mathf.FloorToInt(score));
            scoreText.text = displayScore.ToString() + "/30";
        }
    }

    Color GetColorFromType(BallType type)
    {
        switch (type)
        {
            case BallType.Rojo:
                return new Color(0.86f, 0.15f, 0.15f);

            case BallType.Verde:
                return new Color(0.18f, 0.80f, 0.44f);

            case BallType.Azul:
                return new Color(0.20f, 0.60f, 0.86f);

            case BallType.Amarillo:
                return new Color(1f, 0.84f, 0f);

            case BallType.Morado:
                return new Color(0.67f, 0.57f, 1f);

            case BallType.Rosa:
                return new Color(1f, 0.41f, 0.71f);

            default:
                return Color.white;
        }
    }

    public void StartSpawner()
    {
        SetRandomTarget();
        SpawnObject(countSprites);
        timerScp.StartTimer();
    }

    Vector2 GetGridPosition(int gridIndex)
    {
        int row = gridIndex / columns;
        int column = gridIndex % columns;

        float x = gridOrigin.x + column * spacing;
        float y = gridOrigin.y - row * spacing;

        return new Vector2(x, y);
    }

    public void SetRandomTarget()
    {
        int randomIndex = Random.Range(0, availableTypes.Length);
        currentTarget = availableTypes[randomIndex];

        UpdateTargetColorUI();
        targetColorText.text = currentTarget.ToString();
        targetColorUI.SetActive(true);
    }

    public void CheckBall(PrefabCnt clickedBall)
    {
        if (clickedBall.type == currentTarget)
        {
            IncreaseScore(1);
            remainingTargetColor--;
            Destroy(clickedBall.gameObject);

            if (remainingTargetColor <= 0)
            {
                Respawner();
            }
        }
        else
        {
            Debug.Log("Incorrecto");
        }
    }

    void UpdateTargetColorUI()
    {
        targetColorText.text = currentTarget.ToString();
        targetColorText.color = GetColorFromType(currentTarget);
    }


    public void IncreaseScore(float amount)
    {
        if(score < minScore)
        {
            score++;
        }
        else
        {
            panelManager.GetNextLevelPanel();
            timerScp.ClearTimeBar();
        }

    }

    public void SpawnObject(int amount)
    {
        if (prefabs.Length == 0)
        {
            Debug.LogError("No hay prefabs asignados.");
            return;
        }

        amount = Mathf.Max(amount, prefabs.Length);

        int index = 0;

        foreach (GameObject prefab in prefabs)
        {
            SpawnPrefab(prefab, GetGridPosition(index));
            index++;
        }

        int remaining = amount - prefabs.Length;

        for (int i = 0; i < remaining; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject randomPrefab = prefabs[randomIndex];

            SpawnPrefab(randomPrefab, GetGridPosition(index));
            index++;
        }
    }

    void SpawnPrefab(GameObject prefab, Vector2 position)
    {
        GameObject ball = Instantiate(prefab, position, Quaternion.identity);

        PrefabCnt circle = ball.GetComponent<PrefabCnt>();

        if (circle.type == currentTarget)
        {
            remainingTargetColor++;
        }
    }

    public void Respawner()
    {
        panelManager.ClearBalls();
        remainingTargetColor = 0;
        SetRandomTarget();
        SpawnObject(countSprites);
        Debug.Log("Sipasa");
    }

    public void RestartValues()
    {
        score = 0;
        timerScp.StartTimer();
    }

}
