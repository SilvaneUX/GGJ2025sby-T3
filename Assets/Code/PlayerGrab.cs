using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public static PlayerGrab Instance;
    public Transform player;
    public Transform playerHand;
    public Transform pointer;
    public Transform playerHandModel;
    public Transform objectGrab;
    public GameObject objInHand;

    public float timeSmooth;
    public Vector3 originTransform;
    public GameObject originGameobject;

    public bool isTouching;
    public bool change;
    bool hits;
    bool isComplete;
    bool returnComplete;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
        if (change)
        {
            Returning();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if ((objInHand && !isComplete) || change)
            {
                if (!change)
                {
                    originTransform = objInHand.transform.position;
                    originGameobject = objInHand;
                }
                else
                {
                    hits = true;
                }
                
                Smooth();
            } 
        }

        if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(Action());
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopCoroutine(Action());
            BottleServing(false);
        }
            
                

        if (hits && !change)
        {
            objInHand.transform.position = objectGrab.transform.position;
        }
        else if (hits && originGameobject)
        {
            originGameobject.transform.position = objectGrab.transform.position;
        }
    }

    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.1f);
        BottleServing(true);
        StartCoroutine(Action());
        // do stuff
    }

    void Smooth()
    {

        timeSmooth = 0;
        StartCoroutine(SmoothDamp());

    }

    IEnumerator SmoothDamp()
    {
        IncreaseObjectSize(1, new Vector3(0.01f, 0, 0));
        yield return new WaitForSeconds(0.01f);
        timeSmooth += Time.deltaTime;
        if (timeSmooth < 2 && !isTouching && !returnComplete)
        {
            StartCoroutine(SmoothDamp());
        }
        else if ((objectGrab.position.x < objInHand.transform.position.x) && change)
        {
            timeSmooth = 0;
            hits = false;
            isTouching = false;
            isComplete = true;
            if (change)
            {
                playerHand.eulerAngles = new Vector3(0, 0, 0);

                isComplete = false;
                change = false;
                returnComplete = false;
                originTransform = objInHand.transform.position;
                originGameobject = objInHand;
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SmoothOut());
            }
            change = false;
        }
        else if (change)
        {
            timeSmooth = 0;
            hits = false;
            isTouching = false;
            isComplete = true;
            if (change)
            {
                playerHand.eulerAngles = new Vector3(0, 0, 0);
                
                isComplete = false;
                change = false;
                returnComplete = false;
                originTransform = objInHand.transform.position;
                originGameobject = objInHand;
                Smooth();
            }
            change = false;
        }
        else
        {
            timeSmooth = 0;
            hits = true;
            StartCoroutine(SmoothOut());
            
        }
    }

    IEnumerator SmoothOut()
    {
        Debug.Log("return");
        DecreaseObjectSize(1, new Vector3(0.01f, 0, 0));
        yield return new WaitForSeconds(0.01f);
        timeSmooth += Time.deltaTime;
        if (timeSmooth < 2 && playerHandModel.localScale.y > 1)
        {
            StartCoroutine(SmoothOut());
            
        }
        else
        {
            playerHandModel.localScale = new Vector3(1, 1, 1);
            playerHand.eulerAngles = new Vector3(0, 0, 70);
            hits = false;
            isTouching = false;
            isComplete = true;
            if (change)
            {
                playerHand.eulerAngles = new Vector3(0, 0, 0);
                //Vector2 direction;
                //direction = new Vector2(
                //objInHand.transform.position.x - playerHandModel.transform.position.x,
                //objInHand.transform.position.y - playerHandModel.transform.position.y);
                isComplete = false;
                change = false;
                returnComplete = false;
                originTransform = objInHand.transform.position;
                originGameobject = objInHand;
                Smooth();
            }
            change = false;
        }
    }

    public void IncreaseObjectSize(float amount, Vector3 direction)
    {
        playerHandModel.localScale += new Vector3(0, 0.05f * amount, 0); 
    }
    public void DecreaseObjectSize(float amount, Vector3 direction)
    {
        playerHandModel.localScale -= new Vector3(0, 0.05f * amount, 0);
    }

    void Facing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction;
        if (objInHand && !change)
        {
            direction = new Vector2(
            objInHand.transform.position.x - playerHandModel.transform.position.x,
            objInHand.transform.position.y - playerHandModel.transform.position.y);
        }
        else if (!change)
        {
            direction = new Vector2(
            mousePosition.x - playerHandModel.transform.position.x,
            mousePosition.y - playerHandModel.transform.position.y
        );
        }
        else
        {
            direction = new Vector2(
            originTransform.x - playerHandModel.transform.position.x,
            originTransform.y - playerHandModel.transform.position.y);
        }
     


        if (!isComplete || change)
        {
            playerHand.up = direction;
        }
        
    }

    void BottleServing(bool up)
    {
        if (objInHand)
        {
            if (up)
            {
                objInHand.transform.eulerAngles = new Vector3(0, 0, 160);
            }
            else
            {
                objInHand.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    void Returning()
    {
        if (originGameobject.transform.position.x <= originTransform.x && change)
        {
            hits = false;
            originGameobject.transform.position = originTransform;
            returnComplete = true;
        }
    }

}
