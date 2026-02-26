using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Rojo,
    Azul,
    Verde,
    Amarillo,
    Morado,
    Rosa
}

public class PrefabCnt : MonoBehaviour
{
    public BallType type;

    void OnMouseDown()
    {
        GameManager.Instance.CheckBall(this);
    }

}
