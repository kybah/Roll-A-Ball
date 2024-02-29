using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerController : MonoBehaviour
{  
    public float speed = 1f;
    private Rigidbody rb;
    public int pickUpCount;
    private Timer timer;
    private bool gameOver = false;

    [Header("UI")]
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    public GameObject winPanel;
    public GameObject inGamePanel;
    


    // Start is called before the first frame update
    void Start() 
    {
        //Turn On InGamePanel
        inGamePanel.SetActive(true);
        //Turn Off Our Win Panel
        winPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the check pick ups functon
        CheckPickUps();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        float movehorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movehorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pick Up")
        {
            Destroy(other.gameObject);
            pickUpCount--;
            CheckPickUps();
        }
        


    }

 void CheckPickUps()
    {
        pickUpText.text = "Pick Ups Left: " + pickUpCount; 
        if(pickUpCount == 0)
        {
            WinGame();
        }
    }
    void WinGame()
    {
        // Set our game to true 
        gameOver = true;
        //Turn Off InGamePanel
        inGamePanel.SetActive(false);
        //Turn On Our Win Panel
        winPanel.SetActive(true);
        //Changes variables on completion
        pickUpText.color = Color.red;
        pickUpText.fontSize = 60;
        //Stop Timer
        timer.StopTimer();
        //Display our time to the win time text
        winTimeText.text = "your time was: " + timer.GetTime().ToString("F2");

        //stop the ball from moving 
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //temporary - remove when doing A2 modules 
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
