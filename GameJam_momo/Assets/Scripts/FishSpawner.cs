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
                                          new Vector3(0f,0f,0f),
                                          new Vector3(0f,0f,0f)
                                             };

    List<Type> easyFishType = new List<Type>() {typeof(FloatFish)}  ;
    List<Type> normalFishType = new List<Type>() { typeof(SimpleFish) };
    List<Type> hardFishType = new List<Type>() { typeof(SimpleFish) };


    bool spawning = false;

    float minimumSpawningInterval = 3.0f;
    float intervalTimer = 0f;

    [SerializeField] int idealEasyCount = 3;
    [SerializeField] int idealNormalCount = 3;
    [SerializeField] int idealHardCount = 0;

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
        UpdateFishCount();

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


    void UpdateFishCount()
    {
        int easyCount = 0;
        int normalCount = 0;
        int hardCount = 0;
        foreach(Fish fish in manager.fishesInTheField)
        {
            switch (fish.difficulity)
            {
                case Difficulity.easy:
                    easyCount++;
                    break;
                case Difficulity.normal:
                    normalCount++;
                    break;
                case Difficulity.hard:
                    hardCount++;
                    break;
            }
        }
        this.currentEasyCount = easyCount;
        this.currentNormalCount = normalCount;
        this.currentHardCount = hardCount;
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

        manager.InstantinateFish(fishType, data, position,Difficulity.easy);

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

public enum Difficulity
{
    easy,
    normal,
    hard
}