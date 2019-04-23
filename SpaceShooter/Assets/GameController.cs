using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject starFieldClose;
    public GameObject starFieldDistant;
    public GameObject background;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;

    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text hardModeText;

    private bool gameOver;
    private bool restart;
    private bool playerWon;
    private bool hardModeEnabled;

    private int score;
    private BGScroller bgScroller;
    private Mover mover;
    private Mover mover1;
    private Mover mover2;
    private ParticleSystem psClose;
    private ParticleSystem psFar;

    private AudioSource mainMusic;
    public AudioSource winMusic;
    public AudioSource loseMusic;
    
    void Start()
    {
        hardModeText.text = "";

        hardModeEnabled = false;

        mover = asteroid1.GetComponent<Mover>();

        mover1 = asteroid2.GetComponent<Mover>();

        mover2 = asteroid3.GetComponent<Mover>();

        bgScroller = background.GetComponent<BGScroller>();

        psClose = starFieldClose.GetComponent<ParticleSystem>();

        psFar = starFieldDistant.GetComponent<ParticleSystem>();

        mainMusic = GetComponent<AudioSource>();

        gameOver = false;

        restart = false;

        restartText.text = "";

        gameOverText.text = "";

        score = 0;

        mover.speed = -2.5f;

        mover1.speed = -2.5f;

        mover2.speed = -2.5f;

        UpdateScore();

        StartCoroutine(SpawnWaves());
    }
    
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quitted");
        }


        if (playerWon == true)
        {
            var close = psClose.main;
            close.simulationSpeed = 100f;

            var far = psFar.main;
            far.simulationSpeed = 100f;

            mainMusic.Stop();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            hardModeEnabled = true;

            mover.speed = -4.5f;
            mover1.speed = -4.5f;
            mover2.speed = -4.5f;
            Debug.Log("Hard Mode Activated");

            if(hardModeEnabled == true)
            {
                hardModeText.text = "Hard Mode Enabled";
            }
        }

    }
   
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
            
            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart";

                restart = true;

                break;
            }
        }
    }
    
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        UpdateScore();
    }
    
    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            playerWon = true;

            bgScroller.scrollSpeed = 3f;

            gameOverText.text = "You Win! GAME CREATED BY [JOEL ARROYO]";

            winMusic.Play();

            gameOver = true;

            restart = true;
        }
    }
    
    public void GameOver()
    {
        gameOverText.text = "Game Over!";

        mainMusic.Stop();

        loseMusic.Play();

        gameOver = true;
    }
    
}
