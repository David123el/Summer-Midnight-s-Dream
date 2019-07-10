using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class game_manger_personal_temple_run : MonoBehaviour
{
    [SerializeField]
    private float reset_time=3,time_now = 80, time_max ,bg_speed=0.1f,time_for_next_BG=10, time_for_next_BG_save, ran_time_obstacles_now, ran_time_obstacles_min =7, ran_time_obstacles_max=8,speed_of_invert_color=5, speed_of_invert_color_save;

    [SerializeField]
    private Slider time_slider;

    [SerializeField]
    private Material BG_Material;
    private Vector2 BG_xOffset;

    [SerializeField]
    private Texture[] Bg_sprit;

    [SerializeField]
    private Texture Bg_sprit_end;
    [SerializeField]
    private int index = 0, ran_Direction, ran_obstacles;


    [SerializeField]
    private GameObject[] obstacles_ferb_top;

    [SerializeField]
    private GameObject[] obstacles_ferb_R;
    [SerializeField]
    private GameObject[] obstacles_ferb_L;


    [SerializeField]
    private Transform[] obstacles_Position_top;
  
 
    [SerializeField]
    private Transform[] obstacles_Position_R;
    [SerializeField]
    private Transform[] obstacles_Position_L;

    [SerializeField]
    private Transform end_Herschel;

    public player_control player_control;
    public bool Game_end = false ,animsion_set=false;

    public List<GameObject> ferb_in_game;
    Scene active_scene;
    [SerializeField]
    private bool active_hit = false;
    public GameObject tep_off, tep_off2;
    public bool hit_box = false,end_of_invert=false;
    private int last_use,obstcol_point, obstcol_ferb,men_use=2;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    [SerializeField]
    private Transform canvasParent;
    [SerializeField]
    private GameObject[] reviews;

    private void OnApplicationQuit()
    {
        for (int i = 0; i < ferb_in_game.Count; i++)
        {
            Destroy(ferb_in_game[i]);
        }
        ferb_in_game.Clear();
    }
    private void OnDestroy()
    {
        for (int i = 0; i < ferb_in_game.Count; i++)
        {
            Destroy(ferb_in_game[i]);
        }
        ferb_in_game.Clear();
    }
    private void Awake()
    {
        time_max = time_now;
       BG_Material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        BG_Material.SetFloat("_Threshold", 0);
        BG_Material.SetTexture("_MainTex", Bg_sprit[index]);
        time_for_next_BG_save = time_for_next_BG;
        ran_time_obstacles_now = UnityEngine.Random.Range(ran_time_obstacles_min, ran_time_obstacles_max);
        speed_of_invert_color_save = speed_of_invert_color;
        active_scene = SceneManager.GetActiveScene();
        Resources.UnloadUnusedAssets();
        GC.Collect();

    }

    private void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        EventManager.LevelStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_control.tep_start)
        {
            if (hit_box == false)
            {
                if (Game_end == false)
                {
                    obstacles_spown();
                    if (active_hit == false)
                    {
                        player_control.hit_end = true;
                        BG_control();

                    }
                    else
                    {
                        reset_time -= Time.deltaTime;
                        if (reset_time <= 0)
                        {
                            reset_time = 1;
                            active_hit = false;
                            if (tep_off != null)
                            {
                                tep_off.SetActive(false);
                                player_control.pun_hit = false;
                                player_control.player_anime.SetBool("drown", false);
                            }
                        }
                    }

                }

                Game_time_control();
            }
        }
        
        
    }


    public void Game_time_control()
    {
        if (index>=4)
        {
            if (end_of_invert==false)
            {
                if (speed_of_invert_color <= 0)
                {
                    end_of_invert = true;
                }
                else
                {
                    speed_of_invert_color -= Time.deltaTime;
                }
            }
           
            BG_Material.SetFloat("_Threshold", (float)map2(speed_of_invert_color, speed_of_invert_color_save, 0,0,1));
        }
        if (time_now <= 0)
        {
            if (animsion_set==false)
            {
                for (int i = 0; i < ferb_in_game.Count; i++)
                {
                    Destroy(ferb_in_game[i]);
                }
                ferb_in_game.Clear();
                animsion_set = true;
                BG_Material.SetTexture("_MainTex", Bg_sprit_end);
                end_Herschel.DOMoveY(0, 8).SetEase(Ease.InOutSine).SetSpeedBased().OnComplete(() => {

                    player_control.can_move = false;
                    Game_end = true;

                    StartCoroutine(ShowReviews());
                });
            }
         
        }
        else
        {
            time_now -= Time.deltaTime;
            time_slider.value = ((float)map(time_now, 0, time_max, 1, 0));
            if (active_hit==false)
            {
                BG_xOffset = new Vector2(0, Time.time + bg_speed);
                BG_Material.SetTextureOffset("_MainTex", BG_xOffset);
            }
      
        }
    }

    public void BG_control()
    {
        if (time_for_next_BG <= 0)
        {
            time_for_next_BG = time_for_next_BG_save;
            if (index + 1 >= Bg_sprit.Length)
            {
                index = Bg_sprit.Length - 1;
            }
            else
            {
                index++;
                BG_Material.SetTexture("_MainTex", Bg_sprit[index]);
            }
        }
        else
        {
            time_for_next_BG -= Time.deltaTime;

        }
    }
    public void obstacles_spown()
    {
        if (ran_time_obstacles_now <= 0 )
        {
            if (Game_end==false)
            {
                ran_time_obstacles_now = UnityEngine.Random.Range(ran_time_obstacles_min, ran_time_obstacles_max);
                ran_Direction = UnityEngine.Random.Range(0, 3);
                if ((ran_Direction== 2 || ran_Direction == 1) && last_use== ran_Direction)
                {
                    ran_Direction = 0;
                }
                switch (ran_Direction)
                {
                    case 0:
                        obstcol_point = UnityEngine.Random.Range(0, obstacles_Position_top.Length);
                        obstcol_ferb = UnityEngine.Random.Range(0, obstacles_ferb_top.Length);
                        //if (obstcol_ferb==1 )
                        //{
                        //    //if (men_use-1<0)
                        //    //{
                        //        ferb_in_game.Add(Instantiate(obstacles_ferb_top[0], obstacles_Position_top[obstcol_point].position, Quaternion.identity));
                        //    }
                        //    else
                        //    {
                        //        men_use = men_use - 1;
                        //        ferb_in_game.Add(Instantiate(obstacles_ferb_top[obstcol_ferb], obstacles_Position_top[obstcol_point].position, Quaternion.identity));
                        //    }
                          
                        //}
                        //else
                        //{
                            ferb_in_game.Add(Instantiate(obstacles_ferb_top[obstcol_ferb], obstacles_Position_top[obstcol_point].position, Quaternion.identity));
                        //}
                      
                                
                             
                       
                        
                        last_use = 0;
                        break;
                
                    case 1:
                        ferb_in_game.Add(Instantiate(obstacles_ferb_R[UnityEngine.Random.Range(0, obstacles_ferb_R.Length )], obstacles_Position_R[UnityEngine.Random.Range(0, obstacles_Position_R.Length )].position, Quaternion.identity));
                        last_use = 1;
                        break;
                    case 2:
                        ferb_in_game.Add(Instantiate(obstacles_ferb_L[UnityEngine.Random.Range(0, obstacles_ferb_L.Length )], obstacles_Position_L[UnityEngine.Random.Range(0, obstacles_Position_L.Length )].position, Quaternion.identity));
                        last_use = 2;
                        break;
                

                    default:
                        break;
                }
            }
            
        }
        else
        {
            ran_time_obstacles_now -= Time.deltaTime;

        }
    }
    public void play_level_again()
    {
        SceneManager.LoadScene(active_scene.name);
    }
    public void daliy_time(float a)
    {
        time_for_next_BG = time_for_next_BG + a;
        time_now = time_now + a;
        if (a>=10)
        {
            if (index -1 <0)
            {
                BG_Material.SetTexture("_MainTex", Bg_sprit[index]);
            }
            else
            {
                BG_Material.SetTexture("_MainTex", Bg_sprit[index-1]);
            }
          
        }
        active_hit = true;
        reset_time = 1f;
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

    private IEnumerator ShowReviews()
    {
        int rand = UnityEngine.Random.Range(0, reviews.Length);
        var reviewGO = Instantiate(reviews[rand]);
        reviewGO.transform.SetParent(canvasParent);
        reviewGO.transform.position = Vector3.zero;
        reviewGO.transform.localScale = Vector3.one;
        reviewGO.SetActive(true);

        yield return new WaitForSeconds(5f);
        EventManager.LevelComplete();
        EventManager.ExitLevel();
    }
}
