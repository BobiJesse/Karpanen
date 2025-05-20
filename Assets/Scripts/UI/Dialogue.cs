using UnityEngine;
using TMPro;
using System.Collections;


public class Dialogue : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public TalkTrigger TalkTrigger;

    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.5f;

    private DialogueLine[] lines;
    private int index;

    public UnityEngine.UI.Image portraitImage;

    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        TalkTrigger = GameObject.Find("TalkTrigger").GetComponent<TalkTrigger>();
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Start disabled
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && lines != null)
        {
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
            }
        }
    }

    // Set lines and begin dialogue
    public void SetLines(DialogueLine[] newLines)
    {
        lines = newLines;
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        StartDialogue();
    }

    public void StartDialogue()
    {
        PlayerMovement.enabled = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        portraitImage.sprite = lines[index].portrait;

        foreach (char c in lines[index].text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed * 0.1f);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.Log("Next Line else triggered");
            gameObject.SetActive(false);
            PlayerMovement.enabled = true;
            lines = null;
            TalkTrigger.DialogueEnded();
        }
    }
}