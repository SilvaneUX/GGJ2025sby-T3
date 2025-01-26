using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public GameObject[] bubbleParticle;
    public GameObject pupParticle;
    public Transform swimmer;
    public bool isModel;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartEnding()
    {
        animator.SetBool("IsModel", isModel);

        GameObject particle;
        GameObject particle2;
        if (StaticScore.Instance.score > 36)
        {
            particle = Instantiate(bubbleParticle[0], swimmer.transform.position, Quaternion.identity, swimmer);
            particle.transform.eulerAngles = new Vector3(0, 90, 0);
            particle.transform.position = new Vector3(particle.transform.position.x, particle.transform.position.y, -2);
        }
        else if (StaticScore.Instance.score > 18)
        {
            particle = Instantiate(bubbleParticle[1], swimmer.transform.position, Quaternion.identity, swimmer);
            particle.transform.eulerAngles = new Vector3(0, 90, 0);
            particle.transform.position = new Vector3(particle.transform.position.x, particle.transform.position.y, -2);
        }
        else
        {
            particle2 = Instantiate(pupParticle, swimmer.transform.position, Quaternion.identity, swimmer);
            particle2.transform.eulerAngles = new Vector3(0, 90, 0);
            particle2.transform.position = new Vector3(particle2.transform.position.x, particle2.transform.position.y, -2);
        }

        

        
        
    }
}
