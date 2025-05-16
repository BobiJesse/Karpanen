using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public Dialogue Dialogue;
    public GameObject talktooltip;
    public string[] lines;

    private bool isClose;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClose && Input.GetKeyDown(KeyCode.E))
        {
            Dialogue.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            talktooltip.SetActive(true);
            isClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            talktooltip.SetActive(false);
            isClose = false;
        }
    }
}
