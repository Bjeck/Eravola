using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInvoke : MonoBehaviour
{

    public Button button;

    public void InvokeButton()
    {
        button.onClick.Invoke();
    }

}
	


