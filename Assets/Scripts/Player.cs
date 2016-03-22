using UnityEngine;
using System.Collections;
namespace simplerouge {

	/// <summary>
	/// Player input is handled here, also keeps track of the facing.
	/// </summary>
	public class Player : MonoBehaviour {

	public int x;
	public int y;
	public int faceing; // 0= north, 1 east, 2 south, 3 west
	public Maze maze;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
			if (Input.GetKeyDown(KeyCode.Q)) {
				faceing--; if (faceing==-1) faceing = 3;
			}

			if (Input.GetKeyDown(KeyCode.E)) {
				faceing++; if (faceing==4) faceing = 0;
			}


		if (Input.GetKeyDown(KeyCode.W)) {
			switch (faceing) {
				case 0 :{ if (maze.Level[x,y+1]==0) y++; break;}
				case 1 :{ if (maze.Level[x+1,y]==0)x++; break;}
				case 2 :{ if (maze.Level[x,y-1]==0)y--; break;}
				case 3 :{ if (maze.Level[x-1,y]==0)x--; break;}
				default: break;
				}

				AudioSource aus = transform.Find ("PlayerModel/SoundFootsteps").gameObject.GetComponent<AudioSource>();
				if (!aus.isPlaying)	aus.Play();
			}
			if (Input.GetKeyDown(KeyCode.S)) {

				switch (faceing) {
					case 0 :{ if (maze.Level[x,y-1]==0)y--; break;}
					case 1 :{ if (maze.Level[x-1,y]==0)x--; break;}
					case 2 :{ if (maze.Level[x,y+1]==0)y++; break;}
					case 3 :{ if (maze.Level[x+1,y]==0)x++; break;}
				default: break;
				}
			}
			if (Input.GetKeyDown(KeyCode.D)) {
				int tmp = faceing -1;
				if (tmp==-1) tmp=3;
				switch (tmp) {
				case 0 :{ if (maze.Level[x,y-1]==0)y--; break;}
				case 1 :{ if (maze.Level[x-1,y]==0)x--; break;}
				case 2 :{ if (maze.Level[x,y+1]==0)y++; break;}
				case 3 :{ if (maze.Level[x+1,y]==0)x++; break;}
				default: break;
				}
			}
			if (Input.GetKeyDown(KeyCode.A)) {
				int tmp = faceing +1;
				if (tmp==4) tmp=0;
				switch (tmp) {
				case 0 :{ if (maze.Level[x,y-1]==0)y--; break;}
				case 1 :{ if (maze.Level[x-1,y]==0)x--; break;}
				case 2 :{ if (maze.Level[x,y+1]==0)y++; break;}
				case 3 :{ if (maze.Level[x+1,y]==0)x++; break;}
				default: break;
				}

			}
			if (Input.GetKeyDown (KeyCode.Escape)) {Application.Quit();}

				this.transform.position = new Vector3(0.5f+x,0.5f,0.5f+y);
				//this.transform.Rotate(new Vector3(0,90*faceing,0));
				this.transform.rotation = Quaternion.identity;
				this.transform.eulerAngles = new Vector3(0,90*faceing,0);


		}
	}
}
