using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataContainer 
{
    private List<FishData> fishData;

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
        return fishData[0];
    }

    public List<FishData> GetFishData()
    {
        return fishData;
    }
}

