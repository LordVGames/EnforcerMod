﻿using UnityEngine;
using RoR2;

class EnforcerLightController : MonoBehaviour
{
    private float flashDuration;
    private Light[] lights;
    private float lightStopwatch;
    private int lightFlashes;
    private ChildLocator childLocator;

    private void Start()
    {
        this.childLocator = this.gameObject.GetComponentInChildren<ChildLocator>();
        this.flashDuration = 0.3f;

        this.lights = new Light[]
        {
            this.childLocator.FindChild("LightL").GetComponentInChildren<Light>(),
            this.childLocator.FindChild("LightR").GetComponentInChildren<Light>()
        };

        var charBody = this.GetComponent<CharacterBody>();

        if (charBody)
        {
            Color lightColor = Color.red;

            switch(charBody.skinIndex)
            {
                case 0:
                    lightColor = Color.red;
                    break;
                case 1:
                    lightColor = Color.red;
                    break;
                case 2:
                    lightColor = Color.green;
                    break;
                case 3:
                    lightColor = Color.white;
                    break;
                case 4:
                    lightColor = Color.yellow;
                    break;
                case 5:
                    lightColor = Color.red;
                    break;
            }

            foreach (Light i in this.lights)
            {
                if (i) i.color = lightColor;
            }
        }
    }

    private void FixedUpdate()
    {
        HandleShoulderLights();
    }

    private void HandleShoulderLights()
    {
        this.lightStopwatch -= Time.fixedDeltaTime;

        if (this.lightStopwatch <= 0)
        {
            if (this.lights.Length > 0)
            {
                foreach (Light i in this.lights)
                {
                    if (i) i.enabled = false;
                }

                this.lightFlashes--;

                if (this.lightFlashes > 0) this.FlashLights(0);
            }
        }
    }

    private void EnableLights()
    {
        if (this.lights.Length > 0)
        {
            foreach (Light i in this.lights)
            {
                if (i) i.enabled = true;
            }
        }
    }

    public void FlashLights(int flashCount)
    {
        this.lightFlashes += flashCount;
        this.lightStopwatch = this.flashDuration;
        this.EnableLights();
    }
}