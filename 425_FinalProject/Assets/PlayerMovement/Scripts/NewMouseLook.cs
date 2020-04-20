using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMouseLook : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 1.5f;
	public float smoothing = 2.0f;

	GameObject character;

	// Use this for initialization
	void Start () {
		character =  transform.root.gameObject;
	}

	public void Init() {
		mouseLook = new Vector2 (character.transform.localRotation.x, transform.rotation.y);
	} 

	// Update is called once per frame
	void Update()
	{
		if (FPSController.canRotate)
		{
			var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

			md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;

			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

		}
	}
}
