using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFaint : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PauseMenu PauseMenu;

    public Image faintScreen;

    private bool isFainting;
    public float faintTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        faintScreen = GameObject.Find("faintImage").GetComponent<Image>();
        isFainting = false;
        faintScreen.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFainting && faintTime <= 1)
        {
            faintTime += Time.deltaTime * 0.3f;
            faintScreen.color = new Color(0, 0, 0, faintTime);
        }
        else if(!isFainting && faintTime > 0)
        {
            faintTime -= Time.deltaTime * 0.3f;
            faintScreen.color = new Color(0, 0, 0, faintTime);
        }

        if(faintTime >= 1)
        {
            PauseMenu.ReloadTheScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isFainting=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isFainting=false;
        }
    }

}
