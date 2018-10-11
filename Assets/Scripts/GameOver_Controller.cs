using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Controller : MonoBehaviour
{
    [Tooltip("Objetos a serem desativados quando acontecer o Game Over")]
    [SerializeField] GameObject[] disableObj;
    [Tooltip("Objeto a ser ativado no game over")]
    [SerializeField] GameObject activeObj;
    [Tooltip("Objeto a ser ativado no pause")]
    [SerializeField] GameObject pauseObj;

    [Space]
    [Tooltip("Texto de Score do Game Over")]
    [SerializeField] TMPro.TMP_Text scoreGOText;

    bool pausedGame;

    void Update()
    {
        CheckPause();
    }

    /// <summary>
    /// Checa se vai ser pausado o jogo
    /// </summary>
    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if (pausedGame)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Pausa ou despausa o jogo
    /// </summary>
    public void Pause()
    {
        pausedGame = !pausedGame;
        pauseObj.SetActive(pausedGame);
    }
    /// <summary>
    /// Chama o Game Over
    /// </summary>
    /// <param name="score">Score a ser mostrado no game over</param>
    public void GameOver(int score)
    {
        foreach (GameObject dO in disableObj)
        {
            dO.SetActive(false);
        }
        activeObj.SetActive(true);
        scoreGOText.text = "Score: " + score.ToString();
    }
    /// <summary>
    /// Carrega a Scene
    /// </summary>
    /// <param name="index">Scene a ser carregada</param>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
