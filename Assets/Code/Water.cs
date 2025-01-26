using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    float smoothTime;
    Vector3 originScale;
    Coroutine coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGrab.Instance.pour && coroutine == null)
        {
            smoothTime = 0;
            coroutine = StartCoroutine(Smooth());

 
        }

        if (!PlayerGrab.Instance.pour)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            
            coroutine = null;
            transform.localScale = originScale;
        }
    }

    IEnumerator Smooth()
    {
        yield return new WaitForSeconds(0.001f);
        smoothTime += Time.deltaTime;
        if (smoothTime < 30)
        {
            IncreaseObjectSize(0.8f, Vector3.down);
            StartCoroutine(Smooth());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseObjectSize(float amount, Vector3 direction)
    {
        transform.localScale += new Vector3(0, 1f * amount, 0);
    }
    public void DecreaseObjectSize(float amount, Vector3 direction)
    {
        transform.localScale -= new Vector3(0, -0.1f * amount, 0);
    }
}
