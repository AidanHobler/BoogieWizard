using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public static OSCManager instance;

    private OSC osc;

    [SerializeField]
    private WizardBehavior wizard;

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

        osc.SetAddressHandler("/abletonout", HandleAbletonOut);

    }

    public void HandleAbletonOut(OscMessage msg)
    {
        if ((int) msg.values[0] == 1)
        {
            wizard.OnBeat();
        }
        else
        {
            Debug.Log((int)msg.values[0]);
            FloorManager.instance.MarchColumn((int) msg.values[0] - 2);
        }

    }

    public void HandleMovement(OscMessage msg)
    {
        Debug.Log(msg.values);
    }

    public void SendTrigger(Tile position)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/Track";
        msg.values.Add(position.col * FloorManager.instance.numCols + position.row);
        osc.Send(msg);
    }

    public void SendClear(int col)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/Clear";
        msg.values.Add(col);
        osc.Send(msg);

    }

    public void SendStart()
    {
        OscMessage msg = new OscMessage();
        msg.address = "/Timing";
        msg.values.Add(1);
        osc.Send(msg);

    }

    public void SendFinish()
    {
        OscMessage msg = new OscMessage();
        msg.address = "/Timing";
        msg.values.Add(0);
        osc.Send(msg);

    }

}
