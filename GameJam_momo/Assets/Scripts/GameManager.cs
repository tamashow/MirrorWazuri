using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] FishSpawner spawner;
    public List<Fish> fishesInTheField = new List<Fish>(); //instantinated fishes  ゲームの場にある（インスタンス化された）魚たち
    FishPickUpper pickUpper;
    public FishDataContainer fishDataContainer;
    [SerializeField] public LogController logController;
    [SerializeField] ResultViewController resultView;
    [SerializeField]Text TimeText;
    [SerializeField] int limitTime;
    [SerializeField]Text scoreText;
    [SerializeField]Opening opening;
    public bool inGame;
    float timer; //ゲーム開始時にlimitTimeに設定する
    public int score;
    Coroutine coroutine = null;
    AudioSource audio;
    [SerializeField]AudioClip fanfare;
    [SerializeField]AudioClip penalty;

    // Start is called before the first frame update
    void Start()
    {
        FishLoader fishLoader = new FishLoader();
        fishLoader.LoadFishData();
        fishDataContainer = fishLoader.Export();
        spawner.StartSpawning();
        audio = GetComponent<AudioSource>();
        resultView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGame && Input.anyKeyDown && !resultView.gameObject.activeInHierarchy && coroutine == null)//なんかの入力を受けたらゲームスタート
        {
            coroutine = StartCoroutine(opening.Move());
            //inGame = true;
            timer = limitTime;
            
        }
        if(inGame)
        {
            timer -= Time.deltaTime;
            TimeText.text = ((int)(timer +0.99f)).ToString();
            if(timer <= 0)
            {
                inGame = false;
                Finish();
            }
            if(scoreText.text == "")
            {
                scoreText.text = score.ToString();
            }
        }
    }

    // public void InstantinateFish(Type type,FishData data,Vector3 position)
    // {
    //     var fishObj = new GameObject("fish");
    //     SpriteRenderer spriteRenderer = fishObj.AddComponent<SpriteRenderer>();
    //     Sprite sprite = Sprite.Create(data.bodyImage, new Rect(0.0f, 0.0f, data.bodyImage.width, data.bodyImage.height), Vector2.zero);
    //     sprite.name = "dynamicSprite!";
    //     spriteRenderer.sprite = sprite;
    //     BoxCollider2D colider = fishObj.AddComponent<BoxCollider2D>();
    //     Fish fish = fishObj.AddComponent(type) as Fish;

    //     if (fish == null)
    //     {
    //         throw new Exception("given type is not child of fish");
    //     }
    //         fish.manager = this;
    //     fish.fishData = data;

    //     fishesInTheField.Add(fish as Fish);
    // }

    public void InstantinateFish(Type type, FishData data, Vector3 position, Difficulity difficulity)
    {
        var fishObj = new GameObject("fish");
        SpriteRenderer spriteRenderer = fishObj.AddComponent<SpriteRenderer>();
        Sprite sprite = Sprite.Create(data.bodyImage, new Rect(0.0f, 0.0f, data.bodyImage.width, data.bodyImage.height), Vector2.zero);
        sprite.name = "dynamicSprite!";
        spriteRenderer.sprite = sprite;
        BoxCollider2D collider = fishObj.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(5,5);
        Fish fish = fishObj.AddComponent(type) as Fish;

        if (fish == null)
        {
            throw new Exception("given type is not child of fish");
        }
        fish.manager = this;
        fish.fishData = data;
        fish.difficulity = difficulity;

        fishesInTheField.Add(fish as Fish);
    }

    public void InstantinateFish<FishType>(FishData data, Vector3 position)
    where FishType : Fish
    {
        var fishObj = new GameObject("fish");
        SpriteRenderer spriteRenderer = fishObj.AddComponent<SpriteRenderer>();
        Sprite sprite = Sprite.Create(data.bodyImage, new Rect(0.0f, 0.0f, data.bodyImage.width, data.bodyImage.height), Vector2.zero);
        sprite.name = "dynamicSprite!";
        spriteRenderer.sprite = sprite;
        BoxCollider2D colider = fishObj.AddComponent<BoxCollider2D>();
        FishType fish = fishObj.AddComponent<FishType>();
        fish.manager = this;
        fish.fishData = data;

        fishesInTheField.Add(fish as Fish);
    }


    public void InstantinateFish(FishData data) //テスト用にsimplefishを作るようクラス(非推奨)
    {
        Vector3 position = new Vector3(0, 0, 0);
        InstantinateFish<SimpleFish>(data: data, position: position);
    }

    public void FishCaught(Fish fish) //スコアを計算したりログに流したりしましょう
    {
        if(inGame)
        {
            fishesInTheField.Remove(fish);
            score += (int)fish.fishData.score;
            //ログの計算
            logController.addFishToLog(fish);
            scoreText.text = score.ToString();
            if(fish.fishData.score > 0) audio.PlayOneShot(fanfare);
            else audio.PlayOneShot(penalty);
        }
    }
    
    public void Reset() //以下の処理は全て仮のものです
    {
        score = 0;
        foreach(var fish in fishesInTheField)
        {
            Destroy(fish.gameObject);
        }
        fishesInTheField.Clear();
        SceneManager.LoadScene("MainScene");
    }

    void Finish()
    { 
        resultView.gameObject.SetActive(true);
        resultView.ShowResult();
    }
}

