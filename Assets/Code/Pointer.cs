using UnityEngine;

public class Pointer : MonoBehaviour
{
    public LayerMask mask;
    public Transform handGrab;
    public PlayerGrab playerGrab;

    GameObject currentObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(screenPoint.x, screenPoint.y, -0.9f);
        Raycast();
    }

    

    void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.3f, mask);
            if (hit)
            {
                Debug.Log("Hit");
                if (hit.GetComponent<Item>())
                {
                    
                    if (currentObj)
                    {
                        playerGrab.change = true;
                        playerGrab.objInHand = hit.gameObject;

                        hit.GetComponent<Item>().pick = true;
                    }
                    else
                    {
                        currentObj = hit.gameObject;
                        playerGrab.objInHand = hit.gameObject;
                        hit.GetComponent<Item>().pick = true;
                    }
                    
                }
            }
        }
    }
}
