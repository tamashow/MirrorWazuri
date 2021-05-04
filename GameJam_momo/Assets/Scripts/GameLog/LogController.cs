using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [SerializeField] GameObject logTemplate; //ログのテンプレートオブジェクトをここに
    [SerializeField] GameObject popUpTemplate; //ポップアップ（説明文つき）のテンプレートオブジェクトをここに

    [SerializeField] Vector3 popUpInitPosition = new Vector3(0f,-2f,0f);
    [SerializeField] Vector3 popUpStopPosition = new Vector3(0f, 0f, 0f);
    Queue<Log> logs = new Queue<Log>();
    List<FishData> hisory = new List<FishData>();
    int moveQueue = 0;
   // [SerializeField] float heightDisplayedArea = 5f;
    private const float pitch = 15.0f; //ログとログの間隔
    public float scrollTime= 0.7f; //次の停留点まで移動する時間
    float waitDuration = 3.0f; //ログが同じところに止まる時間
    float waitTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (logTemplate == null)
        {
            throw new System.Exception("plz attach log object's template");
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateLogsState();
    }


    public void addFishToLog(Fish fish)
    {
        addFishToLog(fish.fishData);
    }

    public void addFishToLog(FishData data)
    {
        hisory.Add(data);
         Vector3 spawnOn = this.transform.position + new Vector3(0f, pitch * logs.Count, 0f);

        SpawnPopUp(data);
       
        GameObject newLogObject = Instantiate(original: logTemplate, position: spawnOn, rotation: Quaternion.identity, parent: this.transform);
        if (newLogObject == null)
        {
            throw new System.Exception("failed in instantinating log object");
        }

        Log newLog = newLogObject.GetComponent<Log>();
        newLog.fishData = data;
        if (newLog == null)
        {
            throw new System.Exception("it seems logTemplate does not have {Log} script");
        }

        logs.Enqueue(newLog);

        moveQueue += 1;
    }

    void SpawnPopUp(FishData data)
    {
        GameObject popUpObject = Instantiate(original: popUpTemplate, position: popUpInitPosition, rotation: Quaternion.identity);

        if (popUpObject == null)
        {
            throw new System.Exception("failed in instantinating popUp object");
        }

        PopUp newPopUp = popUpObject.GetComponent<PopUp>();
        newPopUp.fishData = data;
        if (newPopUp == null)
        {
            throw new System.Exception("it seems logTemplate does not have {PopUp} script");
        }
        newPopUp.disappearPoint = new Vector3(0f,10f,0f);
        newPopUp.stopPoint = popUpStopPosition;
    }


    void updateLogsState()
    {
        bool existMoving = false;
        if (logs.Count != 0)
        {
            foreach (Log log in logs.ToArray())
            {
                if (log.isMoving)
                {
                    existMoving = true;
                }
            }
        }


        if(moveQueue > 0)
        {
            if (existMoving)
            {
                //nop
            }
            else
            {
                if(waitTimer > waitDuration)
                {
                    waitTimer = 0f;

                    Debug.Log("move");
                    float duration = scrollTime;

                    Vector3 basePoint = this.transform.position + new Vector3(0f, 0f, 0f); //logs[0]はここにいることを期待される

                    //一番下のログをさらに下に流しつつフェードアウトさせる
                    Log logToDisappeare = logs.Peek();
                    Debug.Log("will disappear");
                    Debug.Log(logToDisappeare.name);
                    logToDisappeare.moveToNextPoint(basePoint + new Vector3(0f, -pitch, 0f), duration);
                    logToDisappeare.StartDisappeare();

                    logs.Dequeue(); //一番古いログを消す

                    Log[] logArray = logs.ToArray();
                    //全てのlogを1段階下に動かす
                    for (int i = 0; i < logArray.Length; i++)
                    {
                        Vector3 nextPoint = basePoint + new Vector3(0f, pitch * i, 0f);
                        Log log = logArray[i];
                        log.moveToNextPoint(nextPoint, duration);
                    }

                    moveQueue--;
                }
                else
                {
                    waitTimer += Time.deltaTime;
                }
            }
        }

    }

    public string  ExportHistory()
    {
        string outStr = "";

        foreach(FishData data in hisory)
        {
            string name = data.name;
            string score = ((int)data.score).ToString("000") + "pt";
            outStr += name + ": " + score + "pt" + "\n";
        }
        return outStr;
    }

    private void Reset()
    {
        hisory = new List<FishData>();
    }
}
