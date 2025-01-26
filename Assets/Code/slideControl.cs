using UnityEngine;
using UnityEngine.UI;

public class slideControl : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float speedSlider;
    [SerializeField] float[] bottlePourSlide = new float[5];
    [SerializeField] Button btnMatch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGrab.Instance.objInHand && PlayerGrab.Instance.pour)
        {
            if ((bottlePourSlide[0] + bottlePourSlide[1] + bottlePourSlide[2] + bottlePourSlide[3] + bottlePourSlide[4] < 100))
            {
                // Debug.Log("RUN");
                int id = PlayerGrab.Instance.objInHand.GetComponent<Item>().itemID;
                switch (id)
                {
                    case 0:
                        {
                            var calc = 1 * speedSlider * Time.deltaTime;
                            slider.value += calc;
                            bottlePourSlide[0] += calc;
                        }
                        break;
                    case 1:
                        {
                            var calc = 1 * speedSlider * Time.deltaTime;
                            slider.value += calc;
                            bottlePourSlide[1] += calc;
                        }
                        break;
                    case 2:
                        {
                            var calc = 1 * speedSlider * Time.deltaTime;
                            slider.value += calc;
                            bottlePourSlide[2] += calc;
                        }
                        break;
                    case 3:
                        {
                            var calc = 1 * speedSlider * Time.deltaTime;
                            slider.value += calc;
                            bottlePourSlide[3] += calc;
                        }
                        break;
                    case 4:
                        {
                            var calc = 1 * speedSlider * Time.deltaTime;
                            slider.value += calc;
                            bottlePourSlide[4] += calc;
                        }
                        break;
                }
            }
            else
            {
                btnMatch.interactable = true;
            }
        }
        
    }

    public void Matching()
    {

        // foreach(float bottlePour in bottlePourSlide)
        // {
        //     foreach(int BottlesObjective in ManagerObjective.Bottles)
        //     {
                
        //         if(Mathf.FloorToInt(bottlePour) == BottlesObjective || 
        //         BottlesObjective + 5 > bottlePour && 
        //         BottlesObjective - 5 < bottlePour)
        //         {
        //             //  perfect score - 18  
        //             StaticScore.Instance.score += 18;
        //         }
        //     }
        // }


        for(int i = 0; i < ManagerObjective.Bottles.Length; i++)
        {
            if(Mathf.FloorToInt(bottlePourSlide[i]) == ManagerObjective.Bottles[i]
            || ManagerObjective.Bottles[i] + 5 > bottlePourSlide[i]
            && ManagerObjective.Bottles[i] - 5 < bottlePourSlide[i]) 
            {
                StaticScore.Instance.score += 18;
            }else if(Mathf.FloorToInt(bottlePourSlide[i]) == ManagerObjective.Bottles[i]
            || ManagerObjective.Bottles[i] + 15 > bottlePourSlide[i]
            && ManagerObjective.Bottles[i] - 15 < bottlePourSlide[i])
            {
                StaticScore.Instance.score += 12;
            }
            else{
                StaticScore.Instance.score += 6;
            }
        }

        Debug.Log(StaticScore.Instance.score);
    }

    
}
