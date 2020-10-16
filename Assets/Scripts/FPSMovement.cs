using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// Handles the "VR" Movement without VR Eguipment
/// </summary>
public class FPSMovement : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float speed = 4.0f;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float xAxis = 0f;
    public float zAxis = 0f;

    public float rotationX = 0f;
    public float rotationY = 0f;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;

    public Transform bodyColliders;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        bodyColliders.position = transform.position - (Vector3.up * Valve.VR.InteractionSystem.Player.instance.eyeHeight);

        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * zAxis * speed * Time.deltaTime;
        transform.position += transform.right * xAxis * speed * Time.deltaTime;

        // When holding the right mousebutton
        if (Input.GetMouseButton(1))
        {
            // When enum that enables movement with X and Y Axis is chosen
            if (axes == RotationAxes.MouseXAndY)
            {
                rotAverageY = 0f;
                rotAverageX = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayY.Add(rotationY);
                rotArrayX.Add(rotationX);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }

                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }

                rotAverageY /= rotArrayY.Count;
                rotAverageX /= rotArrayX.Count;

                rotAverageY = ExtensionMethods.ClampAngle(rotAverageY, minimumY, maximumY);
                rotAverageX = ExtensionMethods.ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            // When enum with that enables movement with X Axis is chosen
            else if (axes == RotationAxes.MouseX) 
            {
                rotAverageX = 0f;

                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;

                rotAverageX = ExtensionMethods.ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            // When other enum is chosen , if adding more modes specify the enum with else if!
            else
            {
                rotAverageY = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

                rotArrayY.Add(rotationY);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;

                rotAverageY = ExtensionMethods.ClampAngle(rotAverageY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
    }



}
