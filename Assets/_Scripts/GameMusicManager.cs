using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    public static GameMusicManager instance;

    [SerializeField] private List<AudioClip> audioList;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int currentAudioTrackNumber;

    [SerializeField] private Animator anim;

    private int lastSelectedIndex = 0;
    private float defaultVolume;

    private void OnEnable()
    {
        // subscribe to events to change music slowly
        InGameUIEventManager.restartLevel += ShuffleMusic;
        InGameUIEventManager.bgmSoundToggle += BGMSoundToggle;
    }

    private void OnDisable()
    {
        // unsubscribe to events to change music slowly
        InGameUIEventManager.restartLevel -= ShuffleMusic;
        InGameUIEventManager.bgmSoundToggle -= ShuffleMusic;
    }

    private void Awake()
    {
        if (GameMusicManager.instance == null)
        {
            GameMusicManager.instance = this;
        }
        else
        {
            if (GameMusicManager.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        defaultVolume = musicSource.volume;
    }

    // called via animator
    public void ChangeMusic()
    {
        // get a random index for music
        int randomIndex = RandomIndexFinder(lastSelectedIndex);
        lastSelectedIndex = randomIndex;

        musicSource.clip = audioList[randomIndex];
        musicSource.Play();
    }

    private void ShuffleMusic()
    {
        anim.SetTrigger("SlowDown");
    }

    // method to find a random audio clip to ensuring current track not being repeated
    private int RandomIndexFinder(int selectedNumber)
    {
        int number = Random.Range(0, audioList.Count);
        return number == selectedNumber ? RandomIndexFinder(selectedNumber) : number;
    }

    private void BGMSoundToggle()
    {
        musicSource.volume = musicSource.volume <= 0f ? defaultVolume : 0f;
        anim.enabled = !(anim.enabled);
    }
}
