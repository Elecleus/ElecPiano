using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实体键盘按键
public class BoardKey : MonoBehaviour
{
    private static BoardKey instance;

    public static BoardKey Instance
    {
        get { return instance; }
    }

    public readonly Dictionary<KeyCode, int> keys_to_be_bound =
        new()
        {
            { KeyCode.Z, 1 },
            { KeyCode.X, 3 },
            { KeyCode.C, 5 },
            { KeyCode.V, 6 },
            { KeyCode.B, 8 },
            { KeyCode.N, 10 },
            { KeyCode.M, 12 },
            { KeyCode.A, 13 },
            { KeyCode.S, 15 },
            { KeyCode.D, 17 },
            { KeyCode.F, 18 },
            { KeyCode.G, 20 },
            { KeyCode.H, 22 },
            { KeyCode.J, 24 },
            { KeyCode.Q, 25 },
            { KeyCode.W, 27 },
            { KeyCode.E, 29 },
            { KeyCode.R, 30 },
            { KeyCode.T, 32 },
            { KeyCode.Y, 34 },
            { KeyCode.U, 36 },
            { KeyCode.Alpha1, 37 },
            { KeyCode.Alpha2, 39 },
            { KeyCode.Alpha3, 41 },
            { KeyCode.Alpha4, 42 },
            { KeyCode.Alpha5, 44 },
            { KeyCode.Alpha6, 46 },
            { KeyCode.Alpha7, 48 },
        };

    public List<int> GetKeyDown()
    {
        List<int> DownKeys = new();
        if (Input.anyKeyDown)
        {
            foreach (KeyValuePair<KeyCode, int> keyValuePair in keys_to_be_bound)
            {
                if (Input.GetKeyDown(keyValuePair.Key))
                {
                    DownKeys.Add(keyValuePair.Value);
                }
            }
        }
        return DownKeys;
    }

    public List<int> GetKeyUp()
    {
        List<int> UpKeys = new();
        foreach (KeyValuePair<KeyCode, int> keyValuePair in keys_to_be_bound)
        {
            if (Input.GetKeyUp(keyValuePair.Key))
            {
                UpKeys.Add(keyValuePair.Value);
            }
        }

        return UpKeys;
    }

    // Start is called before the first frame update
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
