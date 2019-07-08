using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChecker : MonoBehaviour
{
    [SerializeField]
    private Transform BabyTransform;
    [SerializeField]
    private Transform TeddyTransform;
    private Vector3 babyHeight;

    void Start()
    {

    }

    void Update()
    {
        babyHeight = Camera.main.ScreenToWorldPoint(TeddyTransform.position) / Screen.height;
        Debug.Log(babyHeight);

        //if (BabyTransform.localPosition.y > 0 && TeddyTransform.localPosition.y > 0)
        //{
        //    if (BabyTransform.localPosition.y > screenHeight && TeddyTransform.localPosition.y > screenHeight)
        //    {
        //        Debug.Log("switch");
        //    }
        //}
    }
}
