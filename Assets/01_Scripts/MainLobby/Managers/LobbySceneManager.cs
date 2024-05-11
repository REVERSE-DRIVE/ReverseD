using SaveDatas;
using UnityEngine;

namespace MainLobby
{
    public class LobbySceneManager : MonoBehaviour
    {
        public UserData userData;
        public LobbyData lobbyData;

        [SerializeField] private DialogueUI _dialogue;
        
        
        private void Start()
        {
            if (userData.isFirstPlay)
            {
                userData.isFirstPlay = false;
            }
        }
    }
}