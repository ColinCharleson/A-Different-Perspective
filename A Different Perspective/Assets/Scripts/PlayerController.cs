using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	private float playerSpeed = 0.1f;
	private float jumpHeight = 1.0f;
	private float gravityValue = -9.81f;

	public float mouseSensitivity = 500;
	float xRotation;
	Camera cam;

	public GameObject grabArea;

	private void Start()
	{
		cam = GetComponentInChildren<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Movement();
		Rotation();
		PickingUp();
	}

	void Movement()
	{
		if (Input.GetKey(KeyCode.W))
			transform.position += (transform.forward * playerSpeed);
		if (Input.GetKey(KeyCode.S))
			transform.position += (transform.forward * -playerSpeed/2);
		if (Input.GetKey(KeyCode.A))
			transform.position += (transform.right * -playerSpeed/1.5f);
		if (Input.GetKey(KeyCode.D))
			transform.position += (transform.right * playerSpeed / 1.5f);

		// Changes the height position of the player..
		if (Input.GetButtonDown("Jump") && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}
	}
	void Rotation()
	{
		// Get Inputs
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -30f, 30f);

		// Set Rotation
		cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
		this.transform.Rotate(Vector3.up * mouseX);
	}
	void PickingUp()
	{
		GameObject[] pickUps = GameObject.FindGameObjectsWithTag("Grabbable");

		if (Input.GetKey(KeyCode.E))
		{
			foreach (GameObject obj in pickUps)
			{
				if (Vector3.Distance(grabArea.transform.position, obj.transform.position) < 1)
				{
					Inventory.add.twig += obj.transform.GetComponent<Grabbable>().twig;
					Inventory.add.leaf += obj.transform.GetComponent<Grabbable>().leaf;
					Inventory.add.mud += obj.transform.GetComponent<Grabbable>().mud;
					Inventory.add.berry += obj.transform.GetComponent<Grabbable>().berry;
					Destroy(obj.transform.gameObject);
				}
			}
		}
	}
}
