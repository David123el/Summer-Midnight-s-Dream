using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class heat_animsion : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer heart_sprit;

    [SerializeField]
    private float time_for_off = 3, save_time_off;

    [SerializeField]
    private Color start, end;

    [SerializeField]
    private Helena_Trees_game_manger Helena_Trees_game_manger;

    public Animator heart_animson;
    private void Awake()
    {
        save_time_off = time_for_off;
    }
    private void Update()
    {
        if (time_for_off<=0)
        {
            this.gameObject.SetActive(false);
            Helena_Trees_game_manger.heart_use = false;
        }
        else
        {
            time_for_off -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        this.transform.DOScale(1, 1).SetEase(Ease.InOutSine);
        heart_sprit.DOColor(start, 1).SetEase(Ease.InOutSine);
    }
    private void OnDisable()
    {
        this.transform.localScale = Vector3.zero;
        heart_sprit.color = end;
        time_for_off = save_time_off;
        heart_animson.SetBool("take", false);
    }

}
