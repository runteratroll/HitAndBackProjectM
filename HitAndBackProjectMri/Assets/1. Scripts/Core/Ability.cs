using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatringCharacters.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract IEnumerator Cast();
    }
}

