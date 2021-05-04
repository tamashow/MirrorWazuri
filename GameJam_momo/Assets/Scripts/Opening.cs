using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    [SerializeField]GameObject wa;
    [SerializeField]float moveTime; //移動するまでにかかる時間
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move()
    {
        Vector3 translation = ((transform.position - wa.transform.position)/moveTime);
        Vector3 rotation = new Vector3(0,0,180/moveTime);
        float scaleChange = (wa.transform.localScale.x-transform.localScale.x)/moveTime;
        float timer=0f;
        float dt;
        while(timer < moveTime)
        {
            dt = Time.deltaTime;
            transform.Translate(translation*dt);
            transform.Rotate(rotation*dt);
            transform.localScale += new Vector3(1,1,0)*scaleChange*dt;
            timer += dt;
            yield return null;
        }
        gameObject.SetActive(false);
        wa.transform.parent.gameObject.SetActive(true);
    }
}
