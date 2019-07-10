using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OberonTitania : MonoBehaviour
{
    [SerializeField]
    private GameObject teddy;
    [SerializeField]
    private GameObject baby;

    [SerializeField]
    private Animator titaniaAnim;
    [SerializeField]
    private Animator babyAnim;
    [SerializeField]
    private RuntimeAnimatorController babyAnimController;

    private Vector3 teddyPos;
    private Vector3 babyPos;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    [SerializeField]
    private GameObject[] reviews;

    [SerializeField]
    private AudioClip[] audioClips;

    private void OnEnable()
    {
        EventManager.OnSwitchBabyTeddy += SwitchPositions;
        EventManager.OnSwitchBabyTeddy += DisableAnimators;
        EventManager.OnSwitchBabyTeddy += ShowReview;
        EventManager.OnSwitchBabyTeddy += PlayBabyCrySound;
    }

    private void OnDisable()
    {
        EventManager.OnSwitchBabyTeddy -= SwitchPositions;
        EventManager.OnSwitchBabyTeddy -= DisableAnimators;
        EventManager.OnSwitchBabyTeddy -= ShowReview;
        EventManager.OnSwitchBabyTeddy -= PlayBabyCrySound;
    }

    void Start()
    {
        teddyPos = teddy.transform.position;
        babyPos = baby.transform.position;

        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        EventManager.LevelStart();

        SoundManager.Instance.PlayMusic(audioClips[1]);
    }

    void Update()
    {
        
    }

    public void SwitchPositions()
    {
        teddy.transform.position = babyPos;
        baby.transform.position = teddyPos;
    }

    public void DisableAnimators()
    {
        AnimationManager.isGameOn = false;

        babyAnim.runtimeAnimatorController = babyAnimController;
    }

    public void PlayBabyCrySound()
    {
        SoundManager.Instance.Play(audioClips[2]);
    }

    public void ShowReview()
    {
        int rand = Random.Range(0, reviews.Length);
        reviews[rand].SetActive(true);
    }
}
