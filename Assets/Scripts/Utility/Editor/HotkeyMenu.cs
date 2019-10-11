using UnityEngine;
using UnityEditor;

public class HotkeyMenu
{
	/*
	 * 단축키 수정 추가 키워드
	 *   "#" : Shift
	 *   "&" : alt
	 *   "%" : Ctrl (OS X : command)
	 *   "%&" : Ctrl + alt
	 * 
	 *   단축키 수정의 예
	 *     #5 : Shift + 5
	 *     &4 : alt + 4
	 */
	
	
	
	//GameObject 
	[MenuItem("HotKey/Gameobject %g", priority = -1)]
	static protected void HotGameObject()
	{
		GameObject c = new GameObject ( "GameObject" );
		if( Selection.activeTransform != null )
		{
			c.transform.parent = Selection.activeTransform;
			c.transform.localPosition = Vector3.zero;
			c.transform.localScale = Vector3.one;
			c.layer = Selection.activeGameObject.layer;

            var rect = Selection.activeGameObject.GetComponent<RectTransform>();
            if( rect != null )
            {
                c.AddComponent<RectTransform>();
            }
		}
		SelectObject ( c ) ;
	}

	//Select create object
	static private void SelectObject(GameObject obj)
	{
		if( Selection.activeTransform != null )
		{
			obj.transform.parent = Selection.activeTransform;
		}
		Selection.activeInstanceID = obj.GetInstanceID();
	}//end SelectObject
}//end Class HotKeyMenu