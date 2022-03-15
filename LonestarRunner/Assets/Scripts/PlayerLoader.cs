using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLoader : MonoBehaviour {
    private AssetBundle m_LoaderAssets;

    public string BundleName;

    private void Start() {
        LoadAssets(BundleName);
        InstantiatePlayer();
    }

    private void LoadAssets(string bundle_name) {

        m_LoaderAssets = AssetBundle.LoadFromFile(Application.dataPath + "/Bundle/" + bundle_name);
    }

    private void InstantiatePlayer() {
        var player = m_LoaderAssets.LoadAsset<GameObject>("Player");
        if (player == null) Debug.Log("Unable to load player");
        var enemy = m_LoaderAssets.LoadAsset<GameObject>("Enemy");
        if (enemy == null) Debug.Log("Unable to load enemy");

        var player_entity = Instantiate(player, new Vector3(0, 2, 10), Quaternion.identity, null);
        player_entity.name = "Player";
        var enemy_entity = Instantiate(enemy, new Vector3(0, 0, 45), Quaternion.identity, null);
        enemy_entity.name = "Enemy";
        
    }
}
