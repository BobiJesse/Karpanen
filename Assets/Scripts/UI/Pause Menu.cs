using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject verifyMenu;
    public GameObject errorMessage;
    public GameObject infoMessage;
    public GameObject objectiveText;
    public GameObject controlScreen;
    public GameObject buttons;
    public GameObject narrativeImage;

    public bool gamePaused;
    public Image fadeStartImage;
    public bool isFading;
    public float fadeTimer;
    public Dialogue Dialogue;
    public ShowHelp ShowHelp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFading = true;
        Time.timeScale = 1f;
        gamePaused = false;
        pauseMenu.SetActive(false);
        fadeTimer = 1f;
        ShowHelp = GameObject.Find("InfoTrigger").GetComponent<ShowHelp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused && ShowHelp.isActive == false)
        {
            PauseTheGame();
        }

        if(fadeTimer >= 0 && isFading)
        {
            fadeTimer -= Time.deltaTime * 0.5f;
            fadeStartImage.color = new Color(0, 0, 0, fadeTimer);
        }

        else if (fadeTimer <= 1 && !isFading)
        {
            fadeTimer += Time.deltaTime;
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

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeSceneFade()
    {
        isFading = false;
        Invoke(nameof(ShowNarrative), 1.2f);
    }

    public void ShowNarrative()
    {
        narrativeImage.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void ShowControls()
    {
        controlScreen.gameObject.SetActive(true);
        buttons.gameObject.SetActive(false);
    }

    public void HideControls()
    {
        controlScreen.gameObject.SetActive(false);
        buttons.gameObject.SetActive(true);
    }
    
    public void RefreshObjective(string nextObjective)
    {
        objectiveText.GetComponent<TextMeshProUGUI>().text = nextObjective;
    }
}
