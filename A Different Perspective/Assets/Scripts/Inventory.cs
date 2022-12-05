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
}
