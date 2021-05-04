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
    void Regist()
    {

    }
    bool ExistsUnderSea()
    {
        return true;
    }
    void Finish() //turare masita
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
    public string explanation;
    public string name; // displayed name on the log
    public float score; // score
    public Texture2D thumbnail; //samuneiru
    public Texture2D bodyImage;
    //Texture2D hardArea;
    //Texture2D softArea;
}