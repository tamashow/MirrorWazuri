using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndButton : MonoBehaviour
{
    [SerializeField] ResultViewController resultView;
    [SerializeField] GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        resultView.GetComponent<CanvasGroup>().alpha = 0f;
        manager.Reset();
        resultView.gameObject.transform.position = new Vector3(100f,0f,0f);
    }
}
