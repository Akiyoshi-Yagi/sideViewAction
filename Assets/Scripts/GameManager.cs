using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameClearText;

    public void GameOver()
    {
        GameOverText.SetActive(true);
        Invoke("RestartScene", 1.5f);

    }
    public void GameClear()
    {
        GameClearText.SetActive(true);
        Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        //現在のシーンを取得する
        //現在のシーンをスタートさせる
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }


}
