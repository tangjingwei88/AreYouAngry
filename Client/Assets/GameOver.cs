using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text ScoreLabel;

    public void ApplyInfo(float score,byte[] rgba)
    {
        ScoreLabel.text = score.ToString();
        ScoreLabel.color = new Color32(rgba[0], rgba[1], rgba[2], rgba[3]);
    }
}
