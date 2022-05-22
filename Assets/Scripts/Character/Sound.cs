using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
	[SerializeField] private AudioClip[] SonidoTerreno;
	[SerializeField] private AudioSource fuenteSonido;
	private float timer;
	[SerializeField] private float timerPasos;

	Vector3 oldPos;

	void Update()
	{
		if (transform.position != oldPos)
		{
			PlayFootStepSound();
		}
		timer -= Time.deltaTime;
		oldPos = transform.position;
	}

	void PlayFootStepSound()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10f) && timer <= 0)
		{
			timer = timerPasos;

			if (hit.collider.CompareTag("Terrain"))
			{
				fuenteSonido.clip = SonidoTerreno[Random.Range(0, SonidoTerreno.Length)];
				fuenteSonido.Play();
			}
		}
	}
}
