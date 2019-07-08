using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuIn;
    [SerializeField]
    private GameObject menuOut;
    [SerializeField]
    private GameObject toggleOffOnStatic;
    [SerializeField]
    private GameObject toggleOffOn;
    [SerializeField]
    private GameObject toggleOnOff;

    [SerializeField]
    private Animator menuButton;

    [SerializeField]
    private GameObject aboutAnim;
    private GameObject aboutAnimGO;
    [SerializeField]
    private GameObject aboutButton;
    [SerializeField]
    private AudioClip aboutClip;
    [SerializeField]
    private AudioClip buttonClickClip;
    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private GameObject soundOnButton;
    [SerializeField]
    private GameObject soundOffButton;
    [SerializeField]
    private GameObject divider;

    [SerializeField]
    private GameObject shakespearBegin;
    [SerializeField]
    private AudioClip shakespearBeginClip;
    [SerializeField]
    private AudioClip shakespearBeginAmbientClip;

    [SerializeField]
    private AudioClip bgMusicClip;

    [SerializeField]
    private GameObject[] scenesAnims;
    [SerializeField]
    private AudioClip[] scenesClips;

    private static bool isBegin = true;
    private static bool hasTextHappened = false;

    private RaycastController rc;

    private bool isMenuOn = false;
    public static bool isSoundMuted = false;

    IEnumerator Start()
    {
        if (isBegin)
        {
            if (shakespearBegin != null)
            {
                StartCoroutine(PlayShakespearBegin(shakespearBeginAmbientClip, shakespearBeginClip));
                isBegin = false;
                yield return new WaitForSeconds(shakespearBeginAmbientClip.length);
            }
        }

        SoundManager.Instance.PlayMusic(bgMusicClip);
        SoundManager.Instance.LoopBGMusic();

        scenesAnims[GameManager.currentLevel - 1].SetActive(true);
        if (!hasTextHappened)
        {
            SoundManager.Instance.Play(scenesClips[GameManager.currentLevel - 1]);
            hasTextHappened = true;
        }

        rc = GetComponent<RaycastController>();

        if (isSoundMuted)
        {
            Mute();
        }
        else
        {
            soundOffButton.SetActive(false);
            soundOnButton.SetActive(true);
            SoundManager.Instance.UnMute();
            isSoundMuted = false;
        }
    }

    void Update()
    {
        //if (isMenuOn)
        //{
        //    StartCoroutine(ToggleGameSoundToggle(true));
        //}
        //else
        //{
        //    StartCoroutine(ToggleGameSoundToggle(false));
        //}
    }

    public void ToggleMenu()
    {
        //SoundManager.Instance.Play(buttonClickClip);

        if (!menuButton.GetBool("isSelected"))
        {
            menuIn.SetActive(true);
            menuOut.SetActive(false);
            isMenuOn = true;
            menuButton.SetBool("isSelected", true);

            aboutButton.SetActive(true);
        }
        else if(menuButton.GetBool("isSelected"))
        {
            menuIn.SetActive(false);
            menuOut.SetActive(true);
            isMenuOn = false;
            menuButton.SetBool("isSelected", false);

            aboutButton.SetActive(false);
        }
    }

    private IEnumerator PlayShakespearBegin(AudioClip ambientClip, AudioClip speakClip)
    {
        var shakGO = Instantiate(shakespearBegin);
        shakGO.SetActive(true);
        SoundManager.Instance.PlayMusic(ambientClip);
        SoundManager.Instance.Play(speakClip);
        yield return new WaitForSeconds(ambientClip.length);
        shakGO.SetActive(false);
    }

    private IEnumerator ToggleGameSoundToggle(bool toggle)
    {
        //SoundManager.Instance.Play(buttonClickClip);

        yield return new WaitForSeconds(.5f);
        toggleOffOnStatic.SetActive(toggle);
    }

    private IEnumerator DelayAndShowToggle()
    {
        yield return new WaitForSeconds(1f);

        toggleOnOff.SetActive(!toggleOnOff.activeSelf);
    }

    public void PlayAbout()
    {
        //SoundManager.Instance.Play(buttonClickClip);

        //menuButton.gameObject.SetActive(false);
        //soundOnButton.gameObject.SetActive(false);
        aboutAnimGO = Instantiate(aboutAnim);
        aboutAnimGO.SetActive(true);
        skipButton.SetActive(true);
        divider.SetActive(true);
        SoundManager.Instance.Mute();
        SoundManager.Instance.PlayMusic(aboutClip);

        StartCoroutine(DelayForAboutClip(aboutClip));
    }

    private IEnumerator DelayForAboutClip(AudioClip aboutAnim)
    {
        yield return new WaitForSeconds(aboutAnim.length);

        SkipAbout();
    }

    public void SkipAbout()
    {
        //SoundManager.Instance.Play(buttonClickClip);

        skipButton.SetActive(false);
        //aboutAnimGO.SetActive(false);
        Destroy(aboutAnimGO);
        divider.SetActive(false);
        SoundManager.Instance.UnMute();
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(bgMusicClip);
        SoundManager.Instance.LoopBGMusic();
        //SoundManager.Instance.Play(scenesClips[GameManager.currentLevel - 1]);
    }

    public void Mute()
    {
        soundOffButton.SetActive(true);
        soundOnButton.SetActive(false);
        SoundManager.Instance.Mute();
        isSoundMuted = true;
    }

    public void UnMute()
    {
        //SoundManager.Instance.Play(buttonClickClip);

        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
        SoundManager.Instance.UnMute();
        SoundManager.Instance.PlayMusic(bgMusicClip);
        SoundManager.Instance.LoopBGMusic();
        isSoundMuted = false;
    }

    public void PlayButtonSound()
    {
        SoundManager.Instance.StopLoopSFXMusic();
        SoundManager.Instance.Play(buttonClickClip);
    }
}
