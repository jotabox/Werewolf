using System.Collections.Generic;
using UnityEngine;

namespace Werewolf.Input
{
    public class InputBuffer
    {
        private readonly Dictionary<string, float> buffer =
            new Dictionary<string, float>();

        private readonly float bufferTime;

        public InputBuffer(float bufferTime)
        {
            this.bufferTime = bufferTime;
        }

        public void Register(string action)
        {
            buffer[action] = Time.time;
        }

        public bool Consume(string action)
        {
            if (!buffer.ContainsKey(action))
                return false;

            if (Time.time - buffer[action] <= bufferTime)
            {
                buffer.Remove(action);
                return true;
            }

            buffer.Remove(action);
            return false;
        }
    }
}
