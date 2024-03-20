using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 startPoz;
    public GameObject fallDetector;
    SpriteRenderer spriteRenderer;
    public GameObject pauseMenuScreen;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        startPoz = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.tag == "FallDetector") 
        {
            Die();
        }
        
    }
    void Die()
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
