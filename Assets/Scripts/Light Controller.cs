using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private static LightController instance;

    public static LightController Instance
    {
        get { return instance; }
    }

    public Queue<EffectLight> lights;

    public GameObject pretabEffectLight;

    public float timeBetweenLightOn;

    public GameObject Create()
    {
        float x;
        float y;
        Vector3 worldPosition;
        do
        {
            x = Random.Range(-14, 10);
            y = Random.Range(5, 9);

            worldPosition = new Vector3(x, y, -6.75f);
        } while (worldPosition.y < 5f);

        return Instantiate(
            pretabEffectLight,
            worldPosition,
            Quaternion.Euler(new Vector3(0, -180, 0))
        );
    }

    public void LightOn(int index)
    {
        GameObject light = Create();
        EffectLight lightScript = light.GetComponent<EffectLight>();
        lightScript.lightComponent = light.GetComponent<Light>();
        lightScript.lightComponent.color = Color.HSVToRGB(index / 48f, 0.7f, 1f);
        lightScript.Go();
        lights.Enqueue(lightScript);
    }

    public void LightOff()
    {
        lights.Peek().Stop();
        lights.Dequeue();
    }

    void Awake()
    {
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        lights = new();
    }
}
