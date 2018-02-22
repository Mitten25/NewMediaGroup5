using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    private bool emojified;
    private float shakeTimer;
    private float shakeAmount;
    private float pushTimer;
	public Sprite[] sprites;
	public GameObject hitSpawn;
	public GameObject frame;

    // Use this for initialization
    void Start () {
		BoxCollider2D b = GetComponent<BoxCollider2D> ();
		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		Sprite s = sprites [Random.Range (0, sprites.Length)];
		r.sprite = s;
		b.size = r.sprite.bounds.size;
		frame.transform.localScale = new Vector3 (b.size.x/4f, b.size.y / 4f, 0);
    }

    // Update is called once per frame
    void Update () {
		this.transform.position += new Vector3(2f+Time.timeSinceLevelLoad, 0, 0)*Time.deltaTime;
		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		if (this.transform.position.x > 11f + r.size.x*5f)
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
			Destroy (GetComponent<BoxCollider2D> ());
			Instantiate(hitSpawn, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1), Quaternion.identity);
			GetComponent<AudioSource> ().Play ();
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
