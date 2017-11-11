using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	public bool isEnabled;
	void OnTriggerExit(Collider col) {

		// Hide object (don't want to destroy bullets!)
		if (isEnabled)
			col.gameObject.SetActive(false);
	}
}
