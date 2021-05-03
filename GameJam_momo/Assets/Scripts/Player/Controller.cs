using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("移動領域の指定")][SerializeField]Vector2 leftBottom,rightTop;
    Rigidbody2D rb;
    [SerializeField]float Hspeed,Vspeed;
    Vector2 axis;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axis = new Vector2(Input.GetAxisRaw("Horizontal")*Hspeed,Input.GetAxisRaw("Vertical")*Vspeed);
        if((transform.position.x <= leftBottom.x && axis.x < 0) ||
        (transform.position.x >= rightTop.x && axis.x > 0))
            axis.x = 0;
        if((transform.position.y <= leftBottom.y && axis.y < 0) ||
        (transform.position.y >= rightTop.y && axis.y > 0))
            axis.y = 0;
        rb.velocity = axis;
    }
}
