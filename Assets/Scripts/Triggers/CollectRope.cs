using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

        if(ItemManager.hasRope)
        {
            float ropeDistance = Vector3.Distance(transform.position, player.transform.position);

            if(ropeDistance > 80)
            {
                lineRenderer.enabled = false;
                PauseMenu.ShowErrorMessage("Line broke");
                ItemManager.hasRope = false;
                ItemManager.hasItem = false;
                ItemManager.ropeImage.SetActive(false);
            }
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
                PauseMenu.ShowInformationMessage("Rope Collected");
            }
        }
    }


    
}
