using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private static CameraControl instance;

    public static CameraControl Instance
    {
        get { return instance; }
    }

    public Vector3 _defaultPosition;
    public Quaternion _defaultRotation;

    private readonly float _smoothSpeed = 15f;

    public Camera cam;

    private IEnumerator CoroutineGoto(Vector3 targetPositon, Quaternion targetRotaion)
    {
        cam.transform.GetPositionAndRotation(
            out Vector3 startPosition,
            out Quaternion startRotaion
        );

        // 计算移动时间
        float time = 0.0f;
        float moveTime = Vector3.Distance(startPosition, targetPositon) / _smoothSpeed;

        // 移动
        while (time < moveTime)
        {
            float process = time / moveTime;
            // 计算当前位置
            cam.transform.SetPositionAndRotation(
                Vector3.Lerp(startPosition, targetPositon, process),
                Quaternion.Lerp(startRotaion, targetRotaion, process)
            );
            // 更新时间
            time += Time.deltaTime;

            // 等待下一帧
            yield return null;
        }

        // 确保移动到目标位置
        cam.transform.SetPositionAndRotation(targetPositon, targetRotaion);
    }

    public void Goto(Vector3 targetPositon, Quaternion targetRotaion)
    {
        StartCoroutine(CoroutineGoto(targetPositon, targetRotaion));
    }

    public void BackToDefault()
    {
        StartCoroutine(CoroutineGoto(_defaultPosition, _defaultRotation));
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
        cam = FindObjectOfType<Camera>();
        Instance._defaultPosition = cam.transform.position;
        Instance._defaultRotation = cam.transform.rotation;
    }

    void Update()
    {
        if (Input.anyKeyDown && Input.GetKeyDown(KeyCode.Keypad0))
        {
            BackToDefault();
        }
    }
}
