using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

// 虚拟钢琴按键
public class PianoKey : MonoBehaviour
{
    public string bindNote;

    // 默认音符，后续可通过函数进行更改

    public int index;

    // 用于琴键排序，从 1 到 88.

    public bool pressed;

    [DllImport("__Internal")]
    private static extern void PlayNote(string noteName);

    [DllImport("__Internal")]
    private static extern void StopPlay(string noteName);

    public void GetBindNote()
    {
        string name = gameObject.name;

        Regex regex = new(@"(?<=-)[^\.]*(?=\.)|(?<=\.)([^.]*(?=$))");
        MatchCollection key_note_info = regex.Matches(name);
        // 用正则表达式从 gameObject 的名称里提取出对应的音符信息

        string noteName = key_note_info[0].Value switch
        {
            "1" => "C",
            "2" => "D",
            "3" => "E",
            "4" => "F",
            "5" => "G",
            "6" => "A",
            "7" => "B",
            "1h" => "C#",
            "2h" => "D#",
            "4h" => "F#",
            "5h" => "G#",
            "6h" => "A#",
            _ => "C",
        };
        int octave = int.Parse(key_note_info[1].Value) + 2;
        // 正则表达式输出的八度值以0开始，故+2

        bindNote = noteName + octave.ToString();
        // 完成音符绑定

        int noteIndexInOctave = key_note_info[0].Value switch
        {
            "1" => 1,
            "1h" => 2,
            "2" => 3,
            "2h" => 4,
            "3" => 5,
            "4" => 6,
            "4h" => 7,
            "5" => 8,
            "5h" => 9,
            "6" => 10,
            "6h" => 11,
            "7" => 12,
            _ => 1,
        };
        index = (octave - 2) * 12 + noteIndexInOctave;
        // 索引从 1 开始，故 -2.
        // 完成索引
    }

    public void Play()
    {
        pressed = true;
        UpdateKeyDirection();
        LightController.Instance.LightOn(index);
        PlayNote(bindNote);
    }

    public void Stop()
    {
        pressed = false;
        UpdateKeyDirection();
        LightController.Instance.LightOff();
        // 断音效果不好，故停用
        // StopPlay(bindNote);
    }

    public void UpdateKeyDirection()
    {
        if (pressed == true)
            gameObject.transform.eulerAngles = new Vector3(3, 0, 0);
        if (pressed == false)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Awake()
    {
        GetBindNote();
    }

    void OnMouseDown()
    {
        Play();
    }

    void OnMouseUp()
    {
        Stop();
    }

    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
            Play();
    }

    void OnMouseExit()
    {
        if (pressed == true)
            Stop();
    }
}
