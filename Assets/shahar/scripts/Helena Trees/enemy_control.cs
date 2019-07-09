using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_control : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D cat_rg;
    public float start_point;

    [SerializeField]
    private float speed = 3, wait_time=2, wait_time_save ,max_x_dist= -11.65f;

    public Animator cat_animsion;
    public bool stop_moving = false, is_waiting=false;

    public Helena_Trees_game_manger Helena_Trees_game_manger;
    private void OnDisable()
    {
        stop_moving = false;
    }
    private void Awake()
    {
        start_point = this.transform.position.x;
        wait_time_save = wait_time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (is_waiting)
        {
            if (wait_time<=0)
            {
                wait_time = wait_time_save;
                stop_moving = false;
                is_waiting = false;
                cat_animsion.SetBool("lost", false);
            }
            else
            {
                wait_time -= Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        if (stop_moving == false)
        {
            cat_rg.velocity = new Vector2(-1 * speed, cat_rg.velocity.y);
        }
        else
        {
            cat_rg.velocity = Vector2.zero;
        }
        if (this.transform.position.x<= max_x_dist && Helena_Trees_game_manger.cat_use)
        {
            Helena_Trees_game_manger.cat_use = false;
            this.transform.position = new Vector2(start_point, this.transform.position.y);
            cat_rg.velocity = Vector2.zero;
           
            this.gameObject.SetActive(false);
        }
        
    }



    }
