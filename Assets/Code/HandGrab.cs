using UnityEngine;

public class HandGrab : MonoBehaviour
{
    public Transform handGrab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handGrab.position;
        transform.rotation = handGrab.rotation;
    }
}
