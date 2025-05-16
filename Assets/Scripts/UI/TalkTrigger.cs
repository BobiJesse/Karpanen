using UnityEngine;

public enum NPCState
{
    BeforeWaterQuest,
    DuringWaterQuest,
    BeforeFertilizerQuest,
    DuringFertilizerQuest,
    AfterQuest
}

public class TalkTrigger : MonoBehaviour
{
    public Dialogue Dialogue;
    public GameObject talktooltip;

    private bool isClose;
    private bool isTalking;

    public DialogueSet beforeWaterQuestDialogue;
    public DialogueSet duringWaterQuestDialogue;
    public DialogueSet beforeFertilizerQuestDialogue;
    public DialogueSet duringFertilizerQuestDialogue;
    public DialogueSet afterQuestDialogue;

    public NPCState npcState = NPCState.BeforeWaterQuest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            isTalking = true;
            Dialogue.gameObject.SetActive(true);
            Dialogue.SetLines(GetCurrentDialogue().lines);
        }
    }

    private DialogueSet GetCurrentDialogue()
    {
        switch (npcState)
        {
            case NPCState.BeforeWaterQuest:
                return beforeWaterQuestDialogue;
            case NPCState.DuringWaterQuest:
                return duringWaterQuestDialogue;
            case NPCState.BeforeFertilizerQuest:
                return beforeFertilizerQuestDialogue;
            case NPCState.DuringFertilizerQuest:
                return duringFertilizerQuestDialogue;
            case NPCState.AfterQuest:
                return afterQuestDialogue;
            default:
                return beforeWaterQuestDialogue;
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

    public void DialogueEnded()
    {
        Debug.Log("Dialogue ended");
        isTalking = false;

        switch (npcState)

        {
            case NPCState.BeforeWaterQuest:
                npcState = NPCState.DuringWaterQuest;
                break;

            case NPCState.DuringWaterQuest:
                break;

            case NPCState.BeforeFertilizerQuest:
                npcState = NPCState.DuringFertilizerQuest;
                break;

            case NPCState.DuringFertilizerQuest:
                break;

            case NPCState.AfterQuest:
                //Next scene
                break;

            default:
                break;
        }
    }
}
