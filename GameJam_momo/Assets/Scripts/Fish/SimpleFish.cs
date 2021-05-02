using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFish :Fish
{
    Vector3 initPosition = new Vector3(0f,0f,0f);
    Vector3 speed = new Vector3(1.00f,0f,0f);
    float amplitude = 1f; //sinpuku (sindou no)
    public override void Swim()
    {
        float dt = Time.deltaTime;
        Vector2 currentPosition = this.transform.position;
        if (  amplitude < Vector3.Distance(initPosition, currentPosition))
        {
            speed = -speed;
            this.transform.Translate(speed*dt);
        }
        this.transform.Translate(dt * speed);
    }
}
