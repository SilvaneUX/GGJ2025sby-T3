using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerGrab.Instance.change || PlayerGrab.Instance.objInHand.gameObject == this.gameObject)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hand") && pick)
        {
            PlayerGrab.Instance.isTouching = true;
            pick = false;
        }
    }
}
