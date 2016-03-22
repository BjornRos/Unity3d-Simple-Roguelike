using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace simplerouge
{


	public class Maze : MonoBehaviour
    {

		public Material floorMaterial;
		public Material wallMaterial;
		public Material solidRoofMaterial;

		public float WallRoughness = 0.1f;

        private const int size = 16;
        private List<Vector3> verticeslist;
        private List<Vector2> UVlist;
        private List<int> trianglelist;
		private List<int> wallTrianglelist;
		private List<int> solidRoof;
       // int[,] tmplvl;
        
        

        public int[,] Level = new int[size, size]{ {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                    {1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1},
                                    {1,1,1,0,1,1,1,1,1,1,0,1,0,0,0,1},
                                    {1,0,0,0,1,1,1,1,1,1,0,1,0,0,0,1},
                                    {1,1,1,0,1,1,1,0,0,0,0,0,0,0,0,1},
                                    {1,1,1,0,1,1,1,0,1,1,1,1,0,0,0,1},
                                    {1,1,1,0,1,1,1,0,1,1,1,1,0,0,0,1},
                                    {1,1,1,0,1,1,0,0,0,1,1,1,1,0,1,1},
                                    {1,1,1,0,1,1,0,0,0,0,0,0,0,0,1,1},
                                    {1,0,0,0,0,1,0,0,0,1,1,1,1,1,1,1},
                                    {1,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1},
                                    {1,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1},
                                    {1,0,1,1,1,1,1,0,1,1,0,0,0,1,1,1},
                                    {1,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1},
                                    {1,0,0,0,1,1,1,1,1,1,0,0,0,1,1,1},
                                    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};
        
        // Use this for initialization
        void Start()
        {

			//tmplvl = new int[size, size] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        }

        public void BuildMesh()
        {
            if (gameObject.GetComponent("MeshFilter") == null) gameObject.AddComponent("MeshFilter");
            if (gameObject.GetComponent("MeshRenderer") == null) gameObject.AddComponent("MeshRenderer");
            UnityEngine.Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
			mesh.subMeshCount=3;

			verticeslist = new List<Vector3>();
			UVlist = new List<Vector2>();
			trianglelist = new List<int>();
			wallTrianglelist = new List<int>();
			solidRoof = new List<int>();



            Generate(size, Level);

			MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();


            mesh.vertices = verticeslist.ToArray();
            mesh.uv = UVlist.ToArray();
			mesh.SetTriangles(trianglelist.ToArray(),0); //make floor
			mesh.SetTriangles(wallTrianglelist.ToArray(),1); //make walls
			mesh.SetTriangles(solidRoof.ToArray(),2); //make walls
			mesh.Optimize ();
			mesh.RecalculateBounds();
            mesh.RecalculateNormals();
			mesh.calculateMeshTangents();

			Material[] tempm= new Material[3];
			tempm[0] = floorMaterial;
			tempm[1] = wallMaterial;
			tempm[2] = solidRoofMaterial;
			mr.materials = tempm;
            MeshFilter mf = GetComponent<MeshFilter>();
            if (mf == null) { Debug.Log("MeshFilter is null."); } else mf.mesh = mesh;
        }

        void Generate(int size, int[,] data)
        {


            for (int x=0;x<size;x++)
                for (int y = 0; y < size; y++)
                {
                    AddCell(data, x, y);
                }
        }

        void AddCell(int[,] data, int x, int z)
        {
			int basecnt;
            //check if 0 if so
            if (data[x, z] == 0)
            {
                //add floor
				basecnt = verticeslist.Count;

				verticeslist.Add(new Vector3(x, 0, z));
                verticeslist.Add(new Vector3(x, 0, z+1));
                verticeslist.Add(new Vector3(x+1, 0, z+1));
                verticeslist.Add(new Vector3(x+1, 0, z));

                trianglelist.Add(basecnt + 0);
                trianglelist.Add(basecnt + 1);
                trianglelist.Add(basecnt + 2);

                trianglelist.Add(basecnt + 0);
                trianglelist.Add(basecnt + 2);
                trianglelist.Add(basecnt + 3);

				UVlist.Add(new Vector2(0, 0));
				UVlist.Add(new Vector2(1, 0));
				UVlist.Add(new Vector2(1, 1));
				UVlist.Add(new Vector2(0, 1));

				//add roof
				basecnt = verticeslist.Count;

				verticeslist.Add(new Vector3(x, 2, z));
				verticeslist.Add(new Vector3(x, 2, z+1));
				verticeslist.Add(new Vector3(x+1, 2, z+1));
				verticeslist.Add(new Vector3(x+1, 2, z));


				trianglelist.Add(basecnt + 0);
				trianglelist.Add(basecnt + 2);
				trianglelist.Add(basecnt + 1);

				trianglelist.Add(basecnt + 0);
				trianglelist.Add(basecnt + 3);
				trianglelist.Add(basecnt + 2);
				
				UVlist.Add(new Vector2(0, 0));
				UVlist.Add(new Vector2(1, 0));
				UVlist.Add(new Vector2(1, 1));
				UVlist.Add(new Vector2(0, 1));



				//check neighbours, add walls
				if (data[x+1, z] == 1) // east
				{
					basecnt = verticeslist.Count;

					for (int i = 0; i<11;i++)
						for (int j = 0; j<11;j++) {
						if ((i*j==0)||(i==10)||(j==10)) {
							verticeslist.Add(new Vector3(x+1, 0.2f*j, z+0.1f*i));
							} else {
							verticeslist.Add(new Vector3(x+1+Random.Range(-WallRoughness,WallRoughness), 0.2f*j, z+0.1f*i));
							}
							UVlist.Add(new Vector2(0.1f*i, 0.1f*j));
					}
					for (int yy = 0; yy<10;yy++)
						for (int xx = 0; xx<10;xx++) {
							wallTrianglelist.Add(basecnt + xx+yy*11);
							wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
							wallTrianglelist.Add(basecnt + xx+1+yy*11);

							wallTrianglelist.Add(basecnt + xx+yy*11);
							wallTrianglelist.Add(basecnt + xx+(yy+1)*11);
							wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
					}

				}
				if (data[x, z-1] == 1) // south
				{
					basecnt = verticeslist.Count;
					
					for (int i = 0; i<11;i++)
					for (int j = 0; j<11;j++) {
						if ((i*j==0)||(i==10)||(j==10)) {
							verticeslist.Add(new Vector3(x+(0.1f*i), 0.2f*j, z));
						} else {
							verticeslist.Add(new Vector3(x+(0.1f*i)+Random.Range(-WallRoughness,WallRoughness), 0.2f*j, z));
						}
						UVlist.Add(new Vector2(0.1f*i, 0.1f*j));
					}
					for (int yy = 0; yy<10;yy++)
					for (int xx = 0; xx<10;xx++) {
						wallTrianglelist.Add(basecnt + xx+yy*11);
						wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
						wallTrianglelist.Add(basecnt + xx+1+yy*11);
						
						wallTrianglelist.Add(basecnt + xx+yy*11);
						wallTrianglelist.Add(basecnt + xx+(yy+1)*11);
						wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
					}
				}				
				if (data[x-1, z] == 1) // west
				{
					basecnt = verticeslist.Count;

					for (int i = 0; i<11;i++)
						for (int j = 0; j<11;j++) {
						if ((i*j==0)||(i==10)||(j==10)) {
							verticeslist.Add(new Vector3(x, 0.2f*j, z+0.1f*i));
							} else {
							verticeslist.Add(new Vector3(x+Random.Range(-WallRoughness,WallRoughness), 0.2f*j, z+0.1f*i));
							}
							UVlist.Add(new Vector2(0.1f*i, 0.1f*j));
					}
					for (int yy = 0; yy<10;yy++)
						for (int xx = 0; xx<10;xx++) {
							wallTrianglelist.Add(basecnt + xx+yy*11);
							wallTrianglelist.Add(basecnt + xx+1+yy*11);
							wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
	
							wallTrianglelist.Add(basecnt + xx+yy*11);
							wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
							wallTrianglelist.Add(basecnt + xx+(yy+1)*11);
					}

				}
				if (data[x, z+1] == 1) // north
				{
					basecnt = verticeslist.Count;
					
					for (int i = 0; i<11;i++)
					for (int j = 0; j<11;j++) {
						if ((i*j==0)||(i==10)||(j==10)) {
							verticeslist.Add(new Vector3(x+(0.1f*i), 0.2f*j, z+1));
						} else {
							verticeslist.Add(new Vector3(x+(0.1f*i)+Random.Range(-WallRoughness,WallRoughness), 0.2f*j, z+1));
						}
						UVlist.Add(new Vector2(0.1f*i, 0.1f*j));
					}
					for (int yy = 0; yy<10;yy++)
					for (int xx = 0; xx<10;xx++) {
						wallTrianglelist.Add(basecnt + xx+yy*11);
						wallTrianglelist.Add(basecnt + xx+1+yy*11);
						wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);

						wallTrianglelist.Add(basecnt + xx+yy*11);
						wallTrianglelist.Add(basecnt + xx+1+(yy+1)*11);
						wallTrianglelist.Add(basecnt + xx+(yy+1)*11);
					}
				}

            } else { // black on empty spaces for minimap
				basecnt = verticeslist.Count;
				
				verticeslist.Add(new Vector3(x, 2, z));
				verticeslist.Add(new Vector3(x, 2, z+1));
				verticeslist.Add(new Vector3(x+1, 2, z+1));
				verticeslist.Add(new Vector3(x+1, 2, z));
				
				solidRoof.Add(basecnt + 0);
				solidRoof.Add(basecnt + 1);
				solidRoof.Add(basecnt + 2);
				
				solidRoof.Add(basecnt + 0);
				solidRoof.Add(basecnt + 2);
				solidRoof.Add(basecnt + 3);
				
				UVlist.Add(new Vector2(0, 0));
				UVlist.Add(new Vector2(1, 0));
				UVlist.Add(new Vector2(1, 1));
				UVlist.Add(new Vector2(0, 1));

			}
                
                
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}