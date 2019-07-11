using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject fullPlayButton;
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

    [SerializeField]
    private Text playerText;
    [SerializeField]
    private Text againstText;
    [SerializeField]
    private Text challengeText;
    [SerializeField]
    private Text actText;

    public string playerNameText;
    public string againstNameText;
    public string challengeTypeText;
    public string actNumberText;
    public string levelToLoad;

    private SceneController sceneController;

    [SerializeField]
    private GameObject[] levelSelections;
    [SerializeField]
    private Sprite newGameSprite;
    [SerializeField]
    private Sprite noneActiveGameSprite;
    [SerializeField]
    private Sprite[] facesSprites;
    [SerializeField]
    private Sprite[] selectedFacesSprites;

    public string buttonName;

    [SerializeField]
    private GameObject scene02Anim;
    private GameObject scene02GO;

    private void OnApplicationQuit()
    {
        Destroy(scene02GO);
    }

    private void OnDestroy()
    {
        Destroy(scene02GO);
    }

    IEnumerator Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");

        EventManager.LevelStart();

        scene02GO = Instantiate(scene02Anim);

        /*for (int i = 0; i < levelSelections.Length; i++)
        {
            if (levelSelections[i].activeSelf)
            {
                var image = levelSelections[i].GetComponent<Image>();
                if (i+1 < currentLevel)
                {
                    var rand = Random.Range(0, facesSprites.Length);
                    image.sprite = facesSprites[rand];
                }
                else if (i+1 == currentLevel)
                {
                    image.sprite = newGameSprite;
                }
                else
                {
                    image.sprite = noneActiveGameSprite;
                }
            }

            #region backup code
            //if (i < GameManager.currentLevel)
            //{
            //    var rand = Random.Range(0, facesSprites.Length);
            //    image.sprite = facesSprites[rand];
            //}
            //else if (i == GameManager.currentLevel)
            //{
            //    image.sprite = newGameSprite;
            //}
            //else
            //{
            //    image.sprite = noneActiveGameSprite;
            //}

            //if (levelSelections[i].activeSelf)
            //{
            //    if (image.sprite.name == "Game_undone_new")
            //    {
            //        var rand = Random.Range(0, facesSprites.Length);
            //        image.sprite = facesSprites[rand];
            //    }
            //    else
            //    {
            //        if (i == GameManager.currentLevel)
            //        {
            //            image.sprite = newGameSprite;
            //        }
            //        else
            //        {
            //            image.sprite = noneActiveGameSprite;
            //        }
            //    }
            //}
            #endregion
        }*/

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

        if (scenesAnims[currentLevel - 1] != null)
            scenesAnims[currentLevel - 1].SetActive(true);
        if (!hasTextHappened)
        {
            if (scenesAnims[GameManager.currentLevel - 1] != null)
                SoundManager.Instance.Play(scenesClips[currentLevel - 1]);
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

        sceneController = GetComponent<SceneController>();
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
            fullPlayButton.SetActive(true);
        }
        else if(menuButton.GetBool("isSelected"))
        {
            menuIn.SetActive(false);
            menuOut.SetActive(true);
            isMenuOn = false;
            menuButton.SetBool("isSelected", false);

            aboutButton.SetActive(false);
            fullPlayButton.SetActive(false);
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

    public void OpenFullPlayURL()
    {
        Application.OpenURL("http://www.benjamintrigalou.com/midsummer_night_dream/");
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

    public void UpdatePlayerText(string playerNameText)
    {
        playerText.text = playerNameText;
    }

    public void UpdateAgainstText(string againstNameText)
    {
        againstText.text = againstNameText;
    }

    public void UpdateChallengeText(string challengeNameText)
    {
        challengeText.text = challengeNameText;
    }

    public void UpdateActText(string actNumberText)
    {
        actText.text = actNumberText;
    }

    public void UpdateButtonLevel(string levelToLoad)
    {
        sceneController.levelToLoad = levelToLoad;
    }

    /*public void UpdateFace(string buttonName)
    {
        switch (buttonName)
        {
            case "bad_score":
                //face = selectedFacesSprites[0];
                break;
            case "good_score":
                //face = selectedFacesSprites[1];
                break;
            case "ok_score":
                //face = selectedFacesSprites[2];
                break;
            default:
                break;
        }
    }*/
}
