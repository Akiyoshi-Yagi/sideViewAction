using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameClearText;
    [SerializeField] Text scoreText;

    //SE
    [SerializeField] AudioClip gameClearSE;
    [SerializeField] AudioClip gameOverSE;
    AudioSource audioSource;



    const int MAX_VALUE = 9999;
    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_VALUE)
        {
            score = MAX_VALUE;
        }
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        GameOverText.SetActive(true);
        audioSource.PlayOneShot(gameOverSE);
        Invoke("RestartScene", 1.2f);

    }
    public void GameClear()
    {
        GameClearText.SetActive(true);
        audioSource.PlayOneShot(gameClearSE);
        Invoke("RestartScene", 1.0f);
    }

    void RestartScene()
    {
        //現在のシーンを取得する
        //現在のシーンをスタートさせる
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }


}
