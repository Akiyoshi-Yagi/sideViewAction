using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameClearText;
    public void GameOver()
    {
        GameOverText.SetActive(true);

    }
    public void GameClear()
    {
        GameClearText.SetActive(true);

    }


}
