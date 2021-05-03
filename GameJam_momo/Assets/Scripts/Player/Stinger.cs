using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{
    [SerializeField]Controller controller;
    [SerializeField]float pickSpeed; //水上に上げる速度
    [SerializeField]float catchHeight;　//ここまでワ　を上げる
    [SerializeField]float returnHeight; //釣り上げた後ここにワ　を戻す
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var fish = col.gameObject.GetComponent<Fish>();
        if(fish != null && controller.isControllable)//何かしらの判定
        {
            fish.isContactingNeedle = true;
            col.transform.parent = this.transform;
            controller.isControllable = false;
            float xSpeed = pickSpeed*controller.gameObject.transform.position.x*-0.5f/(catchHeight-controller.gameObject.transform.position.y);
            controller.rb.velocity = new Vector2(xSpeed,pickSpeed);
            StartCoroutine(CatchFish(fish));
        }
    }

    void Reset() //つり上げた状態から釣るモードに戻る　BackToWaterと分ける必要なし？
    {
        this.transform.DetachChildren();
        StartCoroutine(BackToWater());
    }

    IEnumerator CatchFish(Fish fish)
    {
        while(controller.gameObject.transform.position.y < catchHeight)
        {
            yield return null;
        }
        controller.rb.velocity = Vector2.zero;
        fish.Finish();
        yield return new WaitForSeconds(2f); //釣り上げた後の演出の時間待つ？
        Reset();
    }

    IEnumerator BackToWater()
    {
        controller.rb.velocity = new Vector2(0,pickSpeed*-1f);
        while(controller.gameObject.transform.position.y > returnHeight)
        {
            yield return null;
        }
        controller.rb.velocity = Vector2.zero;
        controller.isControllable = true;
    }
}
