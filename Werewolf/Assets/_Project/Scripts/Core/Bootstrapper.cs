using UnityEngine;

namespace Werewolf.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Current.Clear();
            EventManager.Clear();
        }
    }
}
