using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishDataContainer 
{
    private List<FishData> fishData = new List<FishData>();

    public FishDataContainer(List<FishData> fishData)
    {
        this.fishData = fishData;
    }
    public FishData RandomPick()
    {
        if (fishData.Count == 0)
        {
            throw new System.Exception("no fishdata found");
        }
        System.Random r1 = new System.Random();
        int index = r1.Next(0, fishData.Count);
        return fishData[index];
    }

    public List<FishData> GetFishData()
    {
        return fishData;
    }
}

