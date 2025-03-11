using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwordManager : MonoBehaviour
{
    [SerializeField] GameObject swordPrefab;
    [SerializeField] int levelSword = 0;
    private List<GameObject> swords = new List<GameObject>();
    [SerializeField] private float radius = 2.0f;
    private Character playerCharacter;
    
    void Awake()
    {
        StatsRefresh.OnRefresh += RefreshSwordStats; // Đăng ký sự kiện
    }
    
    void OnDestroy()
    {
        StatsRefresh.OnRefresh -= RefreshSwordStats; // Đăng ký sự kiện
    }
    void Start()
    {
        playerCharacter = GetComponent<Character>();
        UpdateSwordInventory();
    }
    public void LevelUp(int addLevel)
    {
        Debug.Log($"check ***************************{levelSword}");
        levelSword += addLevel;
        UpdateSwordInventory();
    }
    // Cập nhật số lượng kiếm trong mảng dựa vào level
    private void UpdateSwordInventory()
    {
        foreach (GameObject sword in swords)
        {
            Destroy(sword);
        }
        swords.Clear();

        for (int i = 0; i < levelSword; i++)
        {
            SpawnSword(i);
        }
        
    }
    private void SpawnSword(int i)
    {
        float angle = i * (360f / levelSword); // Chia đều góc trên vòng tròn
            float radian = angle * Mathf.Deg2Rad; // Chuyển sang radian

            // Tính vị trí x, y theo vòng tròn
            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;
            
            Vector3 spawnPosition = new Vector3(x,y,0);
            
    Debug.Log($"check spawn sword {i}");
            GameObject newSword = Instantiate(swordPrefab, spawnPosition, Quaternion.identity);
            Sword newSwordComponent = newSword.GetComponent<Sword>();
            Attr TotalStats = playerCharacter.GetCharacterStats();
            
            newSwordComponent.RefreshSwordStats(TotalStats);
            swords.Add(newSword);
            newSword.transform.SetParent(this.transform, false);
    }

    
    public void RefreshSwordStats( Attr totalStats)
    {
        foreach (GameObject sword in swords)
        {
            Sword swordComponent = sword.GetComponent<Sword>();
            swordComponent.RefreshSwordStats(totalStats);
        }
    }
    
}
