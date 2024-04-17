using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Vector2 startPoz;
    public GameObject fallDetector;
    public SpriteRenderer spriteRenderer;
    public GameObject pauseMenuScreen;
    // Start is called before the first frame update
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Start()
    {
        startPoz = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.tag == "FallDetector") 
        {
            Die();
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
        transform.position = startPoz;
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
