using UnityEngine;

public class WaterDropTrigger : MonoBehaviour
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
            gameObject.transform.Find("K�rp�nen_Kukka").GetComponent<Animator>().SetTrigger("Joy");
        }

        if (other.gameObject.tag == "Player" && ItemManager.hasFertilizer)
        {
            Debug.Log("fertilizer given to plant");
            PlayerMovement.isHeavy = false;
            ItemManager.hasItem = false;
            ItemManager.hasFertilizer = false;
            ItemManager.fertilizerImage.SetActive(false);
            gameObject.transform.Find("K�rp�nen_Kukka").GetComponent<Animator>().SetTrigger("Joy");
        }
    }
}
