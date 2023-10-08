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
            // ���� ���������� �����ϸ�
            mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y + yScreenHalfSize * 2, -10);
            // �������� ����
        }
        else if(player.transform.position.y < mainCamera.transform.position.y - yScreenHalfSize)
        {
            // ���� ���������� �����ϸ�
            mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y - yScreenHalfSize * 2, -10);
            // �������� ���� 
        }
    }
}
