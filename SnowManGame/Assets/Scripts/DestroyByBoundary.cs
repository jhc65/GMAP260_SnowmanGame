using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider col) {

		// Hide object (don't want to destroy bullets!)
		col.gameObject.SetActive(false);
	}
}
