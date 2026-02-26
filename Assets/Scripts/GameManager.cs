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

    public Color targetColor;
    public int targetColorIndex;
    public int remainingTargetColor = 0;

    public PanelManager panelManager;
    public Timer timerScp;

    public BallType currentTarget;
    public BallType[] availableTypes;
    public GameObject[] prefabs;

    public List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow
    };

    public List<string> colorNames;

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

    //public void SetRandomTargetColor()
    //{
    //    targetColorIndex = Random.Range(0, colors.Count);
    //    targetColor = colors[targetColorIndex];

    //    UpdateTargetColorUI();

    //    targetColorUI.SetActive(true);
    //}
    public void SetRandomTarget()
    {
        int randomIndex = Random.Range(0, availableTypes.Length);
        currentTarget = availableTypes[randomIndex];

        targetColorText.text = currentTarget.ToString();
        targetColorUI.SetActive(true);
        UpdateTargetColorUI();
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
        //targetColorText.text = colorNames[targetColorIndex];
        //targetColorText.color = targetColor;
        targetColorText.text = currentTarget.ToString();
        targetColorText.color = GetColorFromType(currentTarget);
    }

    Color GetColorFromType(BallType type)
    {
        switch (type)
        {
            case BallType.Rojo:
                return Color.red;

            case BallType.Verde:
                return Color.green;

            case BallType.Azul:
                return Color.blue;

            case BallType.Amarillo:
                return Color.yellow;

            case BallType.Morado:
                return new Color(0.6f, 0f, 0.8f);

            case BallType.Rosa:
                return new Color(1f, 0.41f, 0.71f);

            default:
                return Color.white;
        }
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

    //public void SpawnObject(int amount)
    //{
    //    amount = Mathf.Max(amount, colors.Count);

    //    List<Color> tempColors = new List<Color>(colors); //copia de la lista para no modificar la otra

    //    int index = 0;

    //    foreach (Color color in tempColors)
    //    {
    //        SpawnBallWithColor(color, GetGridPosition(index));
    //        index++;
    //    }

    //    int remaining = amount - tempColors.Count;

    //    for (int i = 0; i < remaining; i++)
    //    {
    //        Color randomColor = colors[Random.Range(0, colors.Count)];
    //        SpawnBallWithColor(randomColor, GetGridPosition(index));
    //        index++;
    //    }

    //}

    public void SpawnObject(int amount)
    {
        if (prefabs.Length == 0)
        {
            Debug.LogError("No hay prefabs asignados.");
            return;
        }

        amount = Mathf.Max(amount, prefabs.Length);

        int index = 0;

        // Primero: aseguramos que salga al menos uno de cada tipo
        foreach (GameObject prefab in prefabs)
        {
            SpawnBall(prefab, GetGridPosition(index));
            index++;
        }

        int remaining = amount - prefabs.Length;

        // Luego rellenamos con random
        for (int i = 0; i < remaining; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject randomPrefab = prefabs[randomIndex];

            SpawnBall(randomPrefab, GetGridPosition(index));
            index++;
        }
    }

    void SpawnBall(GameObject prefab, Vector2 position)
    {
        GameObject ball = Instantiate(prefab, position, Quaternion.identity);

        PrefabCnt circle = ball.GetComponent<PrefabCnt>();

        if (circle.type == currentTarget)
        {
            remainingTargetColor++;
        }
    }

    //void SpawnBallWithColor(Color color, Vector2 position)
    //{

    //    GameObject ball = Instantiate(objectToSpawn, position, Quaternion.identity);

    //    CircleController circle = ball.GetComponent<CircleController>();
    //    circle.SetColor(color);

    //    if(color == targetColor)
    //    {
    //        remainingTargetColor++;
    //    }
    //}

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
