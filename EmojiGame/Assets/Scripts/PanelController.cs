using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    private bool emojified;
    private float shakeTimer;
    private float shakeAmount;
    private float pushTimer;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        this.transform.position += new Vector3(.1f, 0, 0);
        if (this.transform.position.x > 11f)
        {
            if ( !emojified ) {
                GameManager.instance.lives--;
            }
            Destroy(transform.parent.gameObject);
        }
        if ( shakeTimer > 0 ) {
            shakeTimer -= Time.deltaTime;
            float shake = shakeTimer*shakeAmount;
            transform.position += new Vector3( Random.Range(-shake, shake), Random.Range(-shake, shake), 0 );
        }
        if ( pushTimer > 0 ) {
            pushTimer -= Time.deltaTime*4f;
            float scale = 1f-(Mathf.Sin(((Mathf.PI/2f)-pushTimer)*2f))*0.25f;
            transform.localScale = new Vector3( scale, scale, 1f );
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shake( 1f, 0.2f );
            Push();
            emojified = true;
            GameObject new_emoji = Instantiate(GameManager.instance.square, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1), Quaternion.identity);
            new_emoji.GetComponent<SpriteRenderer>().sprite = GameManager.instance.emojis[Random.Range(0, GameManager.instance.emojis.Count)];
            new_emoji.transform.parent = this.transform;
        }
    }

    private void Shake( float amount, float time ) {
        if ( time > 1f ) { time = 1f; }
        shakeTimer = time;
        shakeAmount = amount;
    }

    private void Push() {
        pushTimer = Mathf.PI/2f;
    }
}
