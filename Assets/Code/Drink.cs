using System;
using UnityEngine;
using UnityEngine.UI;

public class Drink : MonoBehaviour
{
    [SerializeField] Button btnMix;
    [SerializeField] Button btnDrink;
    [SerializeField] private GameObject cupLid;
    [SerializeField] private MenuManager manager;
    [SerializeField] private Animator animator;
    bool mixPressed;
    float subMixScore;
    float speed;
    private void Awake()
    {
        cupLid.SetActive(false);
        speed = 5;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(btnMix.interactable == true && mixPressed)
        {
            btnDrink.interactable = true;
        }
        if(manager.isPressed == true)
        {
            MixScore();
        }

        animator.SetBool("IsPressed", manager.isPressed);
    }

    public void MixPress()
    {
        mixPressed = true;
        cupLid.SetActive(true);
        subMixScore = Math.Clamp(subMixScore, -1, 10);
        StaticScore.Instance.score += Mathf.FloorToInt(subMixScore);
        Debug.Log(StaticScore.Instance.score);
    }

    public void MixScore()
    {
        subMixScore += 1 * speed * Time.deltaTime;
        //Debug.Log(subMixScore);
    }


}
