using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMe : MonoBehaviour {
    public Text countLable;
    public GameObject MoveImg;
    public Transform StartPos;
    public GameObject theGameOverPanel;
    public GameObject ClickBtn;


    private int stageId = 1;
    private float timeLong;
    private float addNum = 0;
    private float minusNum = 0;
    private float dampTime = 0;     //自动衰减时间
    private string countColor = "00FF01";
    private bool running = true;


    
    public float clickCount;
    public float nextClickCount;
    public float beforeClickCount;
    public float count = 0.00f;
    public float timer;
    public byte[] curRGBA;


    void Awake()
    {
        TimeConfigManager.Init();
        //初始化1开始
        GetConfigInfo(stageId);
        MoveImg.gameObject.GetComponent<Move>().Apply(timeLong);
 //       this.StartCoroutine(TimeSliping());
    }

    public void GetConfigInfo(int id)
    {
        //当前等级
        TimeConfigManager.TimeConfig CurCf = TimeConfigManager.GetTimeConfig(id);
        clickCount = CurCf.clickCount;
        addNum = CurCf.addNum;
        timeLong = CurCf.timeLong;
        minusNum = CurCf.minusNum;
        dampTime = CurCf.dampTime;
        countColor = CurCf.countColor;


        //上一等级
        if (id > 1)
        {
            TimeConfigManager.TimeConfig beforeCf = TimeConfigManager.GetTimeConfig(id - 1);
            beforeClickCount = beforeCf.clickCount;
        }

        //下一等级
        TimeConfigManager.TimeConfig nextCf = TimeConfigManager.GetTimeConfig(id + 1);
        nextClickCount = nextCf.clickCount;

        //回到起点
        MoveImg.transform.localPosition = StartPos.localPosition;
        //MoveImg.gameObject.GetComponent<Move>().Apply(timeLong);
    }

    public void OnClick()
    {
        count += addNum;
        countLable.text = count.ToString();
        curRGBA = GetRGB(countColor);
        countLable.color = new Color32(curRGBA[0], curRGBA[1], curRGBA[2], curRGBA[3]);
        timer = 0;
    }

    void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
            if (timer > dampTime && count > 0)
            {
                count -= minusNum;
                countLable.text = count.ToString();
            }


            CheckStage(count);

            if (count < 0)
            {
                count = 0.00f;
                countLable.text = count.ToString();
            }

            timeLong -= Time.deltaTime;
            Debug.LogError("##timelong" + timeLong);
            //时间到
            if (timeLong <= 0)
            {
                ClickBtn.GetComponent<Button>().interactable = false;
                theGameOverPanel.SetActive(true);
                theGameOverPanel.GetComponent<GameOver>().ApplyInfo(count, curRGBA);
                running = false;

            }
        }
    }


    public byte[] GetRGB(string color)
    {
        string[] ls = color.Split(',');
        byte[] rgba = new byte[4];
        rgba[0] = (byte)Convert.ToSingle(ls[0]);
        rgba[1] = (byte)Convert.ToSingle(ls[1]);
        rgba[2] = (byte)Convert.ToSingle(ls[2]);
        rgba[3] = (byte)Convert.ToSingle(ls[3]);

        return rgba;
    }


    public void CheckStage(float count)
    {
        if (count > clickCount)
        {
            stageId += 1;
            GetConfigInfo(stageId);
            byte[] rgba = GetRGB(countColor);
            countLable.color = new Color32(rgba[0], rgba[1], rgba[2], rgba[3]);
        }
        else if (stageId > 1 && count < beforeClickCount)
        {
            stageId -= 1;
            GetConfigInfo(stageId);
            byte[] rgba = GetRGB(countColor);
            countLable.color = new Color32(rgba[0], rgba[1], rgba[2], rgba[3]);
        }
    }


    public IEnumerator TimeSliping()
    {
        while (timeLong >= 0)
        {
            yield return new WaitForSeconds(1);
            timeLong--;
        }
    }

}
