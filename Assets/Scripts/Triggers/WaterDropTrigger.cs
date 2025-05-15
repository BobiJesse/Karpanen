using UnityEngine;

public class WaterDropTrigger : MonoBehaviour
{

    public PlayerMovement PlayerMovement;
    public PauseMenu PauseMenu;
    public ItemManager ItemManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            PauseMenu.ShowInformationMessage("Water given to the plant");
        }


        if (other.gameObject.tag == "Player" && ItemManager.hasFertilizer)
        {
            Debug.Log("fertilizer given to plant");
            PlayerMovement.isHeavy = false;
            ItemManager.hasItem = false;
            ItemManager.hasFertilizer = false;
            ItemManager.fertilizerImage.SetActive(false);
            PauseMenu.ShowInformationMessage("Fertilizer given to the plant");
        }

        else if(other.gameObject.tag == "Player" && ItemManager.hasFertilizer == false && ItemManager.hasWater == false)
        {
            PauseMenu.ShowErrorMessage("Need Water/Fertilizer for Flower");
        }
    }
}
