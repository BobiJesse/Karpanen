using UnityEngine;

public class WaterDropTrigger : MonoBehaviour
{

    public PlayerMovement PlayerMovement;
    public ItemManager ItemManager;
    public TalkTrigger TalkTrigger;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ItemManager = GameObject.Find("Player").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Change player to not heavy as you give water to plant 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ItemManager.hasWater)
        {
            Debug.Log("water given to plant");
            PlayerMovement.isHeavy = false;
            ItemManager.hasItem = false;
            ItemManager.hasWater = false;
            ItemManager.waterImage.SetActive(false);
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Joy");
            TalkTrigger.npcState = NPCState.BeforeFertilizerQuest;
        }

        if (other.gameObject.tag == "Player" && ItemManager.hasFertilizer && TalkTrigger.npcState == NPCState.DuringFertilizerQuest)
        {
            Debug.Log("fertilizer given to plant");
            PlayerMovement.isHeavy = false;
            ItemManager.hasItem = false;
            ItemManager.hasFertilizer = false;
            ItemManager.fertilizerImage.SetActive(false);
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Joy");
            TalkTrigger.npcState = NPCState.AfterQuest;
        }
    }
}
