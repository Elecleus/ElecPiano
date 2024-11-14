using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLight : MonoBehaviour
{
    public Light lightComponent;

    private Coroutine coroutine;

    private float currentLightLevel;

    private float coroutineRunningTime;

    // 计算某时刻的光照效果强度
    // intensity 倍数
    // time 时间
    // start 初始值，用于衔接
    private float LightLevel(float time)
    {
        if (time < 0.25)
            return (float)(-4 * (2 * time - 0.5) * (2 * time - 0.5) + 1);
        else
            return (float)(0.1 * Math.Sin(Math.PI * (time + 0.25)) + 0.9);
    }

    private float LightLevelStopping(float time, float start)
    {
        return (float)(-1 * time * time + start);
    }

    public void Go()
    {
        coroutine = StartCoroutine(Lighting());
    }

    public void Stop()
    {
        StartCoroutine(WaitForStopping());
    }

    IEnumerator Lighting()
    {
        coroutineRunningTime = 0;
        do
        {
            currentLightLevel = LightLevel(coroutineRunningTime);
            lightComponent.spotAngle = 120f * currentLightLevel;
            lightComponent.intensity = 1.7f * currentLightLevel;

            coroutineRunningTime += Time.deltaTime;
            yield return null;
        } while (coroutineRunningTime < 3);
        Stop();
    }

    IEnumerator StopLighting(float start)
    {
        coroutineRunningTime = 0;
        do
        {
            currentLightLevel = LightLevelStopping(coroutineRunningTime, start);
            lightComponent.spotAngle = 120f * currentLightLevel;
            lightComponent.intensity = 1.7f * currentLightLevel; // - 10 * coroutineRunningTime;

            coroutineRunningTime += Time.deltaTime;
            yield return null;
        } while (currentLightLevel > 0.2);
        currentLightLevel = 0;
        gameObject.SetActive(false);
        Debug.Log("here");
        Destroy(gameObject);
    }

    IEnumerator WaitForStopping()
    {
        do
        {
            yield return null;
        } while (coroutineRunningTime < 1.2);
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(StopLighting(currentLightLevel));
    }
}
