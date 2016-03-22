using UnityEngine;
using System.Collections;
/// <summary>
/// Camera light.
/// </summary>
public class CameraLight : MonoBehaviour {

	public Light limelight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPreCull () {
		if (limelight != null)
			limelight.enabled = true;
	}
	void OnPreRender() {
		if (limelight != null)
			limelight.enabled = true;
	}
	
	void OnPostRender() {
		if (limelight != null)
			limelight.enabled = false;
	}


}
