using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ManagerObjective : MonoBehaviour
{
    [SerializeField] Image[] img;
    [SerializeField] TextMeshProUGUI[] percent;

    public static int[] Bottles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bottles = new int[5];
        for(int i = 0; i < Bottles.Length; i++)
        {
            Bottles[i] = 0;
        }
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Randomize()
    {
        for (int i = 0; i < Bottles.Length; i++)
        {
            Bottles[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            img[i].color = Color.clear;
            percent[i].enabled = false;
        }
        int RandomBottle = Random.Range(2, 5);
        int val = 0;
        for(int i = 0; i < RandomBottle; i++)
        {
            percent[i].enabled = true;
            // count random
            int rnd = Random.Range(1, 100-val);
            if(i != RandomBottle-1)
            {
                // Debug.Log("pass");
                val += rnd;
                percent[i].text = rnd.ToString();
                // save to each correspondent bottle
                Bottles[i] = rnd;
            }else if(i == RandomBottle-1)
            {
                rnd = 100 - val;
                // Debug.Log(rnd);
                percent[i].text = rnd.ToString();
                // save to last bottle
                Bottles[i] = rnd;
            }
            img[i].color = Color.white;
        }

        foreach(int i in Bottles)
        {
            Debug.Log(i);
        }
    }
}
