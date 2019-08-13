using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ConsoleApp2
{
    public class Swap
    {
        Dictionary<FieldName, State> states = new Dictionary<FieldName, State>();
        RateInfo field1, field2, field3, field4;
        Random random = new Random(63);
        Context ctx;
        public Swap()
        {
            ctx = new Context();
        }
        
        public Context GetContext(State state)
        {
            ctx.SetContext(state);

            return this.ctx;
        }

        public void SetState(FieldName fieldName, State state)
        {
            ctx.SetState(this.states, fieldName, state);
        }

        public State GetState(FieldName fieldName)
        {
            if (states.ContainsKey(fieldName))
            {
                return states[fieldName];
            }
            else return State.Default;
        }

        public RateInfo Field1
        {
            get { return this.field1; }
            set 
            { 
                this.field1 = value; 
                this.SetState(FieldName.Field1, State.SetByUser); 
                UpdateRandomField(FieldName.Field1); 
            }
        }

        private void UpdateRandomField(FieldName fieldName)
        {
            if (ctx.IsInState(State.Undefined))
            {
                if (random.Next(5) >2 && fieldName != FieldName.Field1)
                {
                    this.SetState(FieldName.Field1, State.Calculated);
                }
                if (random.Next(52) > 25 && fieldName != FieldName.Field2)
                {
                    this.SetState(FieldName.Field2, State.Calculated);
                }
                if (random.Next(500) > 250 && fieldName != FieldName.Field3)
                {
                    this.SetState(FieldName.Field3, State.Calculated);
                }
                if (random.Next(5000) > 2504 && fieldName != FieldName.Field4)
                {
                    this.SetState(FieldName.Field4, State.Calculated);
                }
            }      
        }

        public RateInfo Field2
        {
            get { return this.field2; }
            set 
            { 
                this.field2 = value; 
                this.SetState(FieldName.Field2, State.SetByUser); 
                UpdateRandomField(FieldName.Field2); 
            }
        }

        public RateInfo Field3
        {
            get { return this.field3; }
            set 
            { 
                this.field3 = value; 
                this.SetState(FieldName.Field3, State.SetByUser); 
                UpdateRandomField(FieldName.Field3); 
            }
        }

        public RateInfo Field4
        {
            get { return this.field4; }
            set 
            { this.field4 = value;
              this.SetState(FieldName.Field4, State.SetByUser);
              UpdateRandomField(FieldName.Field4);
            }
        }
    }
}
