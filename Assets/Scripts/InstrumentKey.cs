using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentKey : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Instrument Key " + name);
        InstrumentChanger.Instance.gameObject.SetActive(true);
        CameraControl.Instance.BackToDefault();
        Debug.Log("request to change to " + gameObject.name);
        InstrumentChanger.Instance.ChangeInstrument(gameObject.name);
    }
}
