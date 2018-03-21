using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour {

    private Slider slider;
    public Light directionalLight;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        slider.value = directionalLight.intensity;
        slider.onValueChanged.AddListener(valueChanged);
	}

    void valueChanged(float val)
    {
        directionalLight.intensity = val;
    }

}
