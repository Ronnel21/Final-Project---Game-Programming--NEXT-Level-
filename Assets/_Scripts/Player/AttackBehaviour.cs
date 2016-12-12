using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehaviour : MonoBehaviour {

    public Transform attack;
    //How far from the center of the ship should the laser be
    public float laserDistance = .6f;
    //How much time (in secounds) we should be wait before we can fire again
    public float timeBetweenFires = .3f;
    //If value is less than or equal 0, we fire
    public float timeTilNextFire = 0.0f;
    //The buttons that we can use to shoot laser
    public List<KeyCode> shootButton;

    private Animator _animator;

    private Transform _transform;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                //Debug.Log(element);
                timeTilNextFire = timeBetweenFires;
                _animator.Play(Animator.StringToHash("Hero_Attack"));
                ShootAttack();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;
    }

    void ShootAttack()
    {

        //Play the laser sound
        //audioSource.PlayOneShot(shootSound);

        //We want to position the laser in relation to out player's location
        Vector3 laserPos = this.transform.position;
        //The angle the laser will move away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        /*Calculate the position right in front of the ship's position
        laserDistance units away*/

        laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);

        if (_transform.localScale.x == -12f)
            this.attack.localScale = new Vector2(7f, 7f);
        else
            this.attack.localScale = new Vector2(-7f, 7f);

        Instantiate(attack, laserPos, this.transform.rotation);

    }
}
