using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElecPiano : MonoBehaviour
{
    private static ElecPiano instance;

    public static ElecPiano Instance
    {
        get { return instance; }
    }
    public int addToBaseNote = 0;

    public List<PianoKey> keys_list = new();

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

        // 琴键列表
        GameObject[] piano_keys_obj = GameObject.FindGameObjectsWithTag("Piano_key");
        foreach (GameObject gameObject in piano_keys_obj)
        {
            keys_list.Add(gameObject.GetComponent<PianoKey>());
        }
    }

    void Start()
    {
        keys_list = keys_list.OrderBy(f => f.index).ToList();
    }

    void Update()
    {
        foreach (int note in BoardKey.Instance.GetKeyDown())
        {
            if (note + addToBaseNote > 48)
                continue;
            keys_list[note - 1 + addToBaseNote].Play();
        }
        foreach (int note in BoardKey.Instance.GetKeyUp())
        {
            if (note + addToBaseNote > 48)
                continue;
            keys_list[note - 1 + addToBaseNote].Stop();
        }
    }
}
