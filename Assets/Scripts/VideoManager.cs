using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance;
    public VideoPlayer videoPlayer;
    public VideoPlayer WinningVideoPlayer;

    public GameObject VideoButton;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        // 隐藏视频播放器对象本身
        this.gameObject.SetActive(false);

        if (VideoButton != null)
        {
            VideoButton.SetActive(false);
        }

        // 调用 GameManager 开启黑屏模式
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PrepareToStart();
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void PlayWinningVideo()
    {
        WinningVideoPlayer.Play();
    }


    //public void StartWinningVIdeo()
    //{
    //    VideoManager.instance.PlayWinningVideo();
    //}
}
