using UnityEngine;
using System.Collections;
using System.IO.Ports;
public class ArduinoScript : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM5", 9600);
    float[] lastRot = { 0, 0, 0 };

    void Awake()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }
    void Update()
    {
        try
        {
            string value = sp.ReadLine();
            string[] vector3 = value.Split(',');
            if (vector3[0] != "" && vector3[1] != "")
            {
                transform.Rotate(float.Parse(vector3[0]) - lastRot[0], float.Parse(vector3[1]) - lastRot[1],  0f,  Space.Self);
                lastRot[0] = float.Parse(vector3[0]);
                lastRot[1] = float.Parse(vector3[1]);
                sp.BaseStream.Flush();
            }
            for (int i = 0; i < vector3.Length; i++)
            {
                Debug.Log(vector3[i]);
            }
        }
        catch (System.Exception) {}
    }
}