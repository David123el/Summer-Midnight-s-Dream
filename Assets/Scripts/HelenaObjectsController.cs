using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelenaObjectsController : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite blankCircle;
    [SerializeField]
    private Image circleBG;
    [SerializeField]
    private List<Sprite> greyItems = new List<Sprite>();

    public static bool isAnimOn = false;

    public HelenaObjectsAnimations objectsAnimations;

    private void Start()
    {
        //image = GetComponent<Image>();
        //Resources.UnloadUnusedAssets();
    }

    public void ChangeObject()
    {
        if (!isAnimOn && image.tag != "Grey Item")
        {
            var initImage = image.sprite;

            if (objectsAnimations.objects.Count > 0)
            {
                int rand = Random.Range(0, objectsAnimations.objects.Count);
                var img = objectsAnimations.objects[rand].sprite;

                if (image.sprite.name != "sword")
                {
                    image.sprite = img;

                    StartCoroutine(objectsAnimations.ActivateGO(initImage.name));

                    if (objectsAnimations.objects.Count <= 0 && image.sprite.name != "sword")
                    {
                        circleBG.sprite = blankCircle;

                        for (int i = 0; i < greyItems.Count; i++)
                        {
                            if (greyItems[i].name == image.sprite.name)
                            {
                                image.sprite = greyItems[i];
                                image.tag = "Grey Item";
                            }
                        }
                    }

                    objectsAnimations.objects.Remove(objectsAnimations.objects[rand]);
                }
                else if (image.sprite.name == "sword")
                {
                    StartCoroutine(objectsAnimations.ActivateSwordAnim());
                }
            }
            else if (objectsAnimations.objects.Count <= 0)
            {
                if (image.sprite.name != "sword")
                {
                    StartCoroutine(objectsAnimations.ActivateGO(initImage.name));

                    circleBG.sprite = blankCircle;

                    for (int i = 0; i < greyItems.Count; i++)
                    {
                        if (greyItems[i].name == image.sprite.name)
                        {
                            image.sprite = greyItems[i];
                            image.tag = "Grey Item";
                        }
                    }
                }
                else if (image.sprite.name == "sword")
                {
                    StartCoroutine(objectsAnimations.ActivateSwordAnim());
                }
            }
        }
    }
}