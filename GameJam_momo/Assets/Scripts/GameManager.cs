using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    List<Fish> fishesInTheField; //instantinated fishes  ゲームの場にある（インスタンス化された）魚たち
    FishPickUpper pickUpper;
    FishDataContainer fishDataContainer;

    //テスト用変数
    float period = 2f;
    float timer = 0f;
    //テスト用変数

    // Start is called before the first frame update
    void Start()
    {
        FishLoader fishLoader = new FishLoader();
        fishLoader.LoadFishData();
        fishDataContainer = fishLoader.Export();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(period< timer)
        {
            timer = 0f;
            InstantinateFish(fishDataContainer.RandomPick());
        }
    }

    public void InstantinateFish(FishData data)
    {
        Vector3 position = Vector3.zero;
        var fishObj = new GameObject("fish");
        SpriteRenderer spriteRenderer = fishObj.AddComponent<SpriteRenderer>();
        Sprite sprite = Sprite.Create(data.bodyImage, new Rect(0.0f, 0.0f, data.bodyImage.width, data.bodyImage.height), Vector2.zero);
        sprite.name = "dynamicSprite!";
        spriteRenderer.sprite = sprite;
        BoxCollider2D colider = fishObj.AddComponent<BoxCollider2D>();
        SimpleFish fish = fishObj.AddComponent<SimpleFish>();
        fish.manager = this;
        fish.fishData = data;


        fishesInTheField.Add(fish as Fish);
    }

    public void FishCaught(Fish fish) //スコアを計算したりログに流したりしましょう
    {

    }
    
}

