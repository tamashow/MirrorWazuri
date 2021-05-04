using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogOutPutTester : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] LogController logController;
    int currentIndex = 0;
    float timer = 0f;
    List<FishData> fishDatas;
    List<(float time, FishData fish)> testQueue = new List<(float time, FishData fish)>();
    bool managerIsInitialized = false;

    void Start()
    {
        FishLoader fishLoader = new FishLoader();
        fishLoader.LoadFishData();
        FishDataContainer fishDataContainer = fishLoader.Export();
        fishDatas = fishDataContainer.GetFishData();
        for (int i = 0; i < fishDatas.Count; i++)
        {
            Debug.Log(fishDatas[i].name);
            (float time, FishData data) newQueue = ((i * 2.0f), fishDatas[i]);
            testQueue.Add(newQueue);
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        if(testQueue[currentIndex].time < timer)
        {
            logController.addFishToLog(testQueue[currentIndex].fish);
            currentIndex += 1;
            if(currentIndex >= testQueue.Count)
            {
                currentIndex = 0;
                timer = 0f;
            }
        }
    }
}