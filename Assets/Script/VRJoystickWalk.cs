using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class VRJoystickWalk : MonoBehaviour {
	public float speed = 3.0f;
	private CharacterController controller;
	private GvrViewer gvrViewer;
	private Transform vrHead;
	public bool moveForward;

	public Text countText;
	private int count;
	public Text winText;
	public BoxCollider finishBox;
	public Text textKumpulkan;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		gvrViewer = transform.GetChild (0).GetComponent<GvrViewer> ();
		vrHead = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");

		if(Input.GetAxis("Vertical") != 0) {
			Vector3 forward = vrHead.TransformDirection (new Vector3 (0, 0, v));
			controller.SimpleMove (forward * speed);
		}

		if(Input.GetAxis("Horizontal") != 0) {
			Vector3 right = vrHead.TransformDirection (new Vector3 (h, 0, 0));
			controller.SimpleMove (right * speed);
		}

		if(Input.GetAxis("Fire2") != 0) {
			Vector3 top = vrHead.TransformDirection (new Vector3 (0, 0, v));
			controller.SimpleMove (top * speed);
		}

		if (Input.GetButtonDown("Fire1")) {
			moveForward = !moveForward;
		}

		if (moveForward) {
			Vector3 forwardAuto = vrHead.TransformDirection (Vector3.forward);
			controller.SimpleMove (forwardAuto * speed);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("target1"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
		if (other.gameObject.CompareTag("finish"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText1();
		}
	}

	void SetCountText()
	{
		countText.text = "Terkumpul: " + count.ToString() + " / 15";
		if (count >= 15)
		{
			finishBox.gameObject.SetActive(false);
			textKumpulkan.gameObject.SetActive(false);
		}
	}
	void SetCountText1()
	{

		if (count >= 1)
		{
			winText.text = "Anda Menang";
		}
	}
}
