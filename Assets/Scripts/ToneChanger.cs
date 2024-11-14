using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneChanger : MonoBehaviour
{
    private static ToneChanger instance;

    public static ToneChanger Instance
    {
        get { return instance; }
    }
    public Vector3 _tonePanelPosition = new(4.28f, 4.85f, -0.83f);
    public Quaternion _tonePanelRotation = Quaternion.Euler(85f, -180f, 0);

    void OnMouseDown()
    {
        gameObject.SetActive(false);
        CameraControl.Instance.Goto(_tonePanelPosition, _tonePanelRotation);
    }

    void Awake()
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
}
