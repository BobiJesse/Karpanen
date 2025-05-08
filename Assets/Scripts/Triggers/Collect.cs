using UnityEngine;

public class Collect : MonoBehaviour
{

    public ItemManager ItemManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Tavara kerätty");
            ItemManager.hasRope = true;
        }
    }
}
