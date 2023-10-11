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
        // 플레이어 비활성화
        player.SetActive(false);

        playableDirector.Play();

        StartCoroutine(SwitchCameraAfterCutScene());
    }

    private IEnumerator SwitchCameraAfterCutScene()
    {
        // 컷씬이 재생될 동안 대기
        yield return new WaitForSeconds((float)cutsceneDuration);

        playableDirector.Stop();

        Debug.Log("컷씬 종료");

        // 고양이 비활성화
        cat.SetActive(false);
        // 플레이어 활성화
        player.SetActive(true);
        // 컷씬 카메라 비활성화
        cutsceneCamera.gameObject.SetActive(false);
        // 다른 카메라 활성화
        stageCamera.gameObject.SetActive(true);
    }
}
