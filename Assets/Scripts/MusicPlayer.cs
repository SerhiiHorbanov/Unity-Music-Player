using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
	public Action<bool> OnPauseSet;
	public Action<MusicInfo> OnMusicSet;
	
	[SerializeField] private MusicInfo[] musicInfos;
	private int _currentMusicIndex;

	private AudioSource _audioSource;

	public MusicInfo CurrentMusicInfo
		=> musicInfos[_currentMusicIndex];
	private AudioClip CurrentMusicClip
		=> CurrentMusicInfo.audioClip;

	private bool IsPlaying 
		=> _audioSource.isPlaying;

	public void Next()
	{
		_currentMusicIndex++;
		
		if (_currentMusicIndex >= musicInfos.Length)
			_currentMusicIndex = 0;
		
		UpdatePlayingMusic();
	}

	public void Prev()
	{
		_currentMusicIndex--;
		
		if (_currentMusicIndex < 0)
			_currentMusicIndex = musicInfos.Length - 1;
		
		UpdatePlayingMusic();
	}

	/// <returns>Whether music is paused after the call</returns>
	public bool TogglePause()
	{
		if (_audioSource.isPlaying)
		{
			_audioSource.Pause();
		}
		else
		{
			_audioSource.Play();
		}
		
		return _audioSource.isPlaying;
	}

	private void UpdatePlayingMusic()
		=> PlayMusicByIndex(_currentMusicIndex);
	
	private void PlayMusicByIndex(int index)
	{
		if (index < 0 || index >= musicInfos.Length)
		{
			Debug.LogError("Invalid song index");
			return;
		}
		
		_currentMusicIndex = index;
		_audioSource.clip = CurrentMusicClip;
		_audioSource.Play();
		
		OnMusicSet?.Invoke(CurrentMusicInfo);
		OnPauseSet?.Invoke(false);
	}

	public void SetVolume(float volume)
	{
		_audioSource.volume = volume;
	}
	
	private void Awake()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
        
		PlayMusicByIndex(0);
	}
}
