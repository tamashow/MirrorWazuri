using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HardFish : Fish
{
    bool? isRight = null;
    Vector3 speed = Vector3.right;
    Rect swimArea = new Rect(x: -8f, y: -4f, width: 16f, height: 6f);
    float acceler = 9f;
    float g = 3f;
    float decisionSpeed = 5f;
    float maxSpeed = 4f;
    float decisionTime = 100f;
    Vector3 currentDistination = new Vector3(0f,0f,0f);
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
        decisionTime += Time.deltaTime;
        if(decisionTime > decisionSpeed)
        {
            decisionTime = 0f;
            float x = UnityEngine.Random.Range(swimArea.x, swimArea.x + swimArea.width);
            float y = UnityEngine.Random.Range(swimArea.y, swimArea.y + swimArea.height);
            currentDistination = new Vector3(x, y,0f);
        }
        float cof = 1f;
        if (transform.position.y > 2.5f)
        {
            cof = 0f;
        }
        float dt = Time.deltaTime;
        Vector3 direction = (currentDistination - transform.position).normalized;
        Vector3 a = acceler * direction;
        speed += cof * dt * a + dt * new Vector3(0f,-g,0f) ;
        if(Vector3.Distance(speed,Vector3.zero) > maxSpeed)
        {
            speed = maxSpeed * speed.normalized;
        }
        transform.Translate(dt*speed);
    }
}
