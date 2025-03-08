using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Music Info", order = 1)]
public class MusicInfo : ScriptableObject
{
    public string musicName;
    public string artistName;
    
    public AudioClip audioClip;
}
