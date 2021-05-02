using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickUpper : MonoBehaviour //ランダムに選ぶ、決まった魚を選ぶ、といった機能を派生クラスで実装してください
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual FishData? PickUpFish()
    {
        return new FishData();
    }
}
