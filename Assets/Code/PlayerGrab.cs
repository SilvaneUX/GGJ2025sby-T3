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
    public Transform checkDistance;

    public GameObject objInHand;

    public float timeSmooth;
    public Vector3 originTransform;
    public GameObject originGameobject;

    public bool isTouching;
    public bool change;
    bool hits;
    bool isComplete;
    public bool allCompleted = true;
    bool returnComplete;
    public bool pour;
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
        if (Input.GetMouseButtonDown(0) && allCompleted)
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

                allCompleted = false;
                Smooth();
            } 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BottleServing(true);
            objInHand.GetComponent<Item>().waterOn = true;
            pour = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            BottleServing(false);
            objInHand.GetComponent<Item>().waterOn = false;
            pour = false;
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
        yield return new WaitForSeconds(0);
        BottleServing(true);
        StartCoroutine(Action());
        // do stuff
    }

    void Smooth()
    {
        Vector2 direction;
        direction = new Vector2(
            objInHand.transform.position.x - playerHandModel.transform.position.x,
            objInHand.transform.position.y - playerHandModel.transform.position.y);
        playerHand.up = direction;
        timeSmooth = 0;
        StartCoroutine(SmoothDamp());

    }

    public IEnumerator SmoothDamp()
    {
        IncreaseObjectSize(1, new Vector3(0.01f, 0, 0));
        yield return new WaitForSeconds(0.01f);
        timeSmooth += Time.deltaTime;
        if (timeSmooth < 2 && !isTouching && !returnComplete)
        {
            StartCoroutine(SmoothDamp());
        }
        else if (change && (objectGrab.position.x < objInHand.transform.position.x))
        {
            ItsClose();
     
        }
        else if (change)
        {
            TooFarAway();
        }
        else 
        {
            timeSmooth = 0;
            hits = true;
            StartCoroutine(SmoothOut());
            
        }
    }

    public void TooFarAway()
    {
        timeSmooth = 0;
        hits = false;
        isTouching = false;
        isComplete = true;
        if (change)
        {
            isComplete = false;
            change = false;
            returnComplete = false;
            originTransform = objInHand.transform.position;
            originGameobject = objInHand;
            DecreaseObjectSize(5, new Vector3(0.01f, 0, 0));
            

            Smooth();
        }
        change = false;
    }

    public void ItsClose()
    {
        
        timeSmooth = 0;

        isTouching = false;
        isComplete = true;
        if (change)
        {
            change = false;


            isComplete = false;

            returnComplete = false;
            originTransform = objInHand.transform.position;
            originGameobject = objInHand;
            objInHand.GetComponent<Collider2D>().enabled = true;
            IncreaseObjectSize(5, new Vector3(0.01f, 0, 0));

            StartCoroutine(SmoothOut());

        }
        change = false;
    }


    IEnumerator SmoothOut()
    {
        DecreaseObjectSize(1, new Vector3(0.01f, 0, 0));
        yield return new WaitForSeconds(0.01f);
        timeSmooth += Time.deltaTime;
        if (timeSmooth < 2 && playerHandModel.localScale.y > 1)
        {
            if (isTouching && !returnComplete)
            {
                hits = true;
            }
            StartCoroutine(SmoothOut());
            
        }
        else
        {
            playerHandModel.localScale = new Vector3(1, 1, 1);
            playerHand.eulerAngles = new Vector3(0, 0, 80);
            objInHand.transform.position = objectGrab.position;
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
            //change = false;
            returnComplete = false;
            allCompleted = true;
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
        if (!objInHand && !change)
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
                objInHand.transform.eulerAngles = new Vector3(0, 0, 116);
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
