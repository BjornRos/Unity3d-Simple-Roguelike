using UnityEngine;
using System.Collections;

namespace simplerouge {
	/// <summary>
	///  This file contains a collection of classes that will be used in taking action and paying "time-points" for them so that everyone gets a fair amount of actions relative each other.
	/// </summary>
	public class Action {

		public int PrepTime;
		public int RecoverTime;
		public int CooldownTime;


		public Action (int prep, int recover, int cooldown) {

		}

	}

	public class ActionType {
		public static string Name = "Default Action";

		public static int PrepTime=1;
		public static int RecoverTime=1;
		public static int CooldownTime=1;

		public static Action CreateInstance(Actor actor) {
			Action action = new Action(PrepTime*10/actor.Speed,RecoverTime*10/actor.Speed,CooldownTime*10/actor.Speed);
			return action;
		}
	}

	public class Actor {
				public string Name = "Mr Smith";
				public int Speed = 10;

			}


	public class GameTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}