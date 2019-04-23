using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public int scoreValue;
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject pickUp;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find controller");
        }

        pickUp = GameObject.FindWithTag("PickUp");
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Hazard" || other.tag == "PickUp")
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (this.tag == "Hazard")
        {
            Instantiate(pickUp, transform.position, Quaternion.Euler(90f, 0, 0));
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);

        Destroy(other.gameObject);

        Destroy(gameObject);
    }
	
}
