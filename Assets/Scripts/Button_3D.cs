using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Button_3D_Action { None, Quit }

public class Button_3D : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnClick;
    [SerializeField] private Button_3D_Action buttonAction;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
            if (Physics.Raycast(ray, out RaycastHit hit, 10) && hit.transform.gameObject == gameObject)
			{
				Click();
			}
		}
    }

    public void Click()
	{
        OnClick?.Invoke();

		switch (buttonAction)
		{
			case Button_3D_Action.Quit: //Quit the application or exit playmode in editor
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
				break;
			case Button_3D_Action.None: //Doesn't do anything
				break;
			default:
				break;
		}
	}
}