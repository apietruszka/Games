using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Facet.Combinatorics;

public class Player : MonoBehaviour {
    public Color color;
    public List<Kropka> mojeKropy = new List<Kropka>();
    public double points = 0;

    public int[][] combs;
    public List<int[]> combsList = new List<int[]>();
    float antyAliasing = 0.01f;
	
    public Player(Color col)
    {
        color = col;
    }

    public void checkForPoints()
    {
        foreach(Kropka k in mojeKropy)
        {
            //look for squares

            //BRAINSTORM:
            //1) use raycasts for 0,45,90,135,... take the furthest away found, add to some array...(THESE are not the only angles possible, so nah)

            //RaycastHit hit;
            //Ray checkRay = new Ray

            //2) math:
            //form all possible fours:(possible problem - factorial strength)
            //check distances in every four, if they are equal, make this a square
            List<int[]> kwadrtyID = new List<int[]>();
            List<int> kropkaIDs = new List<int>();
            for (int i = 0; i < mojeKropy.ToArray().Length; i++)
                kropkaIDs.Add(i);
        }
    }

    public static List<int[]> combinations(int n,  List<Kropka> mojeKropki) // n - size of returned arrays
    {
        //1) policz ile będzie kombinacji
        //2) utwórz słownik kropki - ideksy
        if(mojeKropki.Count<n)
        {
            Debug.Log("masz za mało kropek, aby kwadrat mógł istnieć");
            return null;
        }
        int[] indicies = new int[mojeKropki.Count];//Dictionary
        for (int i = 0; i < mojeKropki.Count; i++)
            indicies[i] = i;

        List<int[]> results = new List<int[]>();
        List<int> baseNums = new List<int>();
        List<int> bonusNums = new List<int>();
        foreach (int i in indicies)
        {
            if (i < n)
                baseNums.Add(i);
            else
                bonusNums.Add(i);
        }
        results.Add(baseNums.ToArray());

        for(int i=n;i>0;i--)
        {
            for(int j=0;j<bonusNums.Count;j++)
            {
                //replace the i-th num of baseNums with the j-th num of bonusNums and add the result
                int[] toRet = baseNums.ToArray();
                toRet[i] = baseNums.ToArray()[j];
                //TODO: cache them instead of creating them every iteration

                results.Add(toRet);
            }
        }


        //TODO: transform int indicies to Kropka objects
        

        return results;//TODO: remove doubles
    }

    public void showCombs()
    {
        for(int i = 0; i<combsList.Count;i++)
        {
            for(int j=0; j<4;j++)
            {
                Debug.Log("i: " + i + ", j: " + j + "; " + combsList.ToArray()[i][j]);
            }
        }
    }

    public void findMyCombos(int elemnents)//4 in our case
    {
        points = 0;
        var watch = System.Diagnostics.Stopwatch.StartNew();
        if (mojeKropy.Count > 3)
        {
            Kropka[] inputSet = mojeKropy.ToArray();

            Combinations<Kropka> combination = new Combinations<Kropka>(inputSet, elemnents);
            string cformat = "Combinations of {{A B C D}} elementów 4: size = {0}";
            Debug.Log(String.Format(cformat, combination.Count));
            foreach (IList<Kropka> k in combination)
            {
                //Debug.Log(/*String.Format("{{{0} {1} }}",*/ k[0].name + " " + k[1].name + " " + k[2].name + " " + k[3].name);

                //load all 6 distances. We don't care about vectors, just values here, so 6 instead of 24.
                double d1 = Vector3.Distance(k[0].transform.position, k[1].transform.position);
                double d2 = Vector3.Distance(k[1].transform.position, k[2].transform.position);
                double d3 = Vector3.Distance(k[2].transform.position, k[3].transform.position);
                double d4 = Vector3.Distance(k[3].transform.position, k[0].transform.position);
                double d5 = Vector3.Distance(k[0].transform.position, k[2].transform.position);
                double d6 = Vector3.Distance(k[1].transform.position, k[3].transform.position);
                //Debug.Log("distances: " + d1 + " " + d2 + " " + d3 + " " + d4 + " " + d5 + " " + d6);
                

                //I'll do it like this(can be 6/5/5*6(idk) times faster if i do it smarter):
                //make all 6 element variations of 6 element distances array(120 with no repetitions)
                //if (first 4 elements are equal && last 2 elements are equal) then this is a sqaure and stop the loop
                //if none such arrays are found, return UNLUCKY

                List<double> distances = new List<double>();
                distances.Add(d1);
                distances.Add(d2);
                distances.Add(d3);
                distances.Add(d4);
                distances.Add(d5);
                distances.Add(d6);

                Variations<double> potentialSquares = new Variations<double>(distances.ToArray(), 6);
                
                foreach (IList<double> d in potentialSquares)
                {
                    if(d[0]==d[1] && d[1]==d[2] && d[2]==d[3] && d[4]==d[5])
                    {
                        Debug.Log("i found a square with side = " + d[0]);
                        points += (d[0] * d[0]);
                        break;
                    }
                }
            }
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log("done. Execution time: " + elapsedMs+" ms");
    }

    /*bool isASqaure(double d1, double d2, double d3, double d4, double d5, double d6)
    {
        // Three things can happen:
        //1) first value turns out to be the side of square
        //2) first value turns out to be the diagonal of square
        //3) it turns out not to be a squre
        //we will assume that it is until we prove that it's not.

        if (d1 == d2 && d2 == d3 && d3 == d4 && d5 == d6)
            return true;

        return false;
    }

    bool isClose(double val1, double val2, double closeness)
    {
        if (Math.Abs(val1 - val2) < closeness)
            return true;
        else return false;
    }*/
}
