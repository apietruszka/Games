using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    
    public  Vector2 I = new Vector2(1f, 0f);
    public  Vector2 J = new Vector2(0f, 1f);
    public bool applyTransformation = false;
    public bool changeBase = false;
    public float det;

    public float spaceBendLow = -10f;
    public float spaceBendHigh = 10f;
    public float detToFitSpaceTo=0f;
    public bool transformDet = false;
    

    Vector2 baseI;
    Vector2 baseJ;
    public Vector2 transI = new Vector2(1f, 0f);
    public Vector2 transJ = new Vector2(0f, 1f);
    public Vector[] vectors;
    public Line[] lines;

    public bool changeBaseIWithMouse = false;
    public bool changeBaseJWithMouse = false;
    public float mousePower = 1f;



    private void Start()
    {
        lines = GameObject.FindObjectsOfType<Line>();
        vectors = GameObject.FindObjectsOfType<Vector>();
        baseI = I;
        baseJ = J;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            changeBaseIWithMouse = !changeBaseIWithMouse;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            changeBaseJWithMouse = !changeBaseJWithMouse;
        }
        if (changeBaseIWithMouse)
        {
            I.x = (Input.mousePosition.x / Screen.width * 2f - 1f) * mousePower;
            I.y = (Input.mousePosition.y / Screen.height * 2f - 1f) * mousePower;
        }
        if (changeBaseJWithMouse)
        {
            J.x = (Input.mousePosition.x / Screen.width * 2f - 1f) * mousePower;
            J.y = (Input.mousePosition.y / Screen.height *2f - 1f) * mousePower;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            I.x = 1f;
            I.y = 0f;
            J.x = 0f;
            J.y = 1f;
        }


        foreach (Line l in lines)
        {

            if(l.I)
            {
                l.lineA = J.y / J.x;
                if (I.x != 0)
                {
                    l.desiredPosition = l.normalDist * I;
                    l.desiredRotation = I;
                }
                else
                {
                    Debug.Log("I.x=0");
                    I.x = 0.00001f;
                }
            }
            else
            {
                l.lineA = I.y / I.x;
                if (J.x != 0)
                {
                    l.desiredPosition = l.normalDist * J;
                    l.desiredRotation = J;
                }
                else
                {
                    Debug.Log("J.x=0");
                    J.x = 0.00001f;
                }
            }
        }
        foreach(Vector v in vectors)
        {
            v.desiredPosition = v.v.x * I + v.v.y * J;
        }

        findDet();

        if(changeBase)
        {
            ApplyChangeBase();
            changeBase = false;
        }

        if(applyTransformation)
        {
            transformation();
            applyTransformation = false;
        }
        if(transformDet)
        {
            randomizeToFitDet();
            transformDet = false;
        }
    }

    public void ApplyChangeBase()
    {
        I = transI;
        J = transJ;
    } 

    public void transformation()
    {
        float newIX = I.x * transI.x + J.x * transI.y;
        float newIY = I.y * transI.x + J.y * transI.y;
        float newJX = I.x * transJ.x + J.x * transJ.y;
        float newJY = I.y * transJ.x + J.y * transJ.y;

        I.x = newIX;
        I.y = newIY;
        J.x = newJX;
        J.y = newJY;
    }

    public void findDet()
    {
        det = I.x * J.y - I.y * J.x;
    }

    public void randomizeToFitDet()
    {
        //randomize the structure of space(angles of base vectors)
        float IRotDegrees = Random.Range(0, 360f);
        float JRotDegrees = Random.Range(0, 360f);
        Debug.Log("i angle: "+IRotDegrees);
        Debug.Log("j angle: "+JRotDegrees);

        // 1) randomize Ix
        transI.x = Random.Range(spaceBendLow, spaceBendHigh);//to chyba glupie, itak trzeba normalizowac bedzie; poki co ok

        // 2) find Iy. It is connected by the angle we randomized before
        transI.y = transI.x * Mathf.Tan(IRotDegrees / 180f * Mathf.PI);

        // 3) find Jx, Jy given det equation and Angle corelation for J
        float algebraicAForY = 0;
        algebraicAForY = Mathf.Tan(JRotDegrees / 180f * Mathf.PI);
        transJ.x = det / (transI.x * algebraicAForY - transI.y);
        transJ.y = transJ.x * algebraicAForY;

        // 4) uwzgledniamy det

        transI *= detToFitSpaceTo;
        transJ *= detToFitSpaceTo;

        //co powoduje bardziej square-ish? TO ESTABLISH!
        transformation();
    }
}
