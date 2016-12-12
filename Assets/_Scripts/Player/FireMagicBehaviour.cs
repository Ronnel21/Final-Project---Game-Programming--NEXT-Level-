using UnityEngine;
using System.Collections;

public class FireMagicBehaviour : MonoBehaviour {

    //How long the laser will live
    public float lifetime = 2.5f;
    //How fast will the laser move
    public float speed = 5.0f;
    //How much damage will this laser do if we hit an enemy
    public int damage = 1;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

    }
}
