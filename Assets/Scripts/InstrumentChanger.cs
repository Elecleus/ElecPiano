using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InstrumentChanger : MonoBehaviour
{
    private static InstrumentChanger instance;

    public static InstrumentChanger Instance
    {
        get { return instance; }
    }
    public Vector3 _instrumentPanelPosition = new(0.86f, 2.6f, -0.94f);
    public Quaternion _instrumentPanelRotation = Quaternion.Euler(85f, -180f, 0);

    [DllImport("__Internal")]
    private static extern void PreImport();

    [DllImport("__Internal")]
    private static extern void SwitchInstrument(string instrument);

    public void ChangeInstrument(string instrument)
    {
        SwitchInstrument(instrument);
    }

    void Awake()
    {
        PreImport();

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

    void OnMouseDown()
    {
        gameObject.SetActive(false);
        CameraControl.Instance.Goto(_instrumentPanelPosition, _instrumentPanelRotation);
    }
}
