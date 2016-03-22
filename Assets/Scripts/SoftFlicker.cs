using UnityEngine;
/// <summary>
/// Makes the lightsource representing the players illumination act as a flickering torch by changing the intensity randomly. 
/// </summary>
[RequireComponent(typeof(Light))] public class SoftFlicker : MonoBehaviour { public float minIntensity = 0.25f; public float maxIntensity = 0.5f;
	
	float random;
	
	void Start()
	{
		random = Random.Range(0.0f, 65535.0f);
	}
	
	void Update()
	{
		float noise = Mathf.PerlinNoise(random, Time.time);
		light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
		light.transform.position= transform.parent.position + new Vector3(0f,0.7f,0.0f) + new Vector3(Random.Range(-0.0f,0f),Random.Range(-0.25f,0.25f),Random.Range(-0.0f,0.0f));
	}
}