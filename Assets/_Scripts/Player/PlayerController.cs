using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    // PRIVATE INSTANCE VARIABLES
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private GameObject _camera;
   // private GameObject _spawnPoint;
    //private GameObject _gameControllerObject;
   // private GameController _gameController;

    private float _move;
    private float _jump;
    private bool _isFacingLeft;
    private bool _isGrounded;
    private bool _isStopped = false;



    public Transform attack;
    //How far from the center of the ship should the laser be
    public float laserDistance = .6f;
    //How much time (in secounds) we should be wait before we can fire again
    public float timeBetweenFires = .3f;
    //If value is less than or equal 0, we fire
    public float timeTilNextFire = 0.0f;
    //The buttons that we can use to shoot laser
    public List<KeyCode> shootButton;


    // PUBLIC INSTANCE VARIABLES (FOR TESTING)
    public float Velocity = 15f;
    public float JumpForce = 550f;


    [Header("Sound Clips")]
    public AudioSource JumpSound;
    public AudioSource DeathSound;
    public AudioSource CoinSound;
    public AudioSource HurtSound;
    public AudioSource EnemyDeadSound;

    // Use this for initialization
    void Start()
    {
        this._initialize();
    }

    // Update is called once per frame (Physics)
    void FixedUpdate()
    {


        if (Input.GetKeyDown("z"))
            //print("space key was pressed");

        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                //Debug.Log(element);
                timeTilNextFire = timeBetweenFires;
                this._animator.SetInteger("HeroState", 3);
                ShootAttack();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;


        if (this._isGrounded)
        {
            // check if input is present for movement
            this._move = Input.GetAxis("Horizontal");
            //Debug.Log(this._move);
            if (this._move > 0f)
            {
                this._rigidbody.WakeUp();
                this._isStopped = false;
                this._animator.SetInteger("HeroState", 1);
                this._move = 1.5f;
                this._isFacingLeft = true;
                this._flip();
            }
            else if (this._move < 0f)
            {
                this._rigidbody.WakeUp();
                this._isStopped = false;
                this._animator.SetInteger("HeroState", 1);
                this._move = -1.5f;
                this._isFacingLeft = false;
                this._flip();
            }
            else
            {
                // set the animation state to "Idle"
                this._animator.SetInteger("HeroState", 0);
                this._move = 0f;
                this._isStopped = true;
                this._rigidbody.Sleep();
            }

            // check if input is present for jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._rigidbody.WakeUp();
                if (this._isStopped == true)
                {
                    this._jump = 2.5f;
                    this._isStopped = false;
                }else
                {
                    this._jump = 1.4f;
                }
                //this.JumpSound.Play();
            }

            this._rigidbody.AddForce(new Vector2(
                this._move * this.Velocity,
                this._jump * this.JumpForce),
                ForceMode2D.Force);
            
        }
        else
        {
            this._move = 0f;
            this._jump = 0f;
        }

        this._camera.transform.position = new Vector3(
            this._transform.position.x,
            this._transform.position.y+5,
            -10f);

    }

    // PRIVATE METHODS
    /**
	 * This method initializes variables and object when called
	 */
    private void _initialize()
    {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
        this._camera = GameObject.FindWithTag("MainCamera");
        //this._spawnPoint = GameObject.FindWithTag("SpawnPoint");

        //this._gameControllerObject = GameObject.Find("Game Controller");
        //this._gameController = this._gameControllerObject.GetComponent<GameController>() as GameController;

        this._move = 0f;
        this._isFacingLeft = true;
        this._isGrounded = false;
    }

    /**
	 * This method flips the character's bitmap across the x-axis
	 */
    private void _flip()
    {
        if (this._isFacingLeft)
        {
            this._transform.localScale = new Vector2(-12f, 12f);
        }
        else
        {
            this._transform.localScale = new Vector2(12f, 12f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.CompareTag("DeathZone"))
        {
            // move the player's position to the spawn point's position
            this._transform.position = this._spawnPoint.transform.position;
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            Debug.Log("Perdeu 01");
            Debug.Log(this._gameController.LivesValue);
            if (this._gameController.LivesValue == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        
        
        if (other.gameObject.CompareTag("Enemy"))
        {            
            // move the player's position to the spawn point's position
            this._transform.position = this._spawnPoint.transform.position;
            this.HurtSound.Play();
            this._gameController.LivesValue -= 1;
            Debug.Log("Perdeu 01");
            Debug.Log(this._gameController.LivesValue);
            if (this._gameController.LivesValue == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        */
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        this._animator.SetInteger("HeroState", 2);
        this._isGrounded = false;
    }

    // debug if player lands on object's head
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.EnemyDeadSound.Play();
            Destroy(other.gameObject);
            this._gameController.EnemiesValue -= 1;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            this.CoinSound.Play();
            this._gameController.ScoreValue += 100;
        }
        */

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

        if (this._isFacingLeft)
        {
            this.attack.localScale = new Vector2(-7f, 7f);
        }
        else
        {
            this.attack.localScale = new Vector2(7f, 7f);
        }

        Instantiate(attack, laserPos, this.transform.rotation);
        
    }
}
