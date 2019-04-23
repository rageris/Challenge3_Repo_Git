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

    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private bool playerWon;

    private int score;
    private BGScroller bgScroller;
    private ParticleSystem psClose;
    private ParticleSystem psFar;

    private AudioSource mainMusic;
    public AudioSource winMusic;
    public AudioSource loseMusic;
    
    void Start()
    {
        bgScroller = background.GetComponent<BGScroller>();

        psClose = starFieldClose.GetComponent<ParticleSystem>();

        psFar = starFieldDistant.GetComponent<ParticleSystem>();

        mainMusic = GetComponent<AudioSource>();

        gameOver = false;

        restart = false;

        restartText.text = "";

        gameOverText.text = "";

        score = 0;

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
