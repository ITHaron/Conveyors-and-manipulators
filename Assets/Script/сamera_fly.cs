using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class сamera_fly : MonoBehaviour {

	[SerializeField] private float mouseSensitivity = 3.0f;
	[SerializeField] private float speed = 2.0f;
	
	private Vector3 transfer;
	private float minimumX = -360F;
	private float maximumX = 360F;
	private float minimumY = -60F;
	private float maximumY = 60F;
	private float rotationX = 0F;
	private float rotationY = 0F;
	private Quaternion originalRotation;


	void Start() {
		originalRotation = transform.rotation;
	}

	void Update() {
		rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
		rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
		rotationX = ClampAngle (rotationX, minimumX, maximumX);
		rotationY = ClampAngle (rotationY, minimumY, maximumY);
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
		transform.rotation = originalRotation * xQuaternion * yQuaternion;

		transfer = transform.forward * Input.GetAxis("Vertical");
		transfer += transform.right * Input.GetAxis("Horizontal");
		transform.position += transfer * speed * Time.deltaTime;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F) 
			angle += 360F;
		if (angle > 360F) 
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

}
