using UnityEngine;

public class FertilizerCollect : MonoBehaviour
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

    //Collect water as you enter trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fertilizer picked up");
            PlayerMovement.isHeavy = true;
            ItemManager.hasItem = true;
            ItemManager.hasFertilizer = true;
            ItemManager.fertilizerImage.SetActive(true);
            PauseMenu.ShowInformationMessage("Fertilizer picked up");
        }

        else if (other.gameObject.tag == "Player" && ItemManager.hasItem == true)
        {
            Debug.Log("hands are full");
            PauseMenu.ShowErrorMessage("Hands are full");
        }
    }
}
