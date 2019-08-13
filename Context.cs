using System;
using System.Collections.Generic;
namespace ConsoleApp2
{
     public class Context : IDisposable
    {
        State currentState=State.Undefined;
        public void SetState(Dictionary<FieldName, State> dict, FieldName fieldName, State state)
        {
            if (dict.ContainsKey(fieldName))
            {
                dict[fieldName] = state;
            }
            else
            {
                dict.Add(fieldName, state);
            }             
        }

        public bool IsInState(State state) => currentState == state;
        public void Dispose() => currentState = State.Undefined;
        internal void SetContext(State state) => this.currentState = state;
    }
}
