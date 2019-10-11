using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public GameManager m_GameManager = null;

    public void SetGameManager(GameManager manager)
    {
        m_GameManager = manager;
    }
}
