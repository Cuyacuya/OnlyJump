using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player;
    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
    }

    void Update()
    {
        if(player.transform.position.y >= mainCamera.transform.position.y + yScreenHalfSize)
        {
            // 다음 스테이지가 존재하면
            mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y + yScreenHalfSize * 2, -10);
            // 스테이지 증가
        }
        else if(player.transform.position.y < mainCamera.transform.position.y - yScreenHalfSize)
        {
            // 이전 스테이지가 존재하면
            mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y - yScreenHalfSize * 2, -10);
            // 스테이지 감소 
        }
    }
}
