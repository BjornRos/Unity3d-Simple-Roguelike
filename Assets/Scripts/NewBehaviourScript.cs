using UnityEngine;
using System.Collections;

/// <summary>
/// Not in use. Stress test.
/// </summary>

public class NewBehaviourScript : MonoBehaviour {

	void Start () {
	
		for (int i = 0; i<10000;i++) {
			GameObject cube  = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = new Vector3(Random.Range (-100,+100),Random.Range (-100,+100),Random.Range (-100,+100));

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
