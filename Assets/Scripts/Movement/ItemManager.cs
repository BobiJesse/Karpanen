using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool hasRope;
    public bool hasWater;
    public bool hasItem;

    public GameObject ropeImage;
    public GameObject waterImage;

    public int equippedItemID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreItemID(int iD)
    {
        equippedItemID = iD;
    }

}
