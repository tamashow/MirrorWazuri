using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [SerializeField] GameObject logTemplate; //ログのテンプレートオブジェクトをここに
    List<Log> logs;
    int moveQueue = 0;
   // [SerializeField] float heightDisplayedArea = 5f;
    public float pitch = 0.1f; //ログとログの間隔
    public float scrollTime= 0.7f; //次の停留点まで移動する時間

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


    void addLogFish(Fish fish)
    {
        Vector3 spawnOn = new Vector3(0f,pitch * logs.Count,0f);
        GameObject newLogObject = Instantiate(original: logTemplate, position: spawnOn, rotation: Quaternion.identity, parent: this.transform);
        if(newLogObject == null)
        {
            throw new System.Exception("failed in instantinating log object");
        }

        Log newLog = newLogObject.GetComponent<Log>();
        if (newLog == null)
        {
            throw new System.Exception("it seems logTemplate does not have {Log} script");
        }

        logs.Add(newLog);

        moveQueue += 1;
    }

    void updateLogsState()
    {
        bool existMoving = false;
        foreach(Log log in logs) {
            if (log.isMoving)
            {
                existMoving = true;
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
                float duration = scrollTime;
                //ログコントローラーを親としたローカル座標で話が進むことに注意

                Vector3 basePoint = new Vector3(0f, 0f, 0f); //logs[0]はここにいることを期待される

                //一番下のログをさらに下に流しつつフェードアウトさせる
                Log logToDisappeare = logs[0];
                logToDisappeare.moveToNextPoint( basePoint + new Vector3(0f,-pitch,0f) , duration);
                logToDisappeare.StartDisappeare();

                logs.RemoveAt(0); //インデックスが 2から１へ　1から0へ

                //全てのlogを1段階下に動かす
                for (int i = 0; i < logs.Count; i++)
                {
                    Vector3 nextPoint = new Vector3(0f, pitch*i ,0f);
                    Log log = logs[i];
                    log.moveToNextPoint(nextPoint, duration);
                }

                moveQueue--;
            }
        }

    }

}
