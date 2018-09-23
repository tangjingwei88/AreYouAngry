using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConfigManager : ProtoBase {

    public class TimeConfig
    {
        public int stageId;
        public float clickCount;
        public float addNum;
        public float timeLong;
        public float minusNum;
        public float dampTime;
        public string countColor;

        public TimeConfig(m.TimeConfig s)
        {
            stageId = s.StageID;
            addNum = s.AddNum;
            minusNum = s.MinusNum;
            dampTime = s.dampTime;
            clickCount = s.ClickCount;
            countColor = s.countColor;
            timeLong = s.TimeLong;
        }

        public TimeConfig()
        { }
    }

    public static Dictionary<int, TimeConfig> timeConfigDic = new Dictionary<int, TimeConfig>();

    public static void Init()
    {
        ReadData();
    }

    private static void ReadData()
    {
        List<m.TimeConfig> timeConfig = LoadPoto<m.TimeConfig>("TimeConfig");
        for (int i = 0; i < timeConfig.Count; i++)
        {
            m.TimeConfig sc = timeConfig[i];
            TimeConfig script = new TimeConfig(sc);
            timeConfigDic.Add(script.stageId,script);
        }
    }


    public static TimeConfig GetTimeConfig(int ID)
    {
        if (timeConfigDic.ContainsKey(ID))
        {
            return timeConfigDic[ID];
        }
        else {
            Debug.LogError("GetTimeConfig failed at ID = " + ID);
            return new TimeConfig();
        }
    }

}
