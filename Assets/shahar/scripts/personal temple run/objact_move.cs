using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objact_move : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player_rig;
    [SerializeField]
    private float speed=1;
    [SerializeField]
    private Vector2 move;
    public bool hit_player;
    public bool stop_all = false;
    public GameObject play_for_get;
    public Sprite[] box_animsion;
    public SpriteRenderer box_sprrit;
    public enum what_objact {

        box,pun,paper,BOYS

    };
    public what_objact obstacle;
    private void Awake()
    {
        if (obstacle == what_objact.BOYS)
        {
            play_for_get = GameObject.FindWithTag("Player");
        
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (obstacle == what_objact.BOYS)
        {
           
            this.transform.position = new Vector2(play_for_get.transform.position.x, this.transform.position.y);
        }
    }

    private void FixedUpdate()
    {
        if (obstacle == what_objact.box && stop_all)
        {
            player_rig.velocity = Vector2.zero;
        }
        else
        {
            

       
                player_rig.velocity = new Vector2(move.x * speed, move.y * speed);
            
            
        }
        if (obstacle == what_objact.box)
        {
            if (!(this.transform.position.y<-5))
            {
                box_sprrit.sprite = box_animsion[(int)map(this.transform.position.y, 7, -5, 0, 7)];
            }
          
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && obstacle== what_objact.box)
        {
            stop_all = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && obstacle == what_objact.box)
        {
            stop_all = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="Player" && obstacle == what_objact.box)
        {
            stop_all = false;
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
}
