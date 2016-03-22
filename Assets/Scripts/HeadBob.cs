using UnityEngine;
using System.Collections;
namespace simplerouge {

	/// <summary>
	/// This class is attached to the camera(?) and makes it move slightly randomly each step so the perspective changes slightly for greater immersion.
	/// </summary>
public class HeadBob : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Player player = transform.parent.parent.GetComponent<Player>();
			Random.seed=player.x+player.y;
		transform.position = transform.parent.position + new Vector3(0f,0.3f,0.21f) + new Vector3(Random.Range(-0.06f,0.06f),Random.Range(-0.05f,0.05f),Random.Range(-0.06f,0.06f));
	}
}
}