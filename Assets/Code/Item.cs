using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pick;
    public GameObject water;
    public Transform waterPos;
    public bool waterOn;
    public bool waterBool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        water = GetComponentInChildren<Water>().gameObject;
        waterBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGrab.Instance.change)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }

        if (waterOn)
        {
            SpawnWater();
        }
        else
        {
            DeleteWater();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hand") && pick)
        {
         
            PlayerGrab.Instance.isTouching = true;
            pick = false;
        }
        
        
    }

    void SpawnWater()
    {
        if (water)
        {
            water.SetActive(true);
        }
        
    }

    void DeleteWater()
    {
        if (water)
        {
            if (water.activeInHierarchy)
            {
                water.SetActive(false);
            }
        }
        
        
    }
}
