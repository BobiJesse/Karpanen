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
    public PauseMenu PauseMenu;

    private bool isClose;
    private bool isTalking;

    public DialogueSet beforeWaterQuestDialogue;
    public DialogueSet duringWaterQuestDialogue;
    public DialogueSet beforeFertilizerQuestDialogue;
    public DialogueSet duringFertilizerQuestDialogue;
    public DialogueSet afterQuestDialogue;

    public string firstObjective;
    public string secondObjective;
    public string thirdObjective;

    public NPCState npcState = NPCState.BeforeWaterQuest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        PauseMenu.RefreshObjective(firstObjective);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            isTalking = true;
            Dialogue.gameObject.SetActive(true);
            talktooltip.SetActive(false);
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
                talktooltip.SetActive(true);
                PauseMenu.RefreshObjective(secondObjective);
                break;

            case NPCState.DuringWaterQuest:
                talktooltip.SetActive(true);
                break;

            case NPCState.BeforeFertilizerQuest:
                npcState = NPCState.DuringFertilizerQuest;
                talktooltip.SetActive(true);
                PauseMenu.RefreshObjective(thirdObjective);
                break;

            case NPCState.DuringFertilizerQuest:
                talktooltip.SetActive(true);
                break;

            case NPCState.AfterQuest:
                PauseMenu.RefreshObjective(secondObjective);
                PauseMenu.ChangeSceneFade();
                break;

            default:
                break;
        }
    }
}
