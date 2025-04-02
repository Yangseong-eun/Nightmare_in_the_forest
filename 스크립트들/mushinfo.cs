using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mushinfo : MonoBehaviour
{
    public float health;
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        hpSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}