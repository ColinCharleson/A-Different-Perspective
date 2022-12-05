using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
	public static DayManager instance;

	public GameObject zone1, zone2, zone3, zone4, grass;

	public int dayNumber = 0;
	public int destroyRange = 100;
	public void Awake()
	{
		if (!instance)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update()
	{
		GameObject[] pickUps = GameObject.FindGameObjectsWithTag("Grabbable");

		foreach (GameObject obj in pickUps)
		{
			if (Vector3.Distance(Vector3.zero, obj.transform.position) > destroyRange)
			{
				Destroy(obj.gameObject);
			}
		}
		switch (dayNumber)
		{
			case 0:
				destroyRange = 48;
				grass.transform.localScale = new Vector3(9.7f, 9.7f, 9.7f);
				break;
			case 1:
				break;
			case 2:
				destroyRange = 38;
				grass.transform.localScale = new Vector3(7.2f, 7.2f, 7.2f);
				zone1.SetActive(false);
				break;
			case 3:
				destroyRange = 25;
				grass.transform.localScale = new Vector3(4.9f, 4.9f, 4.9f);
				zone2.SetActive(false);
				break;
			case 4:
				destroyRange = 12;
				grass.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
				zone3.SetActive(false);
				break;
			case 5:
				destroyRange = 0;
				grass.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
				zone4.SetActive(false);
				break;
		}
	}
}
