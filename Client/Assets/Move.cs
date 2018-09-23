using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour {
    public Transform StartPos;
    public GameObject StartPosImg;
    public GameObject EndPosImg;


    private float distance;
    private float moveSpeed;
    private float startTime;
    private float friction;


    void Awake()
    {
        distance = Vector3.Distance(StartPos.localPosition, EndPosImg.transform.localPosition);
    }


    // Use this for initialization
    public void Apply (float time) {
        this.StartCoroutine("ImgMove", time);
    }


    IEnumerator ImgMove(float time)
    {
        moveSpeed = distance / time;
        while (transform.localPosition != EndPosImg.transform.localPosition)
        {
            transform.localPosition = Vector3.MoveTowards(StartPosImg.transform.localPosition, EndPosImg.transform.localPosition, Time.deltaTime * moveSpeed);

            yield return 0;
        }
    }
}
