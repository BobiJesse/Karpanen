using UnityEngine;

public class WaterDropTrigger : MonoBehaviour
{

    public PlayerMovement PlayerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Change player to not heavy 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("water given to plant");
            PlayerMovement.isHeavy = false;
        }
    }
}
