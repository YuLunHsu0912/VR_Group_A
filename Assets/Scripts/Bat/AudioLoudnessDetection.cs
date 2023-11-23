using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
 
    void Start()
    {
        MicrophoneToAudioCLip();
        Debug.Log(Microphone.devices[0]); ///0 for earphone 1 for oculus
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MicrophoneToAudioCLip()
    {
        string microphoneName = Microphone.devices[0];///0 for earphone 1 for oculus
        microphoneClip = Microphone.Start(microphoneName, true, 20,AudioSettings.outputSampleRate);

    }
    public float GetLoudnessFromMicrophone()
    {
        return getLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }
    public float getLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPoisiton = clipPosition - sampleWindow;

        if (startPoisiton < 0)
            return 0;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPoisiton);

        //compute loudness
        float totalLoudness = 0;
        for (int i=0; i<sampleWindow;i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
