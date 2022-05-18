using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    Image AudioButtonImage;
    [SerializeField]
    Image MusicButtonImage;

    [SerializeField]
    Sprite AudioOnSprite;
    [SerializeField]
    Sprite AudioOffSprite;
    [SerializeField]
    Sprite MusicOnSprite;
    [SerializeField]
    Sprite MusicOffSprite;

    void Awake()
    {
        audioManager = this.GetComponent<AudioManager>();
    }

    public void Init()
    {
        AudioButtonImage.sprite = Globals.AudioOn ? AudioOnSprite : AudioOffSprite;
        MusicButtonImage.sprite = Globals.MusicOn ? MusicOnSprite : MusicOffSprite;
    }

    public void SelectAudioButton()
    {
        Globals.AudioOn = !Globals.AudioOn;
        audioManager.PlayMenuSound();
        AudioButtonImage.sprite = Globals.AudioOn ? AudioOnSprite : AudioOffSprite;
        Globals.SaveIntToPlayerPrefs(Globals.AudioPlayerPrefsKey, Globals.AudioOn ? 1 : 0);
    }

    public void SelectMusicButton()
    {
        Globals.MusicOn = !Globals.MusicOn;
        audioManager.PlayMenuSound();
        if (Globals.MusicOn)
            audioManager.StartMusic();
        else
            audioManager.StopMusic();
        MusicButtonImage.sprite = Globals.MusicOn ? MusicOnSprite : MusicOffSprite;
        Globals.SaveIntToPlayerPrefs(Globals.MusicPlayerPrefsKey, Globals.MusicOn ? 1 : 0);
    }
}
