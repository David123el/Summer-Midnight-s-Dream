using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class Helena_Trees_game_manger : MonoBehaviour
{
    [SerializeField]
    private float game_time_now,time_for_spon_cat=15, time_for_spon_cat_save,time_spon_heat=20, time_spon_heat_save, Helena_time;

    public float time_add=0.009f, end_game_time = 90, end_game_time_player=90;

    [SerializeField]
    private GameObject cat_enemy,time_info;
    public GameObject heat;
    private int spon_cat = 3, spon_heart = 3;

    public bool cat_use = false, heart_use = false,player_win=false,player_lose=false;
    Scene active_scene;
    public player_control_Helena_Trees player_control_Helena_Trees;
    public bool game_end = false;
    [SerializeField]
    private Slider Helena_bar, DEMITRIUS_bar;
    public GameObject rain_efx,back_grund_sound,game_music;
    //private GameObject rain;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    private void Awake()
    {
        time_for_spon_cat_save = time_for_spon_cat;
        time_spon_heat_save = time_spon_heat;
        active_scene = SceneManager.GetActiveScene();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        //rain = Instantiate(rain_efx);
        EventManager.LevelStart();
    }
    public void play_level_again()
    {
        SceneManager.LoadScene(active_scene.name);
    }
    // Update is called once per frame
    void Update()
    {
        if (game_end == false)
        {
            if (cat_use == false )
            {
                if (time_for_spon_cat <= 0)
                {
                    time_for_spon_cat = time_for_spon_cat_save;
                    cat_use = true;
                    cat_enemy.SetActive(true);
                    //if (spon_cat - 1 >= 0)
                    //{
                    //    spon_cat = spon_cat - 1;
                    //}
                }
                else
                {
                    time_for_spon_cat -= Time.deltaTime;
                }
            }
            if (heart_use == false )
            {
                if (time_spon_heat <= 0)
                {
                    time_spon_heat = time_spon_heat_save;
                    heart_use = true;
                    heat.SetActive(true);
                    //if (spon_heart - 1 >= 0)
                    //{
                    //    spon_heart = spon_heart - 1;
                    //}
                }
                else
                {
                    time_spon_heat -= Time.deltaTime;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (game_end==false)
        {
            if (player_control_Helena_Trees.got_hit==false && player_control_Helena_Trees.player_stop == false)
            {
                Helena_time += Time.deltaTime ;
            }
      
            game_time_now += Time.deltaTime;
            Helena_bar.value = ((float)map(Helena_time, 0, end_game_time_player, 0, 1));
            DEMITRIUS_bar.value = ((float)map(game_time_now, 0, end_game_time, 0, 1));
            if (Helena_time >= end_game_time_player && game_time_now < end_game_time)
            {
                player_control_Helena_Trees.player_animsion.SetBool("game end", true);
                player_win = true;
                game_end = true;

                time_info.SetActive(false);
                cat_enemy.SetActive(false);
                heat.SetActive(false);
                if (rain_efx != null)
                    rain_efx.SetActive(false);
                back_grund_sound.SetActive(false);
                game_music.SetActive(false);
            }
            if (game_time_now >= end_game_time && Helena_time < end_game_time_player)
            {
                player_control_Helena_Trees.player_animsion.SetBool("game over", true);
                player_lose = true;
                game_end = true;
               
                cat_enemy.SetActive(false);
                heat.SetActive(false);
            }
        }
      
    }

    private double map(float value, float low1, float high1, float low2, float high2)

    {

        if (value == low1)

        {

            return low2;

        }


        float range1 = high1 - low1;

        float range2 = high2 - low2;

        float result = value - low1;

        float ratio = result / range1;

        result = ratio * range2;

        result = result + low2;

        //Debug.Log(result);

        return result;

    }

    private double map2(float value, float low1, float high1, float low2, float high2)

    {

        if (value == low1)

        {

            return low2;

        }


        float range1 = high1 - low1;

        float range2 = high2 - low2;

        float result = value - low1;

        float ratio = result / range1;

        result = ratio * range2;

        result = result + low2;

        //Debug.Log(result);

        return result;

    }
}
