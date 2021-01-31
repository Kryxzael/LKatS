using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFadeIn : MonoBehaviour
{

    private Light light;
    private float maxIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponent<Light>();
        maxIntensity = light.intensity;
        light.intensity = -maxIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (light.intensity < maxIntensity)
        {
            light.intensity += Time.deltaTime * (maxIntensity / 2);
        }
    }
}
