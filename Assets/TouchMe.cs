using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMe : MonoBehaviour {

    public Text countLable;

    public float count = 0.00f;
    public float timer;

    public void OnClick()
    {
        count += 1.11f;
        countLable.text = count.ToString();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.02f && count > 0)
        {
            count -= 0.11f;
            if (count < 0)
            {
                count = 0.00f;
            }
            countLable.text = count.ToString();

        }


    }
}
