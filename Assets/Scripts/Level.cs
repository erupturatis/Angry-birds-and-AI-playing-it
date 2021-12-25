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

    void Start()
    {
        Sl.L = GetComponent<Level>();
        //spawn level
        SpawnNext();
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
        if (CurrentLevel)
        {
            Destroy(CurrentLevel);
        }
        GameObject Lvl = Instantiate(LevelTypes[Random.Range(0, LevelTypes.Length)], transform.position, Quaternion.identity, gameObject.transform);
        CurrentLevel = Lvl;
       
        resetTimer();
    }
    void Update()
    {
        //print(isMoving);
        if(timer <=0)
        {
            txt.text = "0";
            isMoving = false;

            //print(pigNumber);
            //print(Sl.launched);
        }
        else{
            
            txt.text = "" + timer;
            timer -= Time.deltaTime;
        }

        if(pigNumber == 0 && timer <=0 && Sl.launched != 0)
        {
            print("won level");
            
            SpawnNext();
        }
        if(pigNumber!=0 && timer<= 0 && Sl.launched >=3)
        {
            print("lost level");

            SpawnNext();
        }
    }
}
