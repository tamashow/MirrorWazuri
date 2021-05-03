using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Log : MonoBehaviour //サムネイル用のspriteと名前用のtextとスコア用のtextを子に持つゲームオブジェクトにアタッチする
{
    [SerializeField] SpriteRenderer thumbnailSpriteRenderer;
    [SerializeField] TextMesh fishNameText;
    [SerializeField] TextMesh scoreText;

    LogController logController;

    bool isMovingFlag = false;
    public bool isMoving
    {
        get { return isMovingFlag; }
    }

    float alpha = 1.0f;
    float scale = 1.0f;
    
    FishData fishData; //gameManagerが入れてくれる

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

        Sprite sprite = Sprite.Create(fishData.thumbnail, new Rect(0, 0, width, height), Vector2.zero);
        thumbnailSpriteRenderer.sprite = sprite;

        fishNameText.text = fishData.name;
        scoreText.text = ((int)score).ToString("000");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDisappeare()
    {
        thumbnailSpriteRenderer.DOFade(endValue: 0,duration: 0.5f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        }
        );
    }
    public void moveToNextPoint(Vector3 point,float duration)
    {
        isMovingFlag = true;
        thumbnailSpriteRenderer.gameObject.transform.DOMove( point , duration).SetEase(Ease.InOutQuart);
    }
}