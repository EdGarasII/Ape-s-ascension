using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    public Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<RectTransform>();
        if (Health.totalHealth < 0.8f)
        {
            barImage.color = Color.yellow;
        }
        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        barImage = GetComponent<Image>();
        SetSize(Health.totalHealth);
    }
    public void Damage(float damage)
    {
        if((Health.totalHealth -= damage) >= 0f) 
        {
            Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 0f;
        }
        if (Health.totalHealth < 0.8f)
        {
            barImage.color = Color.yellow;
        }
        SetSize(Health.totalHealth);
        if (Health.totalHealth < 0.3f) 
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
        if (Health.totalHealth < 0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void SetSize(float size)
    {
        bar.localScale = new Vector3 (size, 1f);
    }

}
