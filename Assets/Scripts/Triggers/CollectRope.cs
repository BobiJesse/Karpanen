using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CollectRope : MonoBehaviour
{
    public static CollectRope activeRope;

    public ItemManager ItemManager;
    public PauseMenu PauseMenu;
    public int ropeID;
    private string messageToError = "Hands are full";
    public ParticleSystem attachHint;

    public LineRenderer lineRenderer;
    public GameObject lineStart;
    public GameObject player;
    private float ropeDistance;

    public float maxRopeDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
        PauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        ItemManager = GameObject.Find("Player").GetComponent<ItemManager>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeRope != this) return;
        Debug.Log(ropeDistance);

        if ((ItemManager.hasRope))
        {
            lineRenderer.SetPosition(1, player.transform.position);

            ropeDistance = Vector3.Distance(transform.position, player.transform.position);

            if(ropeDistance > maxRopeDistance)
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
                attachHint.Play();
                ItemManager.StoreItemID(ropeID);
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, lineStart.transform.position);
                PauseMenu.ShowInformationMessage("Rope Collected");

                activeRope = this;
            }
        }
    }


    
}
