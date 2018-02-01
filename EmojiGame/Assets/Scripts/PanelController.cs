using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(.1f, 0, 0);
        if (this.transform.position.x > 11f)
        {
            GameManager.instance.lives--;
            Destroy(this.gameObject);
        }
	}

    private void OnMouseOver()
    {
        print("mouse");
        if (Input.GetMouseButtonDown(0))
        {
            GameObject new_emoji = Instantiate(GameManager.instance.square, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1), Quaternion.identity);
            new_emoji.transform.parent = this.transform;
        }
    }
}
