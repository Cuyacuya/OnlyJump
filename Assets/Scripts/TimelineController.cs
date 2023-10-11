using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    double cutsceneDuration;
    [SerializeField] GameObject cutsceneCamera;
    [SerializeField] GameObject stageCamera;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject player;

    void Start()
    {
        cutsceneDuration = playableDirector.duration;

        PlayOpeningCutScene();
    }

    private void PlayOpeningCutScene()
    {
        // �÷��̾� ��Ȱ��ȭ
        player.SetActive(false);

        playableDirector.Play();

        StartCoroutine(SwitchCameraAfterCutScene());
    }

    private IEnumerator SwitchCameraAfterCutScene()
    {
        // �ƾ��� ����� ���� ���
        yield return new WaitForSeconds((float)cutsceneDuration);

        playableDirector.Stop();

        Debug.Log("�ƾ� ����");

        // ����� ��Ȱ��ȭ
        cat.SetActive(false);
        // �÷��̾� Ȱ��ȭ
        player.SetActive(true);
        // �ƾ� ī�޶� ��Ȱ��ȭ
        cutsceneCamera.gameObject.SetActive(false);
        // �ٸ� ī�޶� Ȱ��ȭ
        stageCamera.gameObject.SetActive(true);
    }
}
