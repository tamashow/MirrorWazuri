using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    Rigidbody2D rigidbody;
    AudioSource audio;
    [SerializeField]AudioClip tsuriHajime;
    [SerializeField]AudioClip TsuriOwari;
    [Header("海面の高さ")][SerializeField]float seaLevel;
    bool underSea = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(underSea && transform.position.y > seaLevel && rigidbody.velocity.y > 0)
        {
            audio.PlayOneShot(TsuriOwari);
            underSea = false;
        }
        else if(!underSea && transform.position.y < seaLevel && rigidbody.velocity.y < 0)
        {
            audio.PlayOneShot(tsuriHajime);
            underSea = true;
        }
    }
}
