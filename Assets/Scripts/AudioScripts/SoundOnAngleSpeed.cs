using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAngleSpeed : MonoBehaviour
{
    public float angularSpeed;
    public Quaternion previousAngle = Quaternion.identity;
    public float averageSpeed;

    [FMODUnity.EventRef]
    public string RotateSound = "";
    FMOD.Studio.EventInstance instance;

    public int RotateSoundParam;
    public string ParameterName = "Puzzle_wheel_speed";

    public int arraySize = 100;
    public float[] speedArray;
    public int index = 0;

    public int volumeDivider = 20;
    void Start()
    {
        speedArray = new float[arraySize];

        instance = FMODUnity.RuntimeManager.CreateInstance(RotateSound);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, this.transform)
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        angularSpeed = (transform.rotation.eulerAngles - previousAngle.eulerAngles).magnitude;

        previousAngle = transform.rotation;
        speedArray[index % arraySize] = angularSpeed;
        index++;
        float sum = 0;
        for (int i = 0; i < arraySize; i++)
        {
            sum += speedArray[i];
        }
        averageSpeed = sum / arraySize;
    }

    private void Update()
    {
        instance.setParameterByName(ParameterName, Mathf.Clamp01(averageSpeed/volumeDivider));
      
        /*instance.getParameterByName(ParameterName, out float value);
        Debug.Log(value);*/

        instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        //Debug.Log(state);
        if (averageSpeed != 0)
        {
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                instance.start();
                Debug.Log("start");
            }
        }
        /*else
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Debug.Log("stop");
        }*/
    
    }

 }

