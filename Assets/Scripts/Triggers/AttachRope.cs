using TMPro;
using UnityEngine;

public class AttachRope : MonoBehaviour
{

    public ItemManager ItemManager;
    public PauseMenu PauseMenu;
    public ParticleSystem attachHint;

    public GameObject completedRopeBridge;
    public GameObject originalRope;
    public int requiredRopeID;

    public CollectRope CollectRope;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        ItemManager = GameObject.Find("Player").GetComponent<ItemManager>();
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
                attachHint.Stop();
                ItemManager.ropeImage.SetActive(false);
                CollectRope.lineRenderer.enabled = false;
                Debug.Log("Köysi laitettu paikalleen");
                PauseMenu.ShowInformationMessage("Rope attached firmly");
            }

            if (ItemManager.hasRope == true && requiredRopeID != ItemManager.equippedItemID)
            {
                PauseMenu.ShowErrorMessage("Rope cannot attach here");
            }

        }
    }
}
