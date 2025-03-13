using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwordHandler : MonoBehaviour
{
    [SerializeField] int levelSword = 0;
    private SwordSystem swordSystem;
    
    void Start()
    {
        swordSystem = GetComponent<SwordSystem>();
        UpdateSwordInventory();
    }
    public void LevelUp(int addLevel)
    {
        levelSword += addLevel;
        UpdateSwordInventory();
    }
    // Cập nhật số lượng kiếm trong mảng dựa vào level
    private void UpdateSwordInventory()
    {
        if (!swordSystem) return;
       swordSystem.UpdateSwordInventory(levelSword);
    }
    
}
