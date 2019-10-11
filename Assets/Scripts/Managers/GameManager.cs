using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager m_instance = null;
    static public GameManager Instance
    {
        get { return m_instance; }
    }

    [SerializeField] private TileManager m_TileManager = null;
    [SerializeField] private CraftManager m_CraftManager = null;
    [SerializeField] private MonsterManager m_MonsterManager = null;

    public TileManager TileManager
    {
        get { return m_TileManager; }
    }

    public CraftManager CraftManager
    {
        get { return m_CraftManager; }
    }

    public MonsterManager MonsterManager
    {
        get { return m_MonsterManager; }
    }

    private void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        m_TileManager.SetGameManager(this);
        m_CraftManager.SetGameManager(this);
        m_MonsterManager.SetGameManager(this);

        m_CraftManager.Init();
        m_TileManager.Init();
        m_MonsterManager.Init();
    }
}
