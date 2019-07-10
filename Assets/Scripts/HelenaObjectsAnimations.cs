//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelenaObjectsAnimations : MonoBehaviour
{
    public List<GameObject> animations = new List<GameObject>();
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

    List<string> indexer = new List<string>(new string[]{
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
    });
    public IEnumerator ActivateGO(string name)
    {
        if (!HelenaObjectsController.isAnimOn)
        {
            Debug.Log(name);
            Debug.Log(animations.Count);
            int i = indexer.IndexOf(name);
            Debug.Log(i);
            //for (int i = 0; i < animations.Count; i++)
            {
                var anim = Instantiate(animations[i]);
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
            var swordGameObject = Instantiate(swordGO);
            swordGameObject.transform.SetParent(animationNode.transform, false);
            swordGameObject.SetActive(true);

            HelenaObjectsController.isAnimOn = true;

            float delay = swordGameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
            yield return new WaitForSeconds(delay);

            swordGameObject.SetActive(false);
            Destroy(swordGameObject);
            var stGO = Instantiate(swordTalkGO);
            stGO.transform.SetParent(animationNode.transform, false);
            stGO.SetActive(true);

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
