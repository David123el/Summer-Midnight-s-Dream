using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonkeyManager : MonoBehaviour
{
    public List<GameObject> blockPool = new List<GameObject>();
    public GameObject blockPrefab;
    [HideInInspector]
    public GameObject block;
    public Transform parent;
    public float speed;
    public float fallingSpeed;
    public float gravity;
    public Transform cameraAnchor;
    public Transform anchor;

    private Rigidbody2D rb;
    private Vector3 direction;

    private int counter = 0;
    private bool isClicked = false;

    [SerializeField]
    private CameraMovement cameraMove;
    [SerializeField]
    private float camStep = 30f;
    [SerializeField]
    private float blockStep = 1f;

    public Text scoreText;
    public Text timeLeftText;
    private bool isPlayTime = true;

    private int timeLeft = 60;

    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private GameObject openAnim;

    private static bool hasBeginAnimHappened = false;

    [SerializeField]
    private AudioClip[] randomClips;

    [SerializeField]
    private GameObject greyScreen;
    [SerializeField]
    private GameObject guideText;

    [SerializeField]
    private EventManager eventManager;

    [SerializeField]
    private Image bar;
    [SerializeField]
    private Sprite[] barSprites;
    private int barCounter = 0;

    [SerializeField]
    private GameObject[] reviews;

    [SerializeField]
    private GameObject lastPieceAnim;

    private void OnEnable()
    {
        EventManager.OnBlockLand += MoveCameraUp;
        EventManager.OnBlockLand += RandomDirection;
        EventManager.OnBlockLand += NextBlock;
        EventManager.OnBlockLand += UpdateScore;
        EventManager.OnBlockLand += PlayDropSound;

        //EventManager.OnLevelComplete += 
        EventManager.OnLevelFailed += ResetScene;
    }

    private void OnDisable()
    {
        EventManager.OnBlockLand -= MoveCameraUp;
        EventManager.OnBlockLand -= RandomDirection;
        EventManager.OnBlockLand -= NextBlock;
        EventManager.OnBlockLand -= UpdateScore;
        EventManager.OnBlockLand -= PlayDropSound;

        //EventManager.OnLevelComplete -= 
        EventManager.OnLevelFailed -= ResetScene;
    }

    private void Start()
    {
        GameManager.instance.Guide(greyScreen, guideText);
        eventManager.OnLevelBegin(greyScreen, guideText);

        EventManager.LevelStart();

        if (!hasBeginAnimHappened)
        {
            timeLeft = 63;
            var opening = Instantiate(openAnim);
            hasBeginAnimHappened = true;
        }

        for (int i = 0; i < blockPool.Count; i++)
        {
            blockPool[i] = Instantiate(blockPrefab, parent);
            blockPool[i].SetActive(false);
            blockPool[i].GetComponent<Rigidbody2D>().gravityScale = 0;

            if (i == 0)
            {
                blockPool[i].transform.position = new Vector3(blockPool[i].transform.position.x, blockPool[i].transform.position.y, blockPool[i].transform.position.z);
            }
            else
            {
                blockPool[i].transform.position = new Vector3(blockPool[i].transform.position.x, blockPool[i].transform.position.y + blockPool[i - 1].transform.position.y + blockStep, blockPool[i].transform.position.z);
            }
        }

        direction = Vector3.right;

        Time.timeScale = 1;
        //StartCoroutine(DecreaseTime());
    }

    private void Update()
    {
        if (counter <= 13)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isClicked = true;
            }

            if (counter >= 13)
            {
                StartCoroutine(WinLevel());
            }
        }
            

        //if (timeLeft >= 0)
        //    timeLeftText.text = ("" + timeLeft);

        //if (timeLeft <= 0)
        //    EventManager.LevelFailed();
    }

    private void FixedUpdate()
    {
        block = blockPool[counter];
        block.SetActive(true);

        //block.transform.position = new Vector3(cameraAnchor.transform.position.x, cameraAnchor.transform.position.y, 0);
        block.transform.position += direction * Time.deltaTime * speed;

        //StartCoroutine(Hold());

        if (block.transform.position.x <= -5)
        {
            direction = Vector3.right;
        }
        else if (block.transform.position.x >= 5)
        {
            direction = direction * -1;
        }

        if (counter < 13 && isClicked && isPlayTime && counter < blockPool.Count)
        {
            DetachFairies();
            DropBlock();
            PlayPinkLiquidAnimation();
            UpdateBar();
        }
    }

    private void UpdateBar()
    {
        if (barCounter < barSprites.Length)
            bar.sprite = barSprites[barCounter++];
    }

    private void DropBlock()
    {
        block.GetComponent<Rigidbody2D>().gravityScale = gravity;

        direction = Vector3.down;
        block.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * fallingSpeed);
        //block.transform.position -= new Vector3(block.transform.position.x, -block.transform.position.y * Time.deltaTime * fallingSpeed, block.transform.position.z);

        StartCoroutine(Wait());

        isClicked = false;
    }

    private IEnumerator Wait()
    {
        isPlayTime = false;
        yield return new WaitForSeconds(.3f);
        isPlayTime = true;
        EventManager.BlockLand();
    }

    private void PlayDropSound()
    {
        var rand = Random.Range(0, randomClips.Length);
        SoundManager.Instance.Play(randomClips[rand]);
    }

    private void DetachFairies()
    {
        var fairy1 = blockPool[counter].transform.GetChild(0).gameObject;
        var fairy2 = blockPool[counter].transform.GetChild(1).gameObject;

        fairy1.GetComponent<Animator>().SetBool("isFlyTime", true);
        fairy2.GetComponent<Animator>().SetBool("isFlyTime", true);
    }

    private void PlayPinkLiquidAnimation()
    {
        var fairy3 = blockPool[counter].transform.GetChild(2).gameObject;

        fairy3.SetActive(true);
    }

    private void MoveCameraUp()
    {
        if (counter >= 5)
        {
            cameraMove.MoveCameraUp(camStep);
        }
    }

    private void UpdateScore()
    {
        scoreText.text = counter.ToString();
    }

    private void NextBlock()
    {
        counter++;
    }

    private void RandomDirection()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }
    }

    private void ResetScene()
    {
        sceneController.RestartScene();
    }

    private IEnumerator Hold()
    {
        yield return new WaitForSeconds(.3f);
    }

    private IEnumerator DecreaseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    private IEnumerator WinLevel()
    {
        for (int i = 0; i < blockPool.Count; i++)
        {
            blockPool[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            blockPool[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        block.SetActive(false);
        var lastPieceGO = Instantiate(lastPieceAnim);
        lastPieceGO.SetActive(true);

        var blockCounter = 0;
        for (int i = 0; i < blockPool.Count; i++)
        {
            if (blockPool[i].activeSelf)
            {
                blockCounter++;
            }       
        }
        lastPieceGO.transform.position = new Vector3(lastPieceGO.transform.position.x, blockPool[blockCounter].transform.position.y - 3.2f, lastPieceGO.transform.position.z);

        yield return new WaitForSeconds(6f);

        int rand = Random.Range(0, reviews.Length);
        reviews[rand].SetActive(true);

        yield return new WaitForSeconds(5f);
        EventManager.LevelComplete();
        EventManager.ExitLevel();
    }
}
