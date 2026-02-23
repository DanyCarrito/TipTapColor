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
        SetRandomTargetColor();
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

    public void SetRandomTargetColor()
    {
        targetColorIndex = Random.Range(0, colors.Count);
        targetColor = colors[targetColorIndex];

        UpdateTargetColorUI();

        targetColorUI.SetActive(true);
    }

    void UpdateTargetColorUI()
    {
        targetColorText.text = colorNames[targetColorIndex];
        targetColorText.color = targetColor;
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
        amount = Mathf.Max(amount, colors.Count);

        List<Color> tempColors = new List<Color>(colors); //copia de la lista para no modificar la otra

        int index = 0;

        foreach (Color color in tempColors)
        {
            SpawnBallWithColor(color, GetGridPosition(index));
            index++;
        }

        int remaining = amount - tempColors.Count;

        for (int i = 0; i < remaining; i++)
        {
            Color randomColor = colors[Random.Range(0, colors.Count)];
            SpawnBallWithColor(randomColor, GetGridPosition(index));
            index++;
        }

    }

    void SpawnBallWithColor(Color color, Vector2 position)
    {

        GameObject ball = Instantiate(objectToSpawn, position, Quaternion.identity);

        CircleController circle = ball.GetComponent<CircleController>();
        circle.SetColor(color);

        if(color == targetColor)
        {
            remainingTargetColor++;
        }
    }

    public void Respawner()
    {
        panelManager.ClearBalls();
        remainingTargetColor = 0;
        SetRandomTargetColor();
        SpawnObject(countSprites);
        Debug.Log("Sipasa");
    }

    public void RestartValues()
    {
        score = 0;
        timerScp.StartTimer();
    }

}
