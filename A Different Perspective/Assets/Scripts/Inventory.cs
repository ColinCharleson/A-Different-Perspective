using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory add;

    public int twig;
    public int leaf;
    public int mud;
    public int berry;

	public GameObject shelter;
	public void Awake()
	{
		if (!add)
		{
			add = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(twig >= 4 && leaf >= 10 && mud >= 40)
			{
				Instantiate(shelter, this.transform.position + ((Vector3.forward * 2) - Vector3.up), Quaternion.identity);
			}
		}
	}
}
