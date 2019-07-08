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

    private void OnDisable()
    {
        SoundManager.Instance.StopLoopSFXMusic();
        SoundManager.Instance.StopMusic();
    }

    void Start()
    {
        EventManager.LevelStart();
        SoundManager.Instance.Play(ropeSound);
        SoundManager.Instance.LoopSFXMusic();
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
        }
    }

    private IEnumerator SwitchBool()
    {
        SoundManager.Instance.StopLoopSFXMusic();
        SoundManager.Instance.StopMusic();
        puckAnim.SetBool("isTakingFlower", true);
        yield return new WaitForSeconds(1.12f);
        puckAnim.SetBool("isTakingFlower", false);

        greenFlower.GetComponent<Animator>().SetBool("isTakingFlower", true);
        //greenFlower.SetActive(false);

        yield return new WaitForSeconds(1f);

        blueFlower.SetActive(true);
        blueFlower.GetComponent<Animator>().SetBool("isGrew", true);
    }
}
