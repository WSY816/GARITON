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
        this.gameObject.SetActive(false);
        VideoButton.SetActive(false);
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
