using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]float Force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.AddForce(new Vector3(Input.GetAxisRaw("Horizontal")*Force,0, 0));
        // if(Input.GetAxisRaw("Horizontal")!= 0)
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal")*Force,0,0);
        // Debug.Log(Input.GetAxisRaw("Vertical"));
        // transform.Translate(0,Input.GetAxisRaw("Vertical")*Force*Time.deltaTime,0);
    }
}
