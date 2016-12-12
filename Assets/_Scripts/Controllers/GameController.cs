using UnityEngine;
using System.Collections;

// reference to the UI namespace
using UnityEngine.UI;

// reference to manage my scenes
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // PRIVATE INSTANCE VARIABLES ++++++++++++++++++
    private int _livesValue;
    private int _scoreValue;
    private int _enemiesLeft;

    [Header("UI Objects")]
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text EnemiesLabel;

    // PUBLIC PROPERTIES +++++++++++++++++++++++++++
    public int LivesValue
    {
        get
        {
            return this._livesValue;
        }

        set
        {
            this._livesValue = value;
            if (this._livesValue <= 0)
            {

            }
            else
            {
                this.LivesLabel.text = "Lives: " + this._livesValue;
                if (this._livesValue == 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {
            this._scoreValue = value;
            this.ScoreLabel.text = "Score: " + this._scoreValue;
        }
    }

    public int EnemiesValue
    {
        get
        {
            return this._enemiesLeft;
        }

        set
        {
            this._enemiesLeft = value;
            this.EnemiesLabel.text = "Enemies Left: " + this._enemiesLeft;
            if(this._enemiesLeft == 0)
            {
                SceneManager.LoadScene("GameOver1");
            }
        }
    }




    // Use this for initialization
    void Start()
    {
        this.LivesValue = 5;
        this.ScoreValue = 0;
        this.EnemiesValue = 30;

    }

    // Update is called once per frame
    void Update()
    {
    }


}
