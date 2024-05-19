using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Vector2 checkPointPos;
    public GameObject fallDetector;
    public SpriteRenderer spriteRenderer;
    public GameObject pauseMenuScreen;

    public Text scoreText;
    public HealthBar healthBar;
    // Start is called before the first frame update
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Start()
    {
        checkPointPos = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
    }
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.tag == "FallDetector") 
        {
            Die();
        }
     else if(collision.tag == "banana")
        {
            Scoring.totalScore++;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "trap")
        {
            healthBar.Damage(0.003f);
        }
    }
    public void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }
    IEnumerator Respawn(float delay)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(delay);
        transform.position = checkPointPos;
        spriteRenderer.enabled = true;
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
    public void GoToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
    

}
