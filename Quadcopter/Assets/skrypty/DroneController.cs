using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour
{
    public float engineForce;

    public float powerIncreaseSpeed=1f;
    public float power = 0; //0-100%
    public float engineFL = 0, engineFR = 0, engineBL = 0, engineBR = 0; //0-100%
    public Animator FL, FR, BL, BR;
    public float turnSpeed = 1f;
    public float normalizationRotSpeed = 1f;
    public Rigidbody rb;
    float lvlToMaintain = 0;
    bool maintainLvl = false;

    public float engineFalloffAnimation=0.5f;


    float lastFrameVel;

    void Update ()
    {
        readUserInput();
        updateEngineAnimations();
        generateLift();
        checkMaintainedLvl();//use some level difference for activating different engine forces to maintain given height
        if (maintainLvl == true)
            applyLvlMaintainance();
        rb.velocity *= 0.99f;
        lastFrameVel = rb.velocity.y;
    }
    void readUserInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (power < 100f)
                power += Time.deltaTime * powerIncreaseSpeed;
        }
        if (Input.GetKey(KeyCode.F))
        {
            if (power > 0)
                power -= Time.deltaTime * powerIncreaseSpeed;
        }
        //stabilize
        if (power > 100f)
            power = 100f;
        if (power < 0)
            power = 0;

        if (Input.GetKey(KeyCode.W))
        {
            engineFL = power;
            engineFR = power;
            engineBL = power * engineFalloffAnimation;
            engineBR = power * engineFalloffAnimation;
            this.transform.Rotate((Vector3.right + Vector3.back).normalized, power * turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            engineFL = power * engineFalloffAnimation;
            engineFR = power * engineFalloffAnimation;
            engineBL = power;
            engineBR = power;
            this.transform.Rotate((Vector3.left + Vector3.forward).normalized, power * turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            engineFL = power * engineFalloffAnimation;
            engineFR = power;
            engineBL = power * engineFalloffAnimation;
            engineBR = power;
            this.transform.Rotate((Vector3.left + Vector3.back ).normalized, power * turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            engineFL = power;
            engineFR = power * engineFalloffAnimation;
            engineBL = power;
            engineBR = power * engineFalloffAnimation;
            this.transform.Rotate((Vector3.right + Vector3.forward ).normalized, power * turnSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, 100*turnSpeed * Time.deltaTime);
        }
        else
        {
            normalizeRotation();
            engineFL = power;
            engineFR = power;
            engineBL = power;
            engineBR = power;
        }
    }
    void updateEngineAnimations()
    {
        FL.speed = engineFL/100f;
        FR.speed = engineFR/100f;
        BL.speed = engineBL/100f;
        BR.speed = engineBR/100f;
    }
    void normalizeRotation()
    {
        Quaternion dest = Quaternion.identity;
        dest.eulerAngles.Set(0, this.transform.rotation.y, 0);
        
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, dest, Time.deltaTime);
    }
    void generateLift()
    {
        //Debug.Log("odchylenie: "+Vector3.Dot(Vector3.up, transform.up));
        rb.AddForce(this.transform.up * power / 10f);
    }
    void checkMaintainedLvl()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            maintainLvl = true;
            lvlToMaintain = this.transform.position.y;
        }
        else if(Input.GetKeyDown(KeyCode.N))
        {
            maintainLvl = false;
        }
    }
    void applyLvlMaintainance()
    {
        float dist = transform.position.y - lvlToMaintain;
        Debug.Log(dist);


        if (dist < -30)//go up hard
        {
            power = 100f;
        }
        else if (dist < -5)//go up medium
        {
            power = 85f;
        }
        else if(dist < 0)//go up soft
        {
            power = 82.6f;
        }
        else if (dist < 5)//go down soft
        {
            power = 82f;
        }
        else if(dist<30)//go down medium
        {
            power = 78.5f;
        }
        else//go down hard
        {
            power = 50f;
        }
    }
}
