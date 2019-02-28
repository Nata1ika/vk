using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] GameObject _infoObj;
    [SerializeField] Text _infoText;

    private void Start()
    {
        MainController.ChangeTargetEvent += ChangeTarget;
    }

    private void OnDestroy()
    {
        MainController.ChangeTargetEvent -= ChangeTarget;
    }

    private void ChangeTarget(Target obj)
    {
        _infoObj.gameObject.SetActive(obj != null);
        _infoText.text = obj?.gameObject.name;
    }
}
