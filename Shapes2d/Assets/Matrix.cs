using UnityEngine;
using System.Collections;

public class Matrix : MonoBehaviour {

    public int rows = 2;
    public int columns = 2;
    public float[,] value;
    public float[,] valuesToPut;

    public float v11, v12, v21, v22;
    public bool applyNewValues = false;

    public int newRows;
    public int newColumns;
    public bool applyNewDimensions;
    public float[,] inverseValue;
    
    public float det;

    public bool calculateInverseMatrix = false;

    public bool transformSpaceWithThisMatrix = false;
    public bool setThisAsNewBaseSpace = false;

	
	public void Start ()
    {
        value = new float[rows, columns];
        valuesToPut = new float[rows, columns];
        showInsides();
	}
	
	
	public void Update ()
    {
        findDet();

        if(calculateInverseMatrix)
        {
            calculateInverseMatrix = false;
            findInverseMatrix();
        }

        if(setThisAsNewBaseSpace)
        {
            setThisAsNewBaseSpace = false;
            makeThisNewSpaceBase();
        }

        if(transformSpaceWithThisMatrix)
        {
            transformSpaceWithThisMatrix = false;
            transformSpace();
        }

        if (applyNewValues)
        {
            putValuesIn();
            applyNewValues = false;
        }

        if (applyNewDimensions)
        {
            putNewDimensionsIn();
            showInsides();
            applyNewDimensions = false;
        }

        if(rows==2&&columns==2)
        {
            valuesToPut[0, 0] = v11;
            valuesToPut[1, 0] = v21;
            valuesToPut[0, 1] = v12;
            valuesToPut[1, 1] = v22;
        }
	}

    public void putValuesIn()
    {
        value = valuesToPut;
    }
    public void putNewDimensionsIn()
    {
        int oldRows = rows;
        int oldColumns = columns;
        float[,] valuesToCopy = value;

        rows = newRows;
        columns = newColumns;

        value = new float[rows, columns];
        valuesToPut = new float[rows, columns];

        int howManyRowsToCopy = rows;
        if (oldRows < rows)
            howManyRowsToCopy = oldRows;
        int howManyColumnsToCopy = columns;
        if (oldColumns < columns)
            howManyColumnsToCopy = oldColumns;
        for (int i=0;i<howManyRowsToCopy;i++)
        {
            for(int j=0;j<howManyColumnsToCopy;j++)
            {
                value[i, j] = valuesToCopy[i, j];
            }
        }
    }

    public void showInsides()
    {
        for(int i=0;i<rows;i++)
        {
            for(int j=0;j<columns;j++)
            {
                Debug.Log("row: " + i + " column: " + j + " " + value[i, j]);
            }
        }
    }
    public void findDet()
    {
        if(rows==2&&columns==2)
        {
            det = value[0, 0] * value[1, 1] - value[1, 0] * value[0, 1];
        }
    }

    public void transformSpace()
    {
        Manager m = GameObject.FindObjectOfType<Manager>();
        m.transI.x = value[0, 0];
        m.transI.y = value[1, 0];
        m.transJ.x = value[0, 1];
        m.transJ.y = value[1, 1];
        m.transformation();
    }
    public void makeThisNewSpaceBase()
    {
        Manager m = GameObject.FindObjectOfType<Manager>();
        m.transI.x = value[0, 0];
        m.transI.y = value[1, 0];
        m.transJ.x = value[0, 1];
        m.transJ.y = value[1, 1];
        m.ApplyChangeBase();
    }
    public void findInverseMatrix()
    {
        findDet();
        inverseValue = new float[rows, columns];//dimensional copy
        float scalar = 1f / det;
        if(rows==2&&columns==2)
        {
            value[0, 0] = scalar * value[1, 1];//Ix
            value[1, 0] = -scalar * value[1, 0];//Iy
            value[0, 1] = -scalar * value[0, 1];//Jx
            value[1, 1] = scalar * value[0, 0];//Jy
        }
        else
        {
            Debug.Log("i can only calculate inverse of 2x2 matrix so far :(");
        }
        //Manager m = GameObject.FindObjectOfType<Manager>();
        //m.transI.x=(inverseValue[0, 0]);
        //m.transI.y = (inverseValue[1, 0]);
        //m.transJ.x = (inverseValue[0, 1]);
        //m.transJ.y = (inverseValue[1, 1]);
        //m.transformation();
    }


    



}
