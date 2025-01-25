using System;
using UnityEngine;
using UnityEngine.UI;

public class BottlePour : MonoBehaviour
{
    public Image image; // Reference to the Image component
    public float percentage;

    [SerializeField] float bottleA;
    [SerializeField] float bottleB;
    [SerializeField] float bottleC;
    [SerializeField] int bottleD;
    [SerializeField] int bottleE;

    [SerializeField] float SumBottlePercent = 0f;
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            if(percentage < 19f){
                    percentage += 1 * Time.deltaTime;
                    float percentofFilling = (float)Mathf.FloorToInt(percentage) / 19 * (100);
                    bottleA += 1f * Time.deltaTime;
                    float percentofFillingA = (float)Mathf.FloorToInt(bottleA) / 19 * (100);
                    Debug.Log("A : " + Mathf.FloorToInt(percentofFillingA));
                    Debug.Log(Mathf.FloorToInt(percentage)+ " - "+ Mathf.FloorToInt(percentofFilling) + "%");
                    Resize(5f, new Vector3(1f, 0f, 0f));
            }
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            if(percentage < 19f){
                    percentage += 1 * Time.deltaTime;
                    float percentofFilling = (float)Mathf.FloorToInt(percentage) / 19 * (100);
                    bottleB += 1f * Time.deltaTime;
                    float percentofFillingB = (float)Mathf.FloorToInt(bottleB) / 19 * (100);
                    Debug.Log("B : " + Mathf.FloorToInt(percentofFillingB));
                    Debug.Log(Mathf.FloorToInt(percentage)+ " - "+ Mathf.FloorToInt(percentofFilling) + "%");
                    Resize(5f, new Vector3(1f, 0f, 0f));
            }
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            if(percentage < 19f){

                    SumBottlePercent += 1f * Time.deltaTime;
                    float percentofFilling = (float)Mathf.FloorToInt(SumBottlePercent) / 19 * (100);
                    bottleC += 1f * Time.deltaTime;
                    float percentofFillingC = (float)Mathf.FloorToInt(bottleC) / 19 * (100);
                    Debug.Log("C : " + Mathf.FloorToInt(percentofFillingC));
                    Debug.Log(Mathf.FloorToInt(SumBottlePercent)+ " - "+ Mathf.FloorToInt(percentofFilling) + "%");
                    Resize(5f, new Vector3(1f, 0f, 0f));
                    percentage += 1 * Time.deltaTime;
            }
        }
    }

    public void Resize(float amount, Vector3 direction)
    {
        // image.rectTransform.position +=  new Vector3(0f, 0.1f, 0f) * amount / 3; // Move the object in the direction of scaling, so that the corner on ther side stays in place
        image.rectTransform.localScale += direction * (amount * Time.deltaTime); // Scale object in the specified direction
    }
}
