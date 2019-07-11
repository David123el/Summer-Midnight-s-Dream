//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelenaObjectsAnimations : MonoBehaviour
{
    //private GameObject[] animationsArray;
    public List<Image> objects = new List<Image>();

    public GameObject animationNode;

    [SerializeField]
    private GameObject staticBG;

    [SerializeField]
    private GameObject swordGO;
    [SerializeField]
    private GameObject swordTalkGO;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    [SerializeField]
    private GameObject[] reviews;
    private GameObject animTest;

    [SerializeField]
    private AudioClip bgMusic;
    [SerializeField]
    private AudioClip[] itemClips;
    [SerializeField]
    private AudioClip swordTalkclip;

    private void Awake()
    {
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
    }

    private void Start()
    {
        EventManager.LevelStart();

        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        SoundManager.Instance.PlayMusic(bgMusic);

        //animationsArray = Resources.LoadAll<GameObject>("Prefabs/Resources/Level01");
        //for (int i = 0; i < animationsArray.Length; i++)
        //{
        //    animTest = Instantiate(animationsArray[i]);
        //    Debug.Log(animTest.name);
        //}
    }

    //private void OnDestroy()
    //{
    //    Resources.UnloadUnusedAssets();
    //    for (int i = 0; i < animationsArray.Length; i++)
    //    {
    //        Destroy(animationsArray[i]);
    //    }
    //}

    //public IEnumerator ActivateGO(string name)
    //{
    //    if (!HelenaObjectsController.isAnimOn)
    //    {
    //        if (animTest.GetComponent<HelenaObjectsEnum>().objectType.ToString() == name.ToLower())
    //        {
    //            staticBG.SetActive(false);
    //            animTest.SetActive(true);

    //            HelenaObjectsController.isAnimOn = true;

    //            float delay = animTest.GetComponent<Animation>().clip.length;
    //            yield return new WaitForSeconds(delay);
    //        }

    //        animTest.SetActive(false);
    //        Destroy(animTest);
    //        staticBG.SetActive(true);

    //        HelenaObjectsController.isAnimOn = false;
    //    }
    //}

    List<string> indexer = new List<string>(){
        "bow",
        "bell",
        "fan",
        "globe",
        "goggles",
        "vase",
        "lantern",
        "pendulum",
        "parrot",
        "sword"
    };
    List<string> names = new List<string>(){
        "EXPORT SIZE_bow_00007",
        "EXPORT SIZE_bell_00037",
        "EXPORT SIZE_fan_00060",
        "EXPORT SIZE_globe_00074",
        "EXPORT SIZE_goggles_00033",
        "EXPORT SIZE_vase_00014",
        "EXPORT SIZE_lentern_00045",
        "EXPORT SIZE_pendulum_00099",
        "EXPORT SIZE_parrot_00032",
        "EXPORT SIZE_sward_00023",
    };
    public IEnumerator ActivateGO(string name)
    {
        if (!HelenaObjectsController.isAnimOn)
        {
            //Debug.Log(name);
            //Debug.Log(animations.Count);
            int i = indexer.IndexOf(name);
            //Debug.Log(i);
            //for (int i = 0; i < animations.Count; i++)
            {
                var prefab = Resources.Load("Level01/" + names[i]);
                var anim = (GameObject)Instantiate(prefab);
                //var anim = Instantiate(animations[i]);
                anim.transform.SetParent(animationNode.transform, false);
                //if (anim.GetComponent<HelenaObjectsEnum>().objectType.ToString() == name.ToLower())
                {
                    staticBG.SetActive(false);
                    anim.SetActive(true);

                    HelenaObjectsController.isAnimOn = true;

                    float delay = anim.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
                    yield return new WaitForSeconds(delay);
                }

                anim.SetActive(false);
                Destroy(anim);
                staticBG.SetActive(true);

                HelenaObjectsController.isAnimOn = false;
            }
        }
    }

    public IEnumerator ActivateSwordAnim()
    {
        if (!HelenaObjectsController.isAnimOn)
        {
            staticBG.SetActive(false);
            var prefab = Resources.Load("Level01/EXPORT SIZE_sward_00023");
            var swordGameObject = (GameObject)Instantiate(prefab);
            swordGameObject.transform.SetParent(animationNode.transform, false);
            swordGameObject.SetActive(true);

            HelenaObjectsController.isAnimOn = true;

            float delay = swordGameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
            yield return new WaitForSeconds(delay);

            swordGameObject.SetActive(false);
            Destroy(swordGameObject);
            var prefab2 = Resources.Load("Level01/EXPORT SIZE_sward win_00057");
            var stGO = (GameObject)Instantiate(prefab2);
            stGO.transform.SetParent(animationNode.transform, false);
            stGO.SetActive(true);
            SoundManager.Instance.Play(swordTalkclip);

            delay = stGO.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
            yield return new WaitForSeconds(delay);

            Destroy(stGO);
            //Destroy(swordTalkGO);
            staticBG.SetActive(true);

            HelenaObjectsController.isAnimOn = false;

            int rand = UnityEngine.Random.Range(0, reviews.Length);
            reviews[rand].SetActive(true);

            yield return new WaitForSeconds(5f);

            EventManager.ExitLevel();
        }
    }
}
