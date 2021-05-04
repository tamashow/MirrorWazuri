using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] GameManager manager;
     FishDataContainer fishContainer
    {
        get { return manager.fishDataContainer; }
    }

    List<Vector3> spawnPositionList = new List<Vector3>() {
                                          new Vector3(50f,0f,0f),
                                          new Vector3(-50f,0f,0f)
                                             };

    List<Type> easyFishType = new List<Type>() {typeof(SimpleFish)}  ;
    List<Type> normalFishType = new List<Type>() { typeof(SimpleFish) };
    List<Type> hardFishType = new List<Type>() { typeof(SimpleFish) };

    bool spawning = false;

    float minimumSpawningInterval = 3.0f;
    float intervalTimer = 0f;

    int idealEasyCount = 3;
    int idealNormalCount = 0;
    int idealHardCount = 0;

    int currentEasyCount = 0;
    int currentNormalCount = 0;
    int currentHardCount = 0;

    int idealFishCount 
    {
        get{return idealEasyCount + idealNormalCount + idealHardCount; }
    }
    float currentFishCount
    {
        get { return manager.fishesInTheField.Count; }
    }


    float timer = 0f;

    void Start()
    {
 
    }

    void Update()
    {
        if(spawning == true)
        {
            intervalTimer += Time.deltaTime;
            if(minimumSpawningInterval < intervalTimer)
            {
                intervalTimer = 0f;
                if (idealFishCount > currentFishCount)
                {
                    SpawnAFish();
                }
            }

        }
    }

    public void StartSpawning()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    void SpawnAFish() //spawn one fish
    {
        if(idealEasyCount > currentEasyCount)
        {
            SpawnEasyFish();
        }
        else if(idealNormalCount > currentNormalCount)
        {
            SpawnNormalFish();
        }
        else if(idealHardCount > currentHardCount)
        {
            SpawnHardFish();
        }
        
    }

    void SpawnEasyFish()
    {
        System.Random r1 = new System.Random();
        int typeIndex = r1.Next(0, easyFishType.Count);
        int posIndex = r1.Next(0, spawnPositionList.Count);

        Type fishType = easyFishType[typeIndex];
        FishData data = fishContainer.RandomPick();
        Vector3 position = spawnPositionList[posIndex];

        manager.InstantinateFish(fishType, data, position);

        currentEasyCount++;
    }

    void SpawnNormalFish()
    {

    }

    void SpawnHardFish()
    {

    }


    void InstantinateFish()
    {

    }


}
