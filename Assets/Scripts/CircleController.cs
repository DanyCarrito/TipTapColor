using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public Color myColor;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        myColor = color;
        spriteRenderer.color = color;
    }
    void OnMouseDown()
    {
        if (myColor == GameManager.Instance.targetColor)
        {
            GameManager.Instance.IncreaseScore(1);
            GameManager.Instance.remainingTargetColor--;
            Destroy(gameObject);

            if(GameManager.Instance.remainingTargetColor <= 0)
            {
                GameManager.Instance.Respawner();
            }
        }
        else
        {
            Debug.Log("Color incorrecto");
        }      
    }


}
