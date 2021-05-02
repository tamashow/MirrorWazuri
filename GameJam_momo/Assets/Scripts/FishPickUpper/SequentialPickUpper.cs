using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPickUpper : FishPickUpper//テキストファイルでどの魚をどういう順番で読み込むか決めるクラス
{
    List<FishData> fishData;
    int currentIndex;

    public override FishData? PickUpFish()
    {
        if ( !(currentIndex < fishData.Count))
        {
            return fishData[currentIndex];
        }
        else
        {
            return null;
        }

    }

    void ReadFromFile(string fileData)
    {

    }
}
