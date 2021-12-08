using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public static OSCManager instance;

    private OSC osc;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        osc = GetComponent<OSC>();

        osc.SetAddressHandler("/Movement", HandleMovement);

    }

    public void HandleMovement(OscMessage msg)
    {
        Debug.Log(msg.values);
    }

    public void SendTrigger(Tile position)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/" + position.col.ToString();
        msg.values.Add("HELLO");
        osc.Send(msg);
    }

}
