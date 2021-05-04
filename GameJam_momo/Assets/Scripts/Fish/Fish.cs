using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Fish : MonoBehaviour
{
    public GameManager manager;
    public FishData fishData;
    public bool isContactingNeedle = false;
    public float maxHeight=0;
    public float minHeight=-4;
    public float initialX=8;
    // Start is called before the first frame update
    public Fish()
    {

    }

    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float currentWidth = sprite.bounds.size.x;
        float currentHeight = sprite.bounds.size.y;
        float idealWidth = fishData.width;
        float idealHeight = fishData.height;
        float scaleX = idealWidth / currentWidth;
        float scaleY = idealHeight / currentHeight;
        InitPosition();
        if(transform.position.x < 0)
        {
            scaleX *= -1;
        }
        sprite.transform.localScale = new Vector3(scaleX,scaleY);
    }

    // Update is called once per frame
    void Update()
    {
        if (isContactingNeedle)
        {
            if(ExistsUnderSea())
            {
                Regist();
            }
            else
            {
                Finish();
            }
        }
        else
        {
            Swim();
        }
    
    }
    protected virtual void InitPosition()
    {
        int r = Random.Range(0,2);//0なら左、1なら右
        r = r*2-1; //0を-1に
        this.transform.position = new Vector2(initialX*r,Random.Range(minHeight,maxHeight));
    }
    void Regist()
    {

    }
    bool ExistsUnderSea()
    {
        return true;
    }
    public void Finish() //turare masita
    {

    }
    public virtual void Swim() //update position
    {

    }
}

public struct FishData
{
    public float width;
    public float height;
    public string name; // displayed name on the log
    public float score; // score
    public Texture2D thumbnail; //samuneiru
    public Texture2D bodyImage;
    //Texture2D hardArea;
    //Texture2D softArea;
}