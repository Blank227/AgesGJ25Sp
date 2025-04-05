using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshPro testText;
    void Start()
    {
        testText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
