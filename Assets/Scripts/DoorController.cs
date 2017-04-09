using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public int keys;

	bool IsDoorOpen(){
		if(keys <= 0)
			return true;
		return false;
	}

	void Update(){
		if(IsDoorOpen()){
			Destroy(this.gameObject);
		}

	}

}
