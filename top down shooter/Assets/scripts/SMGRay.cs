﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGRay : MonoBehaviour
{
	public float speed = 20;
	public float damage = 25;
	public float timeout = 0.2f;
	public int targets = 3;
	private float curTimeout;
	public Transform gunPoint;
	public Transform bullet;
	public Rigidbody bul;
	public int number=5;//после какой пули увеличивается разброс
	public float degree = 1;//на сколько градусов изменяется разброс
	public int max = 10;//максимальое значение
	int chetminus = 5;
	int chetplus = 5;
	float razbros = 0;
	int holder;
	public int oboima = 30;
	public float ReloadTime = 2f;
	float rt = 0;
	bool ReloadActive = false;
	// Start is called before the first frame update
	void Start()
	{
		chetminus = chetplus = number;
		rt = ReloadTime;
		holder = oboima;
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButton(0) && ReloadActive==false)
		{
			if (holder != 0)
			{
				curTimeout += Time.deltaTime;
				if (curTimeout > timeout)
				{
					holder--;
					Transform info;
					Vector3 rot = transform.parent.gameObject.transform.rotation.eulerAngles;
					float rand = Random.Range(-razbros, razbros);
					rot.y += rand;
					info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));
					info.GetComponent<RayBullet>().damage = damage;
					info.GetComponent<RayBullet>().speed = speed;
					info.GetComponent<RayBullet>().bullet = bul;
					info.GetComponent<RayBullet>().targets = targets;
					info.GetComponent<RayBullet>().ready = true;
					Debug.Log("Префаб создан");
					curTimeout = 0;
					chetplus--;
					chetminus = number;
				}
			}
			else
            {
				ReloadActive = true;
            }

		}
		else
		{
			curTimeout = timeout + 1;
			chetminus--;
			chetplus = number;
		}
		if (chetplus == 0)
        {
			chetplus = number;
			if (razbros < max)
			{
				razbros += degree;
			}
        }
        if (chetminus == 0)
        {
			chetminus = number;
            if (razbros > 0)
            {
				razbros -= degree;
            }
        }
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (holder != oboima)
			{
				holder = 0;
				ReloadActive = true;

			}
		}
		if (ReloadActive)
		{
			Reload();
		}
	}
	void Reload()
	{
		if (rt > 0)
		{
			rt -= Time.deltaTime;
			Debug.Log("Перезарядка:" + rt);
		}
		else
		{
			razbros = 0;
			ReloadActive = false;
			rt = ReloadTime;
			holder = oboima;
			Debug.Log("Перезарядка закончилась");
		}
	}
	void OnDisable()
	{
		rt = ReloadTime;
	}
}

