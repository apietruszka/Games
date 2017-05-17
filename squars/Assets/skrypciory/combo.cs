using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facet.Combinatorics;
using System;


public class combo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
        {
            int[] inputSet = { 1, 2, 3, 4 };

            Combinations<int> combinations = new Combinations<int>(inputSet, 2);
            string cformat = "Combinations of {{A B C D}} choose 2: size = {0}";
            Debug.Log(String.Format(cformat, combinations.Count));
            foreach (IList<int> i in combinations)
            {
                Debug.Log(/*String.Format("{{{0} {1} }}",*/ i[0]+" "+ i[1]);
            }

            /*Variations<char> variations = new Variations<char>(inputSet, 2);
            string vformat = "Variations of {{A B C D}} choose 2: size = {0}";
            Debug.Log(String.Format(vformat, variations.Count));
            foreach (IList<char> v in variations)
            {
                Debug.Log(String.Format("{{{0} {1}}}", v[0], v[1]));
            }*/
        }
	}
}
