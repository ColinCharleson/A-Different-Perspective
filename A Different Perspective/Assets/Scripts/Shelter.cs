using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{
	public bool goodLocation;
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Grass"))
		{
			goodLocation = true;
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.CompareTag("Grass"))
		{
			goodLocation = false;
		}
	}
}
