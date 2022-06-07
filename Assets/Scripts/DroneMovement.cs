using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneMovement : MonoBehaviour
{
    public GameBehavior gameManager;
    Rigidbody ourDrone;
    private int energyReserve;
    private float upForce=0f;
    private bool switchOnDrone;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        ourDrone = GetComponent<Rigidbody>();
        energyReserve=gameManager.Energy;
        switchOnDrone = false;
    }
    void FixedUpdate()
    { 
         energyReserve=gameManager.Energy;
        OnorOffDrone();
       
        if (switchOnDrone &&  (energyReserve>0)){
            energyReserve-=1;
            MovementUpDown();
            MovementForward();
            MoveRotation();
            //MovementLeftRight();
            ClampingSpeedValues();
            Swerwe();
            ourDrone.AddRelativeForce(Vector3.up*upForce);
            ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward,currentYRotation,tiltAmountSideways));
            
            gameManager.Energy=energyReserve;
            
        }
        else{
            
             upForce = 0f;
             ourDrone.AddRelativeForce(Vector3.up*upForce);
        } 
    }
    void OnorOffDrone()
    {
        if  (Input.GetKey(KeyCode.F))
            switchOnDrone = false;
        if  (Input.GetKey(KeyCode.O))
            switchOnDrone = true;
    }
    void MovementUpDown ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            upForce=450;
            energyReserve-=1;
        }
        else if (Input.GetKey(KeyCode.LeftControl)){
            upForce = -200;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            upForce = 98.1f;
        }
        
    }
    
    private float MovementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward ;
    void MovementForward()
    {
        if (Input.GetAxis("Vertical") !=0)
        {
            energyReserve-=1;
            ourDrone.AddRelativeForce(Vector3.forward*Input.GetAxis("Vertical")*MovementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward,20*Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.17f);
        }
    }
    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmountByKyes = 2.5f;
    private float rotationYVelocity;
    void MoveRotation()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            wantedYRotation -=rotateAmountByKyes;
        }
        if(Input.GetKey(KeyCode.E))
        {
            wantedYRotation +=rotateAmountByKyes;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation,wantedYRotation,ref rotationYVelocity,0.25f);
        //ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward,currentYRotation,tiltAmountSide));
    }
    public Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues(){
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,10.0f,Time.deltaTime*5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,10.0f,Time.deltaTime*5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f){
            ourDrone.velocity=Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,5.0f,Time.deltaTime*5f));
        }
         if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f){
             ourDrone.velocity=Vector3.SmoothDamp(ourDrone.velocity,Vector3.zero,ref velocityToSmoothDampToZero,0.95f);
         }
    }
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerwe(){
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f){
            ourDrone.AddRelativeForce(Vector3.right*Input.GetAxis("Horizontal")*sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal"),ref tiltAmountVelocity,0.1f);
        }
        else{
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways,0,ref tiltAmountVelocity,0.1f);
        }
    }

}
