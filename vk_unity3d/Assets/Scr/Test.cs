using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Text _text;

    public void MyFunction(string arg)
    {
        _text.text = arg;
    }

    public void Click()
    {
        Application.ExternalCall("SayHello", "The game says hello!");
    }
}
