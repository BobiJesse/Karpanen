using UnityEngine;

public class WaterCollectTrigger : MonoBehaviour
{

    public PlayerMovement PlayerMovement;
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
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("water picked up");
            PlayerMovement.isHeavy = true;
            ItemManager.hasItem = true;
            ItemManager.hasWater = true;
            ItemManager.waterImage.SetActive(true);
        }
    }
}
