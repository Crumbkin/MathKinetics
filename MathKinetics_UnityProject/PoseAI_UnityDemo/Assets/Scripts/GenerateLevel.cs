using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoseAI;

public class GenerateLevel : MonoBehaviour
{
    //Script that generates randomly all the sections and the level itself.
    //It also generates the obstacles

    public GameObject[] section;
    public int zPos = 32;
    public bool creatingSection = false;
    public int secNum;
    public GameObject distance_calc_Location;

    public int distanceInt;
    public float distance;

    public PoseAICharacterController poseai_controller_script;
    public GameObject Player;

    public int randombox_number;
    public int randombomb_number;
    public int randombomb_number02;

    public float timer = 0;

    public bool startTime = false;
    private float lastZ = 0;
    public bool counted = false;


    public GameObject QuestionMarkBox;
    public GameObject bombPrefab;
    public GameObject wall;
    public bool obstacle_spawned = false;

    public bool spawncube = false;

    public void Start()
    {
        Player = GameObject.Find("PlayerArmature Variant");
        poseai_controller_script = Player.GetComponent<PoseAICharacterController>();
    }


    void Update()
    {

        distance = poseai_controller_script.GetCurrentDistance();
        distanceInt = Mathf.FloorToInt(distance);
        if (distanceInt > lastZ + 1)
        {
            lastZ = distanceInt;
            counted = false;
            obstacle_spawned = false;
        }

        timer += Time.deltaTime;
        

        if (timer >= 10)
        {
            startTime = true;
        }

        if (startTime == true)
        {
            if (lastZ % 32 == 0 && counted == false)
            {
                createSection();
                counted = true;
            }
        }

        if (startTime == true)
        {
            if (lastZ % 10 == 0 && obstacle_spawned == false)
            {
                SpawnObject();
                obstacle_spawned = true;
            }
        }



    }

    void createSection()
    {
        secNum = Random.Range(0, 3);
        Instantiate(section[secNum], new Vector3(-2, 0, 96), Quaternion.identity);
    }


    //** Source: https://forum.unity.com/threads/making-random-range-generate-a-value-with-probability.336374/, Author: Baste
    struct RandomSelection
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomSelection(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    int GetRandomValue(params RandomSelection[] selections)
    {
        float rand = Random.value;
        float currentProb = 0;
        foreach (var selection in selections)
        {
            currentProb += selection.probability;
            if (rand <= currentProb)
                return selection.GetValue();
        }

        //will happen if the input's probabilities sums to less than 1
        //throw error here if that's appropriate
        return -1;
    }

    // **

    public void SpawnObject()
    {
        randombox_number = Random.Range(-4, 0);
        randombomb_number = Random.Range(-4, 0);
        randombomb_number02 = Random.Range(-4, 0);

        while (randombox_number == randombomb_number)
        {
            randombomb_number = Random.Range(-4, 0);
        }

        while(randombomb_number02 == randombox_number || randombomb_number02 == randombomb_number)
        {
            randombomb_number02 = Random.Range(-4, 0);
        }

        Vector3 randomSpawnPositionBox = new Vector3(randombox_number, 1, 50);
        Vector3 randomSpawnPositionWall = new Vector3(0, 0, 50);
        Vector3 randomSpawnPositionWall02 = new Vector3(0, 1.5f, 50);

        Vector3 randomSpawnPositionBomb = new Vector3(randombomb_number, 1, 50);
        Vector3 randomSpawnPositionBomb02 = new Vector3(randombomb_number02, 1, 50);
        

        //FOR TESTING PURPOSES:
        
        //Vector3 test = new Vector3(-2, 1, 50);
        //Instantiate(QuestionMarkBox, test, Quaternion.identity);
        //return;


        int random = GetRandomValue(
            new RandomSelection(0, 5, .5f),
            new RandomSelection(6, 9, .3f),
            new RandomSelection(9, 11, .2f)
            );

        if (distanceInt > 1000)
        {
            Instantiate(bombPrefab, randomSpawnPositionBomb, Quaternion.Euler(-90, -90, 0));
        }
        if (distanceInt > 1500)
        {
            Instantiate(bombPrefab, randomSpawnPositionBomb02, Quaternion.Euler(-90, -90, 0));
        }

        if (random <= 5)
        {
            Instantiate(QuestionMarkBox, randomSpawnPositionBox, Quaternion.identity);
        }
        if (random >= 6 && random <= 8)
        {
            if (spawncube == true) 
            {
                Instantiate(QuestionMarkBox, randomSpawnPositionBox, Quaternion.identity);
                spawncube = false;
            }
            else
            {
                Instantiate(wall, randomSpawnPositionWall, Quaternion.Euler(0, 90, 0));
                spawncube = true;
            }
            
        }
        if (random >= 9 && random <= 10)
        {
            if (spawncube == true)
            {
                Instantiate(QuestionMarkBox, randomSpawnPositionBox, Quaternion.identity);
                spawncube = false;
            }
            else
            {
                Instantiate(wall, randomSpawnPositionWall02, Quaternion.Euler(0, 90, 0));
                spawncube = true;
            }
            
        }
        if (random >= 10)
        {
            if (spawncube == true)
            {
                Instantiate(bombPrefab, randomSpawnPositionBox, Quaternion.Euler(-90, -90, 0));
                spawncube = false;
            }
            else
            {
                Instantiate(wall, randomSpawnPositionWall02, Quaternion.Euler(0, 90, 0));
                spawncube = true;
            }
            
        }
    }


        /* Old Spawner
       void Update()
       {

           if (creatingSection == false)
           {
               creatingSection = true;
               StartCoroutine(GenerateSection());
           }
       }



       IEnumerator GenerateSection()
       {
           secNum = Random.Range(0, 3);
           Instantiate(section[secNum], new Vector3(-2, 0,  96 + zPos), Quaternion.identity);
           zPos += 32;
           yield return new WaitForSeconds(5);
           creatingSection = false;


       }
       */
    }
