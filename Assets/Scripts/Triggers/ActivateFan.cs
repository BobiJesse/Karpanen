using UnityEngine;

public class ActivateFan : MonoBehaviour
{
    private bool isPressed;

    public ParticleSystem raidPilvi;
    public GameObject cloudTrigger;
    public ParticleSystem tuuli;

    public ItemManager ItemManager;
    public PlayerMovement PlayerMovement;
    public PauseMenu PauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPressed)
        {
            if(ItemManager.hasWater == true && PlayerMovement.isHeavy == true)
            {
                isPressed = true;
                Debug.Log("fan activated");
                GameObject.Find("K�rp�nen_Fan").GetComponent<Animator>().SetTrigger("Spin");
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
                tuuli.Play();
                Invoke(nameof(BlowGasOut), 4f);
                PauseMenu.ShowInformationMessage("Fan activated");
            }

            else
            {
                Debug.Log("not enough weight");
                PauseMenu.ShowErrorMessage("Need more weight");
            }
            

        }
    }

    private void BlowGasOut()
    {
        raidPilvi.Stop();
        cloudTrigger.SetActive(false);
    }
}
