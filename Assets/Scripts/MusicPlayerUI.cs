using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MusicPlayerUI : MonoBehaviour
{
    public UnityEvent onPaused;
    public UnityEvent onPlaying;
    
    [SerializeField] private MusicPlayer musicPlayer;
    
    [SerializeField] private TMP_Text nameText;	
    [SerializeField] private TMP_Text artistText;
    
    public void TogglePause()
    {
        if (musicPlayer.TogglePause())
        {
            onPlaying.Invoke();
            return;
        }
        
        onPaused.Invoke();
    }

    public void Next()
    {
        musicPlayer.Next();
    }

    public void Prev()
    {
        musicPlayer.Prev();
    }
    
    public void SetVolume(float volume)
    {
        musicPlayer.SetVolume(volume);
    }

    private void OnMusicChanged(MusicInfo musicInfo)
    {
        onPlaying.Invoke();
        
        nameText.text = musicInfo.musicName;
        artistText.text = "By " + musicInfo.artistName;
    }

    private void OnPauseSet(bool paused)
    {
        if (paused)
        {
            onPaused.Invoke();
            return;
        }

        onPlaying.Invoke();
    }
    
    private void Start()
    {
        musicPlayer.OnMusicSet += OnMusicChanged;
        musicPlayer.OnPauseSet += OnPauseSet;

        OnMusicChanged(musicPlayer.CurrentMusicInfo);
    }

    private void OnDestroy()
    {
        musicPlayer.OnMusicSet -= OnMusicChanged;
        musicPlayer.OnPauseSet -= OnPauseSet;
    }
}
