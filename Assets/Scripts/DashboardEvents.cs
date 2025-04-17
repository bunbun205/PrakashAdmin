using UnityEngine;
using UnityEngine.UIElements;

public class DashboardEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _button;

    private DataManagement dbmngr;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("LoadData") as Button;

        dbmngr = GameObject.Find("DataManager").GetComponent<DataManagement>();
        _button.RegisterCallback<ClickEvent>(OnLoadDataClicked);
        
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnLoadDataClicked);
    }

    private void OnLoadDataClicked(ClickEvent evt) => dbmngr.RecieveData();
}
