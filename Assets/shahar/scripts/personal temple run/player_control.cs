using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    private Vector2 touch_point , begin_touch_position, end_touch_position;

    private float move_time, move_duration=0.1f,deltax, deltay;

   
    public Animator player_anime;

    [SerializeField]
    private Rigidbody2D player_rig;
    private bool jump = false;
    private Touch touch_pos;
    public bool can_move=true;
    public game_manger_personal_temple_run game_manger_personal_temple_run;
    public bool hit_end = true,pun_hit=false;

    [SerializeField]
    private AudioSource efx;
    [SerializeField]
    private AudioClip box_hit, men_hit, drown,paper_bump;
    public bool tep_start=false;
    public float Time_cool = 1;

    // Update is called once per frame
    void Update()
    {
        if (tep_start)
        {
            if (can_move)
            {
                if (game_manger_personal_temple_run.hit_box)
                {
                    game_manger_personal_temple_run.hit_box = game_manger_personal_temple_run.tep_off2.GetComponent<objact_move>().stop_all;
                }
                if (pun_hit == false)
                {
                    if (Input.touchCount > 0)
                    {
                        touch_pos = Input.GetTouch(0);
                        touch_point = Camera.main.ScreenToWorldPoint(touch_pos.position);
                        switch (touch_pos.phase)
                        {
                            case TouchPhase.Began:
                                deltax = touch_point.x - this.transform.position.x;
                                begin_touch_position = touch_pos.position;
                                //deltay= touch_point.y - this.transform.position.y;
                                break;
                            case TouchPhase.Moved:
                                end_touch_position = touch_pos.position;
                                if (end_touch_position != begin_touch_position)
                                {
                                    player_rig.MovePosition(new Vector2(Mathf.Clamp(touch_point.x - deltax, -5.77f, 7f), 0));
                                }

                                break;
                            case TouchPhase.Ended:
                                end_touch_position = touch_pos.position;

                                if (end_touch_position != begin_touch_position)
                                {
                                    player_rig.velocity = Vector2.zero;
                                }
                                else
                                {
                                    jump = true;
                                    player_anime.SetBool("jump", true);
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }

            }
            else
            {
                this.transform.position = Vector2.zero;
                player_anime.SetBool("lie_down", true);
                efx.enabled = false;

            }
        }
        else
        {
            if (Time_cool<=0)
            {
                if (Input.touchCount > 0)
                {
                    touch_pos = Input.GetTouch(0);
                    //touch_point = Camera.main.ScreenToWorldPoint(touch_pos.position);
                    switch (touch_pos.phase)
                    {
                        case TouchPhase.Began:

                            break;

                        case TouchPhase.Ended:
                            tep_start = true;
                            efx.enabled = true;
                            efx.Play();
                            player_anime.SetTrigger("game start");
                            break;
                        default:
                            break;
                    }

                }
            }
            else
            {
                Time_cool -= Time.deltaTime;
            }
          
        }
      
        
        
  
    }
    public void zeiro_jump()
    {
        jump = false;
        player_anime.SetBool("jump", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit_end)
        {
           
            switch (collision.gameObject.GetComponent<objact_move>().obstacle)
            {
                case objact_move.what_objact.box:
                    hit_end = false;
                    efx.PlayOneShot(box_hit);
                    game_manger_personal_temple_run.tep_off2 = collision.gameObject;
                    game_manger_personal_temple_run.hit_box = true;
                    break;
                case objact_move.what_objact.pun:
                    pun_hit = true;
                    this.transform.position = new Vector2(collision.gameObject.transform.position.x, this.transform.position.y);
                    efx.PlayOneShot(drown);
                    hit_end = false;
                    game_manger_personal_temple_run.daliy_time(11);
                    game_manger_personal_temple_run.tep_off = collision.gameObject;
                  
                    player_anime.SetBool("drown", true);

                    break;
                case objact_move.what_objact.paper:
                    if (jump==false)
                    {
                        efx.PlayOneShot(paper_bump);
                        hit_end = false;
                        game_manger_personal_temple_run.daliy_time(5);
                    }
                   
                    break;
                case objact_move.what_objact.BOYS:
                    efx.PlayOneShot(men_hit);
                    hit_end = false;
                    game_manger_personal_temple_run.play_level_again();
                    break;
                default:
                    break;
            }
        }
        
    }

}
