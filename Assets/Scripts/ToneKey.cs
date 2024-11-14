using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneKey : MonoBehaviour
{
    public int addToBaseNote;

    void Awake()
    {
        addToBaseNote = gameObject.name switch
        {
            "C" => 0,
            "C#" => 1,
            "D" => 2,
            "D#" => 3,
            "E" => 4,
            "F" => 5,
            "F#" => 6,
            "G" => 7,
            "G#" => 8,
            "A" => 9,
            "A#" => 10,
            "B" => 11,
            _ => 0,
        };
    }

    void OnMouseDown()
    {
        Debug.Log("Tone Key " + name);
        ToneChanger.Instance.gameObject.SetActive(true);
        CameraControl.Instance.BackToDefault();
        ElecPiano.Instance.addToBaseNote = addToBaseNote;
    }
}
