using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollectRope : MonoBehaviour
{

    public ItemManager ItemManager;
    public PauseMenu PauseMenu;
    public int ropeID;
    private string messageToError = "Hands are full";

    public LineRenderer lineRenderer;
    public GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if ((ItemManager.hasRope))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ItemManager.hasItem == true)
            {
                Debug.Log("Already have an item");
                PauseMenu.ShowErrorMessage(messageToError);
            }

            if (ItemManager.hasItem == false)
            {
                Debug.Log("Tavara kerätty");
                ItemManager.hasItem = true;
                ItemManager.hasRope = true;
                ItemManager.ropeImage.SetActive(true);
                ItemManager.StoreItemID(ropeID);
                lineRenderer.enabled = true;
            }  
        }
    }


    
}
