using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }

    private int m_Score = 0;
    private bool m_Ended = false;
    
    [SerializeField] 
    private GameObject m_ScoreText;

    [SerializeField]
    private TextMeshProUGUI m_MenuScoreText;

    [SerializeField]
    private Player m_Player;

    [SerializeField]
    private GameObject m_Enemy;

    [SerializeField]
    private GameObject m_EndGameMenu;

    public void ScoreIncrement() {
        m_Score++;
        m_ScoreText.GetComponent<Text>().text = "Score: " + m_Score;
    }

    private void Update() {
        if (m_Player.m_Alive) return;
        if (!m_Ended) {
            //m_EndGameMenu.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Score: " + m_Score;
            m_MenuScoreText.text = "Score: " + m_Score;
            m_Enemy.SetActive(false);
            m_ScoreText.SetActive(false);
            m_EndGameMenu.SetActive(true);
            m_Ended = true;
        }
    }
}
