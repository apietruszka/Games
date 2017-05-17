using UnityEngine;
using System.Collections;

public class hexTilesControl : MonoBehaviour {

    hexTile[] hex;
    public int howManyHexes=959;
    int activated = 1000;
    public bool paintMode = false;
    public int mode = 1;

    private void Start()
    {
        hex=GetComponentsInChildren<hexTile>();
    }

    void findNearbyHexTiles(float radius)
    {
        if (mode == 1)//cursor closest
        {
            if (activated != 1000)
            {
                for (int i = 0; i < howManyHexes; i++)
                {
                    if ((hex[activated].transform.position - hex[i].transform.position).sqrMagnitude < radius)
                    {
                        hex[i].goUp();
                    }
                    else if (!paintMode)
                    {
                        hex[i].goDown();
                    }
                }
            }
            else if (!paintMode)
            {
                for (int i = 0; i < howManyHexes; i++)
                {
                    hex[i].goDown();
                }
            }
        }
        else if(mode==2)//wave X
        {
            if (activated != 1000)
            {
                for (int i = 0; i < howManyHexes; i++)
                {

                    if ((hex[activated].transform.position.x - hex[i].transform.position.x) < 2f&& (hex[activated].transform.position.x - hex[i].transform.position.x)>-2f)
                    {
                        hex[i].goUp();
                    }
                    else if (!paintMode)
                    {
                        hex[i].goDown();
                    }
                }
            }
            else if (!paintMode)
            {
                for (int i = 0; i < howManyHexes; i++)
                {
                    hex[i].goDown();
                }
            }
        }

        else if (mode == 3)//wave Z
        {
            if (activated != 1000)
            {
                for (int i = 0; i < howManyHexes; i++)
                {

                    if ((hex[activated].transform.position.z - hex[i].transform.position.z) < 2f && (hex[activated].transform.position.z - hex[i].transform.position.z) > -2f)
                    {
                        hex[i].goUp();
                    }
                    else if (!paintMode)
                    {
                        hex[i].goDown();
                    }
                }
            }
            else if (!paintMode)
            {
                for (int i = 0; i < howManyHexes; i++)
                {
                    hex[i].goDown();
                }
            }
        }
    }

    int findActiveHexTile()
    {
            for (int i = 0; i < howManyHexes; i++)
            {
                if (hex[i].active == true)
                {
                    return i;
                }
            }
            return 1000;
        
    }

    void Update ()
    {
        activated=findActiveHexTile();
        findNearbyHexTiles(10f);
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paintMode)
                paintMode = false;
            else
                paintMode = true;
        }
	}
}
