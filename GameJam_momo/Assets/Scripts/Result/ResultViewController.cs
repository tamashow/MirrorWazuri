using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class ResultViewController : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] Text historyBox;
    [SerializeField] Text sum;
    [SerializeField] EndButton endButton;
    CanvasGroup group;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowResult()
    {
        this.transform.position = Vector3.zero;
        group.DOFade(1, duration: 2.0f);
        historyBox.text = manager.logController.ExportHistory();
        sum.text = ((int)manager.score).ToString("0000000") + "pt";
    }
}
