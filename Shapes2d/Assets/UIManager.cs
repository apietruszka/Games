using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text matrixDet;
    public Text spaceDet;
    
    public Text s00, s01, s02, s10, s11, s12, s20, s21, s22;



    public Canvas matrix;
    public Canvas anim;
    
    void updateSpaceTexts()
    {
        Manager m = GameObject.FindObjectOfType<Manager>();
        s00.text = m.I.x.ToString();
        s10.text = m.I.y.ToString();
        s01.text = m.J.x.ToString();
        s11.text = m.J.y.ToString();
        m.findDet();
        spaceDet.text = "det: "+m.det.ToString();
    }

    public void goMatrixMode()
    {
        anim.gameObject.SetActive(false);
        matrix.gameObject.SetActive(true);
    }
    public void goAnimationMode()
    {
        anim.gameObject.SetActive(true);
        matrix.gameObject.SetActive(false);
    }

    public void TransformSpaceButton()
    {
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.transformSpace();
        updateSpaceTexts();
    }

    public void SetInverseButton()
    {
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.findInverseMatrix();
        //TODO:  zmien tez napisy w UI Input field

        m.findDet();
        matrixDet.text = "det: " + m.det.ToString();
    }

    public void SetNewBaseButton()
    {
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.makeThisNewSpaceBase();
        updateSpaceTexts();
    }

    public void in00(string newText)
    {
        float newValue = float.Parse(newText);

        //input to matrix object
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.value[0, 0] = newValue;

        m.findDet();
        matrixDet.text = "det: "+m.det.ToString();
    }
    public void in01(string newText)
    {
        float newValue = float.Parse(newText);

        //input to matrix object
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.value[0, 1] = newValue;

        m.findDet();
        matrixDet.text = "det: "+m.det.ToString();
    }
    public void in10(string newText)
    {
        float newValue = float.Parse(newText);

        //input to matrix object
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.value[1, 0] = newValue;

        m.findDet();
        matrixDet.text = "det: "+m.det.ToString();
    }
    public void in11(string newText)
    {
        float newValue = float.Parse(newText);

        //input to matrix object
        Matrix m = GameObject.FindObjectOfType<Matrix>();
        m.value[1, 1] = newValue;

        m.findDet();
        matrixDet.text = "det: "+m.det.ToString();
    }





    //Animations:
    public void setLinesMovementSpeed(string newText)
    {
        float newValue = float.Parse(newText);

        Line[] lines = GameObject.FindObjectsOfType<Line>();
        foreach(Line l in lines)
        {
            l.positionSpeed = newValue;
        }
    }

    public void setLinesRotationSpeed(string newText)
    {
        float newValue = float.Parse(newText);

        Line[] lines = GameObject.FindObjectsOfType<Line>();
        foreach (Line l in lines)
        {
            l.turnSpeed = newValue;
        }
    }

    public void setVectorMovementSpeed(string newText)
    {
        float newValue = float.Parse(newText);

        Vector[] vectors = GameObject.FindObjectsOfType<Vector>();
        foreach (Vector v in vectors)
        {
            v.speed = newValue;
        }
    }
}
