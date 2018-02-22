using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject square;
    public List<Sprite> emojis;
    public GameObject panel;
    public GameObject GameOver;
    public AudioSource song;
    public AudioSource NERD;
    public float deadTimer = 2f;
    Coroutine c;

    public Vector3 panel_spawn;

    public int lives;
    public GameObject lives_text;

    private void Awake()
    {
        //singleton setup
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        c = StartCoroutine(SpawnPanel());
    }

    // Update is called once per frame
    void FixedUpdate () {

        lives_text.GetComponent<Text>().text = "Lives: " + lives;
        if (lives == 0)
        {
            lives--;
            song.Stop ();
            NERD.Play ();
            GameOver.SetActive (true);
            StopCoroutine (c);
            //TODO: GameOver
        }
        if (lives <= 0) {
            deadTimer -= Time.deltaTime;
            if (deadTimer <= 0 && Input.GetMouseButtonDown (0)) {
                SceneManager.LoadScene (0);
            }
        }
    }

    IEnumerator SpawnPanel()
    {
        while (true)
        {
            GameObject new_panel = Instantiate(panel);
            new_panel.transform.position = (panel_spawn + new Vector3(0, Random.Range(-3f, 3f), 0));
            yield return new WaitForSeconds(2/(Time.timeSinceLevelLoad/5f+1f));
        }
    }
}
