using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelenaObjectsAnimations : MonoBehaviour
{
    public List<GameObject> animations = new List<GameObject>();
    public List<Image> objects = new List<Image>();

    [SerializeField]
    private GameObject staticBG;

    [SerializeField]
    private GameObject swordGO;
    [SerializeField]
    private GameObject swordTalkGO;

    public IEnumerator ActivateGO(string name)
    {
        if (!HelenaObjectsController.isAnimOn)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                var anim = Instantiate(animations[i]);
                if (anim.GetComponent<HelenaObjectsEnum>().objectType.ToString() == name.ToLower())
                {
                    staticBG.SetActive(false);
                    anim.SetActive(true);

                    HelenaObjectsController.isAnimOn = true;

                    float delay = anim.GetComponent<Animation>().clip.length;
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
            swordGameObject.SetActive(true);

            HelenaObjectsController.isAnimOn = true;

            float delay = swordGameObject.GetComponent<Animation>().clip.length;
            yield return new WaitForSeconds(delay);

            swordGameObject.SetActive(false);
            Destroy(swordGameObject);
            Instantiate(swordTalkGO);
            swordTalkGO.SetActive(true);

            delay = swordTalkGO.GetComponent<Animation>().clip.length;
            yield return new WaitForSeconds(delay);

            swordTalkGO.SetActive(false);
            //Destroy(swordTalkGO);
            staticBG.SetActive(true);

            HelenaObjectsController.isAnimOn = false;
        }
    }
}
