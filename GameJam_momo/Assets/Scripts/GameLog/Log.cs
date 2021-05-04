using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]

public class Log : MonoBehaviour //サムネイル用のspriteと名前用のtextとスコア用のtextを子に持つゲームオブジェクトにアタッチする
{ 
    [SerializeField] SpriteRenderer thumbnailSpriteRenderer;
    [SerializeField] Text fishNameText;
    [SerializeField] Text scoreText;

    LogController logController;

    bool isMovingFlag = false;
    public bool isMoving
    {
        get { return isMovingFlag; }
    }

    float alpha = 1.0f;
    float scale = 1.0f;
    
    public FishData fishData = new FishData(); //gameManagerが入れてくれる

    float score
    {
        get { return fishData.score; }
    }
    string name
    {
        get { return fishData.name; }
    }

    Vector2 position
    {
        get { return this.transform.position; }
        set { this.transform.position = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        float width = thumbnailSpriteRenderer.bounds.size.x;
        float height = thumbnailSpriteRenderer.bounds.size.y;

        if(fishData.bodyImage == null)
        {
            throw new System.Exception("fish data not set");
        }

        Sprite sprite = Sprite.Create(fishData.bodyImage, new Rect(0.0f, 0.0f, fishData.bodyImage.width, fishData.bodyImage.height), Vector2.zero);
        sprite.name = "dynamicSprite!";
        thumbnailSpriteRenderer.sprite = sprite;
        thumbnailSpriteRenderer.gameObject.transform.Translate(new Vector3(-width/2, -height/2,0.0f));

        float currentWidth = sprite.bounds.size.x;
        float currentHeight = sprite.bounds.size.y;
        float idealWidth = width;
        float idealHeight = height;
        float scaleX = idealWidth / currentWidth;
        float scaleY = idealHeight / currentHeight;
       // thumbnailSpriteRenderer.gameObject.transform.localScale = new Vector3(scaleX, scaleY);


        fishNameText.text = fishData.name;
        scoreText.text = ((int)score).ToString("000") + "pt";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDisappeare()
    {
        CanvasGroup group = this.GetComponent<CanvasGroup>();
        group.DOFade(endValue: 0,duration: 0.5f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        }
        );
    }
    public void moveToNextPoint(Vector3 point,float duration)
    {
        isMovingFlag = true;
        this.gameObject.transform.DOMove( point , duration).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            isMovingFlag = false;
        });
    }
}