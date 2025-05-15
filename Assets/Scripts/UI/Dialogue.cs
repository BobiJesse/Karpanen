using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework;

public class Dialogue : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public string[] currentLines;
    public float textSpeed;

    public int index;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        PlayerMovement.enabled = false;
        isActive = true;
        index = 0;
        StartCoroutine(TypeLine(lines));
    }

    IEnumerator TypeLine(string[] lines)
    {
        foreach (char c in lines[index].ToCharArray())
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
            StartCoroutine(TypeLine(lines));
        }
        else
        {
            gameObject.SetActive(false);
            isActive = false;
            PlayerMovement.enabled = true;
        }
    }
}
