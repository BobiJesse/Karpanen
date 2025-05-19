using UnityEngine;

public class WaterCollectTrigger : MonoBehaviour
{

    public PlayerMovement PlayerMovement;
    public PauseMenu PauseMenu;
    public ItemManager ItemManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ItemManager = GameObject.Find("Player").GetComponent<ItemManager>();
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collect water as you enter trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && ItemManager.hasItem == false)
        {
            Debug.Log("water picked up");
            PlayerMovement.isHeavy = true;
            ItemManager.hasItem = true;
            ItemManager.hasWater = true;
            ItemManager.waterImage.SetActive(true);
            PauseMenu.ShowInformationMessage("Water collected");
        }

        else if (other.gameObject.tag == "Player" && ItemManager.hasItem == true)
        {
            Debug.Log("hands are full");
            PauseMenu.ShowErrorMessage("Hands are full");
        }
    }
}
