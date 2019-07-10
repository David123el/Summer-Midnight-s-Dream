using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHermia : MonoBehaviour
{
    [SerializeField]
    private RaycastController raycastController;

    [SerializeField]
    private SpriteRenderer punchHolder;
    [SerializeField]
    private Sprite[] punchSprites;

    [SerializeField]
    private GameObject choose;
    [SerializeField]
    private GameObject cryObj;
    [SerializeField]
    private GameObject surrenderObj;

    [SerializeField]
    private GameObject cryAnim;
    [SerializeField]
    private GameObject surrenderAnim;

    [SerializeField]
    private AudioClip punchClip;
    [SerializeField]
    private AudioClip[] punchPhraseClips;

    private int counter = 0;
    private int punchSoundCounter = 0;

    private bool isChoosingTime = false;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    [SerializeField]
    private GameObject[] reviews;

    void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        //raycastController = new RaycastController();
        EventManager.LevelStart();
    }

    void Update()
    {
        if (counter < punchSprites.Length - 1)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var col = raycastController.ReturnCollider2D(Input.mousePosition);

                if (col != null)
                {
                    if (col.gameObject.tag == "Hermia")
                    {
                        punchHolder.sprite = punchSprites[counter++];
                        SoundManager.Instance.Play(punchClip);
                        //int rand = Random.Range(0, punchPhraseClips.Length);
                        SoundManager.Instance.StopLoopBGMusic();
                        SoundManager.Instance.PlayMusic(punchPhraseClips[punchSoundCounter++]);
                        //counter++;
                    }
                }          
            }
        }

        else if (counter >= punchSprites.Length - 1 && !isChoosingTime)
        {
            StartCoroutine(DelayAndActivateChoosingScreen());
        }

        if (isChoosingTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var col = raycastController.ReturnCollider2D(Input.mousePosition);

                if (col != null)
                {
                    if (col.gameObject.tag == "Forgive Me")
                    {
                        choose.SetActive(false);
                        cryObj.SetActive(true);

                        StartCoroutine(WaitAndPlayAnim(cryAnim));
                    }
                    else if (col.gameObject.tag == "I Surrender")
                    {
                        choose.SetActive(false);
                        surrenderObj.SetActive(true);

                        StartCoroutine(WaitAndPlayAnim(surrenderAnim));
                    }
                }
            }
        }
    }

    private IEnumerator DelayAndActivateChoosingScreen()
    {
        yield return new WaitForSeconds(.5f);

        //punchHolder.gameObject.SetActive(false);
        choose.SetActive(true);

        isChoosingTime = true;
    }

    private IEnumerator WaitAndPlayAnim(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(true);

        yield return new WaitForSeconds(3f);

        int rand = Random.Range(0, reviews.Length);
        reviews[rand].SetActive(true);

        yield return new WaitForSeconds(5f);
        EventManager.LevelComplete();
        EventManager.ExitLevel();
    }
}
