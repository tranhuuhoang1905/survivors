using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    public void UpdateSliderBar(float currentValue, float maxValue)
    {
        
        slider.value = currentValue/maxValue;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
