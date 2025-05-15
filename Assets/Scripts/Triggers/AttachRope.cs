using TMPro;
using UnityEngine;

public class AttachRope : MonoBehaviour
{

    public ItemManager ItemManager;
    public PauseMenu PauseMenu;

    public GameObject completedRopeBridge;
    public GameObject originalRope;
    public int requiredRopeID;

    public CollectRope CollectRope;

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
        if (other.gameObject.tag == "Player" && ItemManager.hasItem)
        {
            
            if (ItemManager.hasRope == true && requiredRopeID == ItemManager.equippedItemID)
            {
                ItemManager.hasItem = false;
                ItemManager.hasRope = false;
                completedRopeBridge.SetActive(true);
                originalRope.SetActive(false);
                ItemManager.ropeImage.SetActive(false);
                CollectRope.lineRenderer.enabled = false;
                Debug.Log("Köysi laitettu paikalleen");
            }

            if(ItemManager.hasRope == true && requiredRopeID != ItemManager.equippedItemID)
            {
                PauseMenu.ShowErrorMessage("Rope cannot attach here");
            }

        }
    }
}
