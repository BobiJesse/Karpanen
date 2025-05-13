using UnityEngine;

public class AttachRope : MonoBehaviour
{

    public ItemManager ItemManager;
    public GameObject completedRopeBridge;
    public GameObject originalRope;

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
        if (other.gameObject.tag == "Player" && ItemManager.hasRope)
        {
            Debug.Log("Köysi laitettu paikalleen");
            if (ItemManager.hasItem == true)
            {
                ItemManager.hasItem = false;
                ItemManager.hasRope = false;
                completedRopeBridge.SetActive(true);
                originalRope.SetActive(false);
            }

        }
    }
}
