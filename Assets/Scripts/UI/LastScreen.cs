using UnityEngine;
using UnityEngine.UI;

public class LastScreen : MonoBehaviour
{
    public float feidi = 0;
    public PauseMenu PauseMenu;
    private bool startFading = false;
    public Image screen;

     //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        screen = gameObject.GetComponent<Image>();
        startFading = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startFading)
        {
            feidi += Time.deltaTime * 0.07f;
            screen.color = new Color(0, 0, 0, feidi);

            if (feidi >= 1.2f)
            {
                PauseMenu.GoToMainMenu();
            }
        }
        
    }
}
