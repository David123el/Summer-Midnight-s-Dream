using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaaliHamelachaController : MonoBehaviour
{
    #region fields
    public Sprite[] arrows;
    public List<GameObject> arrowsList = new List<GameObject>();

    public AudioClip rythmClip;
    public AudioClip[] clips;
    public AudioClip[] randomClips;

    public AudioClip nonoClip;

    public GameObject curtainAnim;

    public GameObject directorShowMoves;
    public GameObject directorCheers;
    public GameObject directorSitting;

    public GameObject playerMove01;
    public GameObject playerMove02;
    public GameObject playerMove03;
    public GameObject playerMove04;

    public GameObject move01;
    public GameObject move02;
    public GameObject move03;
    public GameObject move04;

    public GameObject standingStudents;

    public GameObject shakespearSpeaks;
    public AudioClip bravoClip;

    private int counter = 1;
    private int clickCounter = 0;
    private bool isPlayTime = false;

    public Collider2D[] buttons;
    public Sprite[] buttonColors;
    public SpriteRenderer simonController;

    public RaycastController raycastController;
    public SceneController sceneController;

    private Sprite arrowSprite;
    private GameObject directorSit;
    private GameObject directorCheer;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;
    #endregion

    void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        for (int i = 0; i < arrowsList.Count; i++)
        {
            int rand = Random.Range(0, arrows.Length);
            var sprite = arrows[rand];
            arrowSprite = arrowsList[i].GetComponent<Image>().sprite;
            arrowSprite = sprite;

            switch (arrowSprite.name)
            {
                case "GAME_arrow 01":
                    arrowsList[i].tag = "Green Button";
                    break;
                case "game_arrow 02":
                    arrowsList[i].tag = "Yellow Button";
                    break;
                case "game_arrow 03":
                    arrowsList[i].tag = "Blue Button";
                    break;
                case "game_arrow 04":
                    arrowsList[i].tag = "Red Button";
                    break;
                default:
                    break;
            }
        }

        //StartCoroutine(PlayAnim(curtainAnim));

        //StartCoroutine(PlayInitials(clips));

        //yield return new WaitForSeconds(curtainAnim.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length * 3);
        StartCoroutine(ShowArrows());
    }

    void Update()
    {
        if (isPlayTime)
        {
            if (directorSit != null)
                directorSit.SetActive(false);

            if (directorCheer == null)
                directorCheer = Instantiate(directorCheers);
            directorCheer.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                //ResetMoves();

                var col = raycastController.ReturnCollider2D(Input.mousePosition);
                if (col != null)
                {
                    if (arrowsList[clickCounter].tag == col.tag)
                    {
                        if (clickCounter < counter)
                        {
                            switch (arrowsList[clickCounter].tag)
                            {
                                case "Green Button":
                                    arrowsList[clickCounter].SetActive(true);
                                    StartCoroutine(ToggleControllerColors(buttonColors[0]));
                                    StartCoroutine(PlayMoveAnimationSequence(playerMove01, move01, 0));
                                    break;
                                case "Yellow Button":
                                    arrowsList[clickCounter].SetActive(true);
                                    StartCoroutine(ToggleControllerColors(buttonColors[1]));
                                    StartCoroutine(PlayMoveAnimationSequence(playerMove03, move03, 1));
                                    break;
                                case "Blue Button":
                                    arrowsList[clickCounter].SetActive(true);
                                    StartCoroutine(ToggleControllerColors(buttonColors[2]));
                                    StartCoroutine(PlayMoveAnimationSequence(playerMove04, move04, 2));
                                    break;
                                case "Red Button":
                                    arrowsList[clickCounter].SetActive(true);
                                    StartCoroutine(ToggleControllerColors(buttonColors[3]));
                                    StartCoroutine(PlayMoveAnimationSequence(playerMove02, move02, 3));
                                    break;
                                default:
                                    break;
                            }
                            clickCounter++;

                            if (clickCounter >= arrowsList.Count)
                            {
                                StartCoroutine(LevelComplete());
                            }
                        }
                        if (clickCounter == counter && clickCounter < arrowsList.Count)
                        {                            
                            StartCoroutine(Success());
                        }
                    }
                    else
                    {
                        switch (col.tag)
                        {
                            case "Green Button":
                                StartCoroutine(LevelFailed(buttonColors[0]));
                                break;
                            case "Yellow Button":
                                StartCoroutine(LevelFailed(buttonColors[1]));
                                break;
                            case "Blue Button":
                                StartCoroutine(LevelFailed(buttonColors[2]));
                                break;
                            case "Red Button":
                                StartCoroutine(LevelFailed(buttonColors[3]));
                                break;
                            default:
                                break;
                        }                 
                    }
                }
            }
        }
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1f);
        shakespearSpeaks.SetActive(true);
        SoundManager.Instance.Play(bravoClip);
        yield return new WaitForSeconds(bravoClip.length);

        sceneController.RestartScene();
        //EventManager.LevelComplete();
    }

    private IEnumerator LevelFailed(Sprite button)
    {
        StartCoroutine(ToggleControllerColors(button));
        standingStudents.SetActive(true);
        SoundManager.Instance.PlayMusic(nonoClip);
        yield return new WaitForSeconds(nonoClip.length);
        sceneController.RestartScene();
    }

    private IEnumerator Success()
    {
        isPlayTime = false;

        //yield return new WaitForSeconds(2f);
        
        counter++;
        clickCounter = 0;

        yield return new WaitForSeconds(1f);
        StartCoroutine(ShowArrows());
    }

    private IEnumerator PlayAnim(GameObject animToPlay)
    {
        animToPlay.SetActive(true);
        var delay = animToPlay.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(delay * 2);
        animToPlay.SetActive(false);
    }

    private void ResetMoves()
    {
        playerMove01.SetActive(false);
        playerMove02.SetActive(false);
        playerMove03.SetActive(false);
        playerMove04.SetActive(false);

        move01.SetActive(false);
        move02.SetActive(false);
        move03.SetActive(false);
        move04.SetActive(false);

        standingStudents.SetActive(false);
    }

    private IEnumerator PlayMoveAnimationSequence(GameObject playerMove, GameObject move, int clipIndex)
    {
        isPlayTime = false;
        SoundManager.Instance.Play(randomClips[clipIndex]);
        standingStudents.SetActive(false);
        var playerMoveGO = Instantiate(playerMove);
        playerMoveGO.SetActive(true);
        var moveGO = Instantiate(move);
        moveGO.SetActive(true);

        yield return new WaitForSeconds(randomClips[clipIndex].length);
        //yield return new WaitForSeconds(playerMove.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);

        playerMoveGO.SetActive(false);
        moveGO.SetActive(false);
        standingStudents.SetActive(true);
        isPlayTime = true;
    }

    private IEnumerator ToggleControllerColors(Sprite button)
    {
        var simonButton = simonController.sprite;

        simonController.sprite = button;

        yield return new WaitForSeconds(.1f);

        simonController.sprite = simonButton;
    }

    private IEnumerator PlayArrowShound(int clipIndex)
    {
        isPlayTime = false;
        SoundManager.Instance.Play(randomClips[clipIndex]);

        yield return new WaitForSeconds(randomClips[clipIndex].length);

        isPlayTime = true;
    }

    private IEnumerator ShowArrows()
    {
        if (directorCheer != null)
            directorCheer.SetActive(false);
        if (directorSit != null)
            directorSitting.SetActive(false);
        var directorMoves = Instantiate(directorShowMoves);
        directorMoves.SetActive(true);

        for (int j = 0; j < counter; j++)
        {
            arrowsList[j].SetActive(true);

            switch (arrowsList[j].GetComponent<Image>().sprite.name)
            {
                case "GAME_arrow 01":
                    StartCoroutine(PlayArrowShound(0));
                    //StartCoroutine(PlayMoveAnimationSequence(playerMove01, move01, 0));
                    yield return new WaitForSeconds(randomClips[0].length);
                    break;
                case "game_arrow 02":
                    StartCoroutine(PlayArrowShound(1));
                    //StartCoroutine(PlayMoveAnimationSequence(playerMove03, move03, 1));
                    yield return new WaitForSeconds(randomClips[1].length);
                    break;
                case "game_arrow 03":
                    StartCoroutine(PlayArrowShound(2));
                    //StartCoroutine(PlayMoveAnimationSequence(playerMove04, move04, 2));
                    yield return new WaitForSeconds(randomClips[2].length);
                    break;
                case "game_arrow 04":
                    StartCoroutine(PlayArrowShound(3));
                    //StartCoroutine(PlayMoveAnimationSequence(playerMove01, move01, 3));
                    yield return new WaitForSeconds(randomClips[3].length);
                    break;
                default:
                    break;
            }      
        }

        for (int i = 0; i < arrowsList.Count; i++)
        {
            arrowsList[i].SetActive(false);
        }

        directorMoves.SetActive(false);
        Destroy(directorMoves);
        directorSit = Instantiate(directorSitting);
        directorSit.SetActive(true);

        isPlayTime = true;
    }

    private IEnumerator PlayInitials(AudioClip[] clips)
    {
        SoundManager.Instance.PlayMusic(rythmClip);

        for (int i = 0; i < clips.Length; i++)
        {
            SoundManager.Instance.Play(clips[i]);
            yield return new WaitForSeconds(clips[i].length);
        }
        SoundManager.Instance.StopMusic();
        //yield return new WaitForSeconds(.5f);
        //isPlayTime = true;
    }

    private IEnumerator PlayRandomClips(AudioClip[] clips)
    {
        for (int i = 0; i < counter; i++)
        {
            var rand = Random.Range(0, 4);

            SoundManager.Instance.Play(clips[rand]);
            yield return new WaitForSeconds(clips[rand].length);
        }

        //isPlayTime = false;
    }
}
