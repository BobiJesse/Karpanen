using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject verifyMenu;
    public bool gamePaused;
    public GameObject errorMessage;
    public GameObject infoMessage;
    public Image fadeStartImage;
    public float fadeTimer;
    public Dialogue Dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        pauseMenu.SetActive(false);
        fadeTimer = 1f;
        Invoke(nameof(StartDialogue), 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            PauseTheGame();
        }

        if(fadeTimer >= 0)
        {
            fadeTimer -= Time.deltaTime * 0.5f;
            fadeStartImage.color = new Color(0, 0, 0, fadeTimer);
        }
    }

    private void PauseTheGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

    public void DeleteErrorMessage()
    {
        errorMessage.SetActive(false);
    }

    public void ShowErrorMessage(string errorText)
    {
        Debug.Log(errorText);
        errorMessage.GetComponent<TextMeshProUGUI>().text = errorText;
        errorMessage.SetActive(true);
        Invoke(nameof(DeleteErrorMessage), 2f);
    }

    public void ShowInformationMessage(string infoText)
    {
        Debug.Log(infoText);
        infoMessage.GetComponent<TextMeshProUGUI>().text = infoText;
        infoMessage.SetActive(true);
        Invoke(nameof(DeleteInformationMessage), 2f);
    }

    public void DeleteInformationMessage()
    {
        infoMessage.SetActive(false);
    }

    public void ReloadTheScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void StartDialogue()
    {
        Dialogue.gameObject.SetActive(true);
    }
}
