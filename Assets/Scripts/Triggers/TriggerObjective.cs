using UnityEngine;

public class TriggerObjective : MonoBehaviour
{
    public TalkTrigger TalkTrigger;
    public PauseMenu PauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TalkTrigger = GameObject.Find("TalkTrigger").GetComponent<TalkTrigger>();
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PauseMenu.RefreshObjective(TalkTrigger.secondObjective);
            gameObject.SetActive(false);
        }
    }
}
