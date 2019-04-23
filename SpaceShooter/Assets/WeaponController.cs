using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource clip;


	void Start ()
    {
        clip = GetComponent<AudioSource>();

        InvokeRepeating("Fire", delay, fireRate);
	}
	
    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

        clip.Play();
    }
}
