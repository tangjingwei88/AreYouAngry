using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMe : MonoBehaviour {
    public Text countLable;

    private int stageId = 1;
    private float addNum = 0;
    private float minusNum = 0;
    private float dampTime = 0;     //自动衰减时间
    private string countColor = "[00FF16FF]";
    
    public float clickCount;
    public float count = 0.00f;
    public float timer;


     void Awake()
    {
        //初始化1开始
        GetConfigInfo(stageId);
    }

    public void GetConfigInfo(int id)
    {
        TimeConfigManager.TimeConfig cf = TimeConfigManager.GetTimeConfig(id);
        clickCount = cf.clickCount;
        addNum = cf.addNum;
        minusNum = cf.minusNum;
        dampTime = cf.dampTime;
        countColor = cf.countColor;
    }

    public void OnClick()
    {
        count += addNum;
        countLable.text = countColor + count.ToString();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > dampTime && count > 0)
        {
            count -= minusNum;
            countLable.text = count.ToString();
        }
        if (count > clickCount)
        {
            stageId += 1;
            GetConfigInfo(stageId);
        }

        if (count < 0)
        {
            count = 0.00f;
        }
    }
}
