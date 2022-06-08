using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    SettingsManager settingsManager;

    [SerializeField]
    AudioClip MenuSound;
    [SerializeField]
    AudioClip StartSound;
    [SerializeField]
    AudioClip StartMovingSound;
    [SerializeField]
    AudioClip SpeedUpSound;
    [SerializeField]
    AudioClip InvincibleSound;
    [SerializeField]
    AudioClip SmashSound;
    [SerializeField]
    AudioClip BombSound;
    [SerializeField]
    AudioClip CoinSound;
    [SerializeField]
    AudioClip HeartSound;
    [SerializeField]
    AudioClip[] SwipeSound;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        int audioOn = Globals.LoadIntFromPlayerPrefs(Globals.AudioPlayerPrefsKey, 1);
        int musicOn = Globals.LoadIntFromPlayerPrefs(Globals.MusicPlayerPrefsKey, 1);
        Globals.AudioOn = audioOn == 1 ? true : false;
        Globals.MusicOn = musicOn == 1 ? true : false;
        if (Globals.MusicOn)
            audioSource.Play();

        settingsManager = this.GetComponent<SettingsManager>();
        settingsManager.Init();
    }

    public void StartMusic()
    {
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMenuSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(MenuSound, 1f);
    }

    public void PlayStartSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(StartSound, 1f);
    }

    public void PlayStartMovingSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(StartMovingSound, 1f);
    }

    public void PlaySpeedUpSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(SpeedUpSound, .7f);
    }

    public void PlayInvincibleSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(InvincibleSound, 1f);
    }

    public void PlaySmashSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(SmashSound, 1f);
    }

    public void PlayCoinSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(CoinSound, .5f);
    }

    public void PlayHeartSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(HeartSound, 1f);
    }

    public void PlayBombSound()
    {
        if (Globals.AudioOn)
            audioSource.PlayOneShot(BombSound, 1f);
    }

    public void PlaySwipeSound()
    {
        int num = Random.Range(0, SwipeSound.Length);
        if (Globals.AudioOn)
            audioSource.PlayOneShot(SwipeSound[num], 1f);
    }

}
