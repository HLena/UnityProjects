using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScenary : MonoBehaviour
{
    // public Transform grassblock;
    private GameObject currentBlockType;
    public GameObject[] blockTypes;
    public GameObject[] treeTypes;

    [Tooltip("true")]
    public bool SnapToGrid = true;

	public float amp = 10f;
	public float freq = 10f;
    public int consola;

	public int seed = 0;
	bool seeded = false;
	private GameObject mySeeder;

	private Vector3 myPos;

	int currentX = 0;
	int currentZ = 0;
	bool finishedTerrain;
	public bool useRunningTerrain = false;

    void Start()
    {
        myPos = this.transform.position;
        generateTerrain();
        
    }
   
    //Funcion que genera el terreno
    void generateTerrain(){
    	int cols = 100;
    	int rows = 100;
    	for( float x = 0; x < cols; x++)
    	{
    		for(int z = 0; z < rows; z++)
    		{
                //seed = getSeed();
    			float y = Mathf.PerlinNoise((seed + myPos.x + x) /freq,(myPos.z + z)/freq)*amp;
    			if (SnapToGrid)
    			{ 
    				y = Mathf.Floor(y);
    			}
    			
    			if(y > amp /2){
    				currentBlockType = blockTypes[0];
				}
    			else {

					consola = Random.Range(1, blockTypes.Length);
                    print(consola);
    				currentBlockType = blockTypes[consola];
				}
                GameObject newBlock = GameObject.Instantiate(currentBlockType);
                newBlock.transform.position = new Vector3(myPos.x + x, y, myPos.z + z);                              

    			//Para decidir si crear un arbol o no
    			if(Random.value * 100 < 0.1){
    				float adjust = newBlock.transform.lossyScale.y / 2f;
					GameObject treeBabe = GameObject.CreatePrimitive (PrimitiveType.Cube);

					Vector3 tT = treeBabe.transform.localScale;
					tT.y = Random.value * 24; 
                    treeBabe.transform.localScale =tT;

					adjust += treeBabe.transform.localScale.y / 2f;

					//treeBabe.transform.position = new Vector3 (myPos.x + x, y + 1f + adjust, myPos.z + z);
                    treeTypes[0].transform.position = new Vector3(myPos.x + x, y + 1f + adjust, myPos.z + z);
                }
    		    newBlock.transform.SetParent(this.transform);
    		}
    	}

    }

    float seedFeatures(int _x, int _z){
    	return Mathf.Sin(_x + _z);
    }

   /*int getSeed(){		
		return mySeeder.GetComponent<seedManager> ().getSeed ();
	}*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
