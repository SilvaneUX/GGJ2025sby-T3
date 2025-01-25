using UnityEngine;

public class StaticScore : MonoBehaviour
{
    // SINGLETON
    private static StaticScore _score;
    public static StaticScore Instance
    {
        get{
            if(_score == null)
            {
                return null;
            }
            return _score;
        }
    }

    public int score;

    void Awake()
    {
        _score = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
