using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OberonTitania : MonoBehaviour
{
    [SerializeField]
    private GameObject teddy;
    [SerializeField]
    private GameObject baby;

    [SerializeField]
    private Animator titaniaAnim;
    [SerializeField]
    private Animator babyAnim;
    [SerializeField]
    private RuntimeAnimatorController babyAnimController;

    private Vector3 teddyPos;
    private Vector3 babyPos;

    private void OnEnable()
    {
        EventManager.OnSwitchBabyTeddy += SwitchPositions;
        EventManager.OnSwitchBabyTeddy += DisableAnimators;
    }

    private void OnDisable()
    {
        EventManager.OnSwitchBabyTeddy -= SwitchPositions;
        EventManager.OnSwitchBabyTeddy -= DisableAnimators;
    }

    void Start()
    {
        teddyPos = teddy.transform.position;
        babyPos = baby.transform.position;
    }

    void Update()
    {
        
    }

    public void SwitchPositions()
    {
        teddy.transform.position = babyPos;
        baby.transform.position = teddyPos;
    }

    public void DisableAnimators()
    {
        AnimationManager.isGameOn = false;

        babyAnim.runtimeAnimatorController = babyAnimController;
    }
}
