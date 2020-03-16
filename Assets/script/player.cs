using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    private float pos = 0;
    private Rigidbody2D rb;
    public float speed;
    private Animation anim;
    private float sec = 0;
    private float timer = 0;
    private bool jump = false;
    public int meet = 0;
    public Text text;
    public Text ftext;
    private GameObject wins;
    private GameObject again_btn;
    private GameObject gameui;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        wins = GameObject.Find("wins");
        again_btn = GameObject.Find("Finish");
        gameui = GameObject.Find("GameUI");
        wins.SetActive(false);
        again_btn.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.transform.Translate(1*(speed/100), 0, 0);
        sec += 1;
        if (jump && sec - timer > 20)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            jump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "meet")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.SetActive(false);
            meet += 1;
            text.text = "" + meet;
        }

        if (collision.gameObject.tag == "wolf")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "win")
        {
            ftext.text = "" + meet;
            wins.SetActive(true);
            again_btn.SetActive(true);
            gameui.SetActive(false);
        }
    }

    public void JumpUp()
    {
        if (pos < 1)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rb.AddForce(new Vector2(5, 50)*8);
            timer = sec;
            jump = true;
            pos += 1;
        }
    }
    public void JumpDown()
    {
        if (pos > -1)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rb.AddForce(new Vector2(5, -50) * 5);
            timer = sec;
            jump = true;
            pos -= 1;
        }
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
