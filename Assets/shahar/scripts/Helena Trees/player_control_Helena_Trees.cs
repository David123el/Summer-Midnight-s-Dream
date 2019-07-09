using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_Helena_Trees : MonoBehaviour
{
 
    public Animator player_animsion,heart_animsion;

    public float run_time_out = 15, run_time_out_save;


    private Touch touch_pos;
    [SerializeField]
    private bool on_a_tree = false, can_use_climbing = false, can_get_heart = false, got_heart = false;
    public bool got_hit = false,player_stop=false,runing_use=false;
    private Vector2 begin_touch_position, end_touch_position;

    [SerializeField]
    private ScrollingBackground  BG1, BG2, BG3, BG4,BG5;

    public Helena_Trees_game_manger Helena_Trees_game_manger;

    [SerializeField]
    private AudioSource player_efx,out_player_efx,cat_efx;
    [SerializeField]
    private AudioClip walk, run,grab_tree,cat_atk;

    
    public GameObject end_music, talk_end;
    public void play_fin_game_talk()
    {
        talk_end.SetActive(true);
    }
    public void fin_game_music_play()
    {
        end_music.SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {
        run_time_out_save = run_time_out;
    }

    public void game_reset()
    {
        Helena_Trees_game_manger.play_level_again();
    }
    // Update is called once per frame
    void Update()
    {
        if (Helena_Trees_game_manger.game_end==false)
        {
            if (runing_use)
            {
                if (run_time_out<=0)
                {
                    player_animsion.SetBool("run", false);
                    BG1.Speed = BG1.speed_walk_save;
                    BG2.Speed = BG2.speed_walk_save;
                    BG3.Speed = BG3.speed_walk_save;
                    BG4.Speed = BG4.speed_walk_save;
                    BG5.Speed = BG5.speed_walk_save;
                    runing_use = false;
                    run_time_out = run_time_out_save;
                }
                else
                {
                    run_time_out -= Time.deltaTime;
                }
            }
            if (Input.touchCount > 0)
            {
                touch_pos = Input.GetTouch(0);

                switch (touch_pos.phase)
                {
                    case TouchPhase.Began:
                        begin_touch_position = touch_pos.position;
                        break;
                    case TouchPhase.Moved:
                        end_touch_position = touch_pos.position;
                        if (end_touch_position != begin_touch_position)
                        {

                        }
                        break;
                    case TouchPhase.Ended:
                        end_touch_position = touch_pos.position;
                        if (end_touch_position != begin_touch_position)
                        {
                            if (on_a_tree && can_get_heart)
                            {
                                player_animsion.SetBool("got heart", true);
                                player_animsion.SetBool("clim", false);
                            }
                        }
                        if (end_touch_position == begin_touch_position)
                        {
                            if (can_use_climbing && on_a_tree == false && got_hit == false)
                            {
                                player_efx.enabled = false;
                                BG1.stop_moving = true;
                                BG2.stop_moving = true;
                                BG3.stop_moving = true;
                                BG4.stop_moving = true;
                                BG5.stop_moving = true;
                                player_animsion.SetBool("clim", true);
                                player_stop = true;
                                can_use_climbing = false;
                            }
                            if (on_a_tree)
                            {
                                off_clim();
                            }
                        }


                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            if (Helena_Trees_game_manger.player_lose)
            {
                player_efx.enabled = false;
                BG1.stop_moving = true;
                BG2.stop_moving = true;
                BG3.stop_moving = true;
                BG4.stop_moving = true;
                BG5.stop_moving = true;
            }

        }
        
    }
    public void off_win()
    {
        player_efx.enabled = false;
        BG1.stop_moving = true;
        BG2.stop_moving = true;
        BG3.stop_moving = true;
        BG4.stop_moving = true;
        BG5.stop_moving = true;
    }
    public void off_clim()
    {
        player_animsion.SetBool("clim", false);
    }
    public void acive_heart_animsion()
    {
        heart_animsion.SetBool("take", true);
    }
    public void heart_end()
    {
        Helena_Trees_game_manger.end_game_time = Helena_Trees_game_manger.end_game_time + Helena_Trees_game_manger.time_add;
        Helena_Trees_game_manger.heat.SetActive(false);
        player_animsion.SetBool("got heart", false);
        player_animsion.SetBool("run", true);
       
        BG1.Speed = BG1.speed_run;
        BG2.Speed = BG2.speed_run;
        BG3.Speed = BG3.speed_run;
        BG4.Speed = BG4.speed_run;
        BG5.Speed = BG5.speed_run;
        Helena_Trees_game_manger.heart_use = false;
        player_efx.clip = run;
        can_get_heart = false;
        runing_use = true;
    }
    public void set_off_tree()
    {
        player_efx.enabled = true;
        on_a_tree = false;
        player_animsion.SetBool("on tree", false);
        BG1.stop_moving = false;
        BG2.stop_moving = false;
        BG3.stop_moving = false;
        BG4.stop_moving = false;
        BG5.stop_moving = false;
        player_stop = false;
        player_efx.Play();


    }


    public void On_Cat_Attack_end()
    {
        player_efx.enabled = true;
        player_efx.Play();
        BG1.stop_moving = false;
        BG2.stop_moving = false;
        BG3.stop_moving = false;
        BG4.stop_moving = false;
        BG5.stop_moving = false;
        player_stop = false;
        player_animsion.SetBool("dead", false);
        player_animsion.SetBool("clim", false);
        got_hit = false;
        Helena_Trees_game_manger.cat_use = false;
        on_a_tree = false;
    }
    public void set_on_tree()
    {
        out_player_efx.clip= grab_tree;
        out_player_efx.Play();
        on_a_tree = true;
        player_animsion.SetBool("on tree", true);
 

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.GetComponent<grab_point>().info_on_use)
        {
            case grab_point.what_objact.tree:
                can_use_climbing = true;
                break;
            case grab_point.what_objact.enemy:
                collision.gameObject.GetComponent<enemy_control>().stop_moving = true;
                if (on_a_tree==false)
                {
                    cat_efx.PlayOneShot(cat_atk);
                    player_efx.enabled = false;
                    player_efx.clip = walk;
     
                    got_hit = true;
                    BG1.stop_moving = true;
                    BG2.stop_moving = true;
                    BG3.stop_moving = true;
                    BG4.stop_moving = true;
                    BG5.stop_moving = true;
                    player_animsion.SetBool("dead", true);
                   
                    collision.gameObject.SetActive(false);
                    Helena_Trees_game_manger.end_game_time = Helena_Trees_game_manger.end_game_time - Helena_Trees_game_manger.time_add;
                    collision.gameObject.transform.position = new Vector2(collision.gameObject.GetComponent<enemy_control>().start_point, collision.gameObject.transform.position.y);

                }
                else
                {
                    if (collision.gameObject.GetComponent<enemy_control>().is_waiting==false)
                    {
                        collision.gameObject.GetComponent<enemy_control>().cat_animsion.SetBool("lost", true);
                        collision.gameObject.GetComponent<enemy_control>().is_waiting = true;
                    }
                
                }
               
                break;
            case grab_point.what_objact.heart:
                can_get_heart = true;
                break;
            default:
                break;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.GetComponent<grab_point>().info_on_use)
        {
            case grab_point.what_objact.tree:
                can_use_climbing = false;
                break;
            case grab_point.what_objact.heart:
                can_get_heart = false;
                break;
            default:
                break;
        }
    }


}
