using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatFish : Fish
{
    bool? isRight = null;
    Vector2 speed = Vector2.right;

    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
    public override void Swim()
    {
        if(isRight == null) //無理矢理向きを決定しています
        {
            if(transform.position.x>0) isRight = false;
            else isRight = true;
        }
        float dt = Time.deltaTime;
        if(isRight == false) dt *= -1f;
        transform.Translate(speed*dt);
    }
}
