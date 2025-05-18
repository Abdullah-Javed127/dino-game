using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
//using PuppeteerSharp.Input; // ✅ New Input System

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    Animator anim;
    bool moved = false;


    float RightMove = 40f;
    float LeftMove = -40f;

    bool crouch = false;
    bool jump = false;

    public Slider HealthSlider;
    float healthValue = 100;

    float scorevalue = 0;
    public Text ScoreText;

    public GameObject cloud;
    public GameObject cloud1;
    public GameObject cloud2;

    public AudioSource GameSound;
    public AudioClip appleBite;
    public AudioClip coinCollection;

    public UIManager uiManager;


    float startTouchPosition, endTouchPosition;

    void Start()
    {
        anim = GetComponent<Animator>();

        HealthSlider.value = healthValue;
        ScoreText.text = "Score: " + scorevalue.ToString();
        GameSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Touchscreen.current == null) return;

        var touches = Touchscreen.current.touches;

        foreach (var touchControl in touches)
        {
            if (!touchControl.press.isPressed) continue;

            var touch = touchControl;
            var pos = touch.position.ReadValue();

            if (pos.x > Screen.width / 2)
            {
                controller.Move(RightMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("isWalk");
                UpdateHealth(-1);
                moved = true;
            }
            else if (pos.x < Screen.width / 2)
            {
                controller.Move(LeftMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("isWalk");
                UpdateHealth(-1);
                moved = true;
            }

        }

        if (!moved)
        {
            anim.SetTrigger("isIdle");
        }

        if (healthValue <= 0)
        {
            healthValue = 0;
            HealthSlider.value = healthValue;
            anim.SetTrigger("TriggerDead");

            if (uiManager != null)
                uiManager.ShowGameOverPanel();

            this.enabled = false;
        }

    }

    void Update()
    {
        if (Touchscreen.current == null) return;

        foreach (var touchControl in Touchscreen.current.touches)
        {
            var touch = touchControl;

            if (touch.press.wasPressedThisFrame)
            {
                startTouchPosition = touch.position.ReadValue().y;
            }

            if (touch.press.wasReleasedThisFrame)
            {
                endTouchPosition = touch.position.ReadValue().y;

                if (endTouchPosition > startTouchPosition) // swipe up
                {
                    transform.Translate(Vector2.up * 200 * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.StartsWith("Saw"))
        {
            UpdateHealth(-10);
        }

        if (col.gameObject.name.StartsWith("Apple"))
        {
            GameSound.PlayOneShot(appleBite);
            UpdateHealth(20);
            Destroy(col.gameObject);
        }

        if (col.gameObject.name.StartsWith("coin"))
        {
            GameSound.PlayOneShot(coinCollection);
            scorevalue += 10;
            ScoreText.text = "Score: " + scorevalue.ToString();
            Destroy(col.gameObject);
        }

        if (col.gameObject.name.StartsWith("cloud") ||
            col.gameObject.name.StartsWith("cloud1") ||
            col.gameObject.name.StartsWith("cloud2"))
        {
            transform.parent = col.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void UpdateHealth(float amount)
    {
        healthValue += amount;
        healthValue = Mathf.Clamp(healthValue, 0, 100);
        HealthSlider.value = healthValue;
    }
}
