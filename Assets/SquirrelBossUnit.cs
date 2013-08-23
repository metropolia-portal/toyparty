using UnityEngine;
using System.Collections;

public class SquirrelBossUnit : Unit {

	public AudioSource damageAudioSource;
	
	protected override void OnDamage() {
		damageAudioSource.clip = gameManager.soundManager.OwlBossDamage;
		damageAudioSource.Play();
	}

	protected override IEnumerator MainScript() {
		SetMovementSpeed(2);
		// ENTRANCE
		yield return MoveTo(0.7f,0.5f);
		// PHASE 1
		SetMovementSpeed(3);
		while (!interrupt) {
			yield return MoveTo (0.7f, Random.Range(0.2f,0.8f));

			if (!interrupt) yield return Idle(1,"attack");

			if (!interrupt) Shoot (bullet);		

			if (!interrupt) yield return Idle(2);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
