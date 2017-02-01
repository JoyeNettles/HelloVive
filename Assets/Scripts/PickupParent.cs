using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	public Transform sphere;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate () {
		device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You are holding 'Touch' on the Trigger");
		}

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You activated TouchDown on the Trigger");
		}

		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You activated TouchUp on the Trigger");
		}

		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You are holding 'Press' on the Trigger");
		}

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You activated PressDown on the Trigger");
		}

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			Debug.Log("You activated PressUp on the Trigger");
		}

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			Debug.Log("You activated PressUp on the Touchpad");
			sphere.transform.position = Vector3.zero;
			sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
			sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}

	void OnTriggerStay (Collider col)
	{
		Debug.Log ("You have collided with " + col.name + " and activated OnTriggerStay");
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have collided with " + col.name + " while holding down touch.");
			col.attachedRigidbody.isKinematic = true; //makes it so the sphere is not being affected by the physics system
			col.gameObject.transform.SetParent (this.gameObject.transform); // attaches the sphere to the controller
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have released touch while colliding with " + col.name);
			col.attachedRigidbody.isKinematic = false;
			col.gameObject.transform.SetParent (null); // removes the controller as parent
		}
	}
}			