using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject AdPanel;
    public GameObject GameOverPanel;

    void Start()
    {
        MenuPanel.SetActive(false);
        AdPanel.SetActive(false);
        GameOverPanel.SetActive(false); // Start hidden
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        ShowMenuPanel();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
    }

    public void ShowAdPanel()
    {
        AdPanel.SetActive(true);
    }

    public void goBack()
    {
        AdPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
