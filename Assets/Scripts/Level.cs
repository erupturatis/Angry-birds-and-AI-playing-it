using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Score sc;
    public Slingshot Sl;

    [HideInInspector]
    public bool isMoving = false;
    float timer=0f;

    public GameObject[] LevelTypes;
    [HideInInspector]
    public GameObject CurrentLevel;
    public Text txt;

    public int pigNumber;

    public ShootTHeFuckingBirdAgent S;

    void Start()
    {
        Sl.L = GetComponent<Level>();
        //spawn level
        SpawnNext();
    }

    public void GetDecision()
    {
        S.RequestDecision();
    }

    public void AddScore(float a)
    {
        if (Sl.launched != 0)
        {
            sc.AddScore((int)a);
        }
    }
    public void resetTimer()
    {

        isMoving = true;
        timer = 1;
    }
    public void SpawnNext()
    {
        
        Sl.launched = 0;
        Sl.launched2 = 0;
        sc.ResetScore();
        if (CurrentLevel)
        {
            Destroy(CurrentLevel);
        }
        pigNumber = 0;
        GameObject Lvl = Instantiate(LevelTypes[Random.Range(0, LevelTypes.Length)], transform.position, Quaternion.identity, gameObject.transform);
        CurrentLevel = Lvl;
        
        resetTimer();
    }
    void Update()
    {
        if(timer <=0)
        {
            txt.text = "0";
            isMoving = false;
        }
        else{
            
            txt.text = "" + timer;
            timer -= Time.deltaTime;
        }

        if(pigNumber <= 0 && timer <=0 && Sl.launched != 0)
        {
            //print("won level");
            S.AddReward(20);
            S.AddReward((3 - Sl.launched) * 20);
            if (Sl.launched < 3)
            {
                //print("finished early");
            }
            S.EndEpisode();

            SpawnNext();
            
        }else
        if(pigNumber!=0 && timer<= 0 && Sl.launched >=3)
        {
            //print("lost level");
            S.AddReward(-20);
            S.AddReward(-20 * (pigNumber));
            S.EndEpisode();
            SpawnNext();
        }
        else
        {
            //print(pigNumber + "   " + Sl.launched + " " + timer);
        }
    }
}
