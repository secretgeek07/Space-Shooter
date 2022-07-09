using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        //FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game Over");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
