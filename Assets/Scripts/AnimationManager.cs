using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private RaycastController raycastController;

    [SerializeField]
    private Animator[] animators;
    [SerializeField]
    private GameObject[] go;
    private Animation anim;
    [SerializeField]
    private AnimationClip animClip;

    private int number = 3;
    private int rand;

    [SerializeField]
    private Animator[] clickableAnimators;
    [SerializeField]
    private GameObject[] clickableGO;

    [SerializeField]
    private AudioClip[] audioClips;

    public static bool isGameOn = true;

    private void Start()
    {
        isGameOn = true;
        EventManager.LevelStart();

        for (int i = 0; i < go.Length; i++)
        {
            animators[i] = go[i].GetComponent<Animator>();
        }

        for (int j = 0; j < clickableGO.Length; j++)
        {
            clickableAnimators[j] = clickableGO[j].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (isGameOn)
        {
            StartCoroutine(GenerateRandomNumber());

            if (rand == number)
            {
                SetAnimatorBool("isPlay", animators);
                SoundManager.Instance.Play(audioClips[0]);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                var col = raycastController.ReturnCollider2D(Input.mousePosition);

                if (col != null)
                {
                    if (col.gameObject.tag == "Oberon")
                    {
                        SetAnimatorBool("isClicked", clickableAnimators);
                        SoundManager.Instance.Play(audioClips[0]);
                        //EventManager.LevelComplete();
                    }
                }
            }
        }
    }

    private void ActivateAnimators()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            if (!animators[i].enabled)
            {
                animators[i].enabled = true;
            }
        }
    }

    private void SetAnimatorBool(string boolName, Animator[] animators)
    {
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i].GetBool(boolName).Equals(false))
            {
                animators[i].SetBool(boolName, true);
            }

            StartCoroutine(DelayAndSetBoolToFalse(animators, i, boolName));
        }
    }

    private void PlayAnimation()
    {
        //animation.clip = animClip;
        anim.Play();
    }

    private IEnumerator DelayAndSetBoolToFalse(Animator[] anim, int index, string boolName)
    {
        yield return new WaitForSeconds(1f);
        anim[index].SetBool(boolName, false);
    }

    private IEnumerator GenerateRandomNumber()
    {
        yield return new WaitForSeconds(4);
        rand = Random.Range(0, 100);
    }
}
