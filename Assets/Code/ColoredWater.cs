using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColoredWater : MonoBehaviour
{
    public List<GameObject> water;
    public slideControl slideControl;
    private float slideValue;
    private Slider slideSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slideValue = slideSlider.value;
    }
}
