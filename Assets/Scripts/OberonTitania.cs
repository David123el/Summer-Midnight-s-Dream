using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    List<string> reviewPaths = new List<string>(){
        "review_good",
        "review_bad",
        "review_ok",
    };

    public GameObject UICanvas;

    public void ShowReview()
    {
        int rand = Random.Range(0, reviewPaths.Count);
        var prefab = Resources.Load("Level03/" + reviewPaths[rand]);
        var anim = (GameObject)Instantiate(prefab);
        anim.SetActive(true);
        anim.transform.SetParent(UICanvas.transform, false);
        anim.transform.Find("Exit Button").GetComponent<Button>().onClick.AddListener(Exit);
        anim.transform.Find("play again Button").GetComponent<Button>().onClick.AddListener(Restart);
    }

    public void Exit() {
        GameManager.instance.ExitToMainMenu();
    }
    
    public void Restart() {
        SceneManager.LoadScene("Level_03_Scene");
    }
}
