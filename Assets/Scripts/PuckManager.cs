using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckManager : MonoBehaviour
{
    [SerializeField]
    private PuckDetector puckDetector;
    [SerializeField]
    private Animator puckAnim;

    [SerializeField]
    private GameObject greenFlower;
    [SerializeField]
    private GameObject blueFlower;

    [SerializeField]
    private AudioClip ropeSound;
    [SerializeField]
    private AudioClip vivaldiSound;
    [SerializeField]
    private AudioClip pullFlowerSound;
    [SerializeField]
    private AudioClip errorSound;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;
    
    private void OnDisable()
    {
        SoundManager.Instance.StopLoopSFXMusic();
        SoundManager.Instance.StopMusic();
    }

    void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        EventManager.LevelStart();
        SoundManager.Instance.Play(ropeSound);
        SoundManager.Instance.LoopSFXMusic();
        SoundManager.Instance.PlayMusic(vivaldiSound);
        SoundManager.Instance.LoopBGMusic();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (puckDetector.ReturnPuckCol() != null)
            {
                if (puckDetector.ReturnPuckCol().gameObject.tag == "Puck")
                {
                    StartCoroutine(SwitchBool());
                }
            }
            else
            {
                SoundManager.Instance.StopLoopSFXMusic();
                SoundManager.Instance.Play(errorSound);
            }
        }
    }

    private IEnumerator SwitchBool()
    {
        SoundManager.Instance.StopLoopSFXMusic();
        SoundManager.Instance.StopMusic();
        
        puckAnim.SetBool("isTakingFlower", true);
        SoundManager.Instance.Play(pullFlowerSound);
        yield return new WaitForSeconds(1.12f);
        puckAnim.SetBool("isTakingFlower", false);

        greenFlower.GetComponent<Animator>().SetBool("isTakingFlower", true);
        //greenFlower.SetActive(false);

        yield return new WaitForSeconds(1f);

        blueFlower.SetActive(true);
        blueFlower.GetComponent<Animator>().SetBool("isGrew", true);
    }
}
