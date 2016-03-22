using UnityEngine;
using System.Collections;
namespace simplerouge{
	/// <summary>
	/// Main gameloop. Builds the mapmesh and starts the music. 
	/// </summary>
public class GameManager : MonoBehaviour {

    public Maze Maze;

	// Use this for initialization
	void Start () {
        Maze.BuildMesh();
		

			PlayMusic();

	}

		void PlayMusic() {
			AudioSource aus = gameObject.GetComponent<AudioSource>();

			aus.loop=true;

			aus.Play ();


		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}