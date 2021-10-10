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

    private void OnEnable()
    {
        // subscribe to events to change music slowly
        InGameUIEventManager.restartLevel += ShuffleMusic;
    }

    private void OnDisable()
    {
        // unsubscribe to events to change music slowly
        InGameUIEventManager.restartLevel -= ShuffleMusic;
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
}
