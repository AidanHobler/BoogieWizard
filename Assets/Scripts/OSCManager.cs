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

    }

    public void HandleMovement(OscMessage msg)
    {
        Debug.Log(msg.values);
    }

    public void SendTrigger(Tile position)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/Track/" + position.col.ToString();
        msg.values.Add(position.row);
        osc.Send(msg);
    }

}
