using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ConsoleApp2
{
    public class Swap
    {
        Dictionary<FieldName, State> states = new Dictionary<FieldName, State>();
        RateInfo field1, field2, field3, field4, field5, field6, field7, field8, field9;
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
                if (random.Next(50000) > 25040 && fieldName != FieldName.Field5)
                {
                    this.SetState(FieldName.Field5, State.Calculated);
                }
                if (random.Next(500001) > 250409 && fieldName != FieldName.Field6)
                {
                    this.SetState(FieldName.Field6, State.Calculated);
                }
                if (random.Next(5000777) > 2504777 && fieldName != FieldName.Field7)
                {
                    this.SetState(FieldName.Field7, State.Calculated);
                }
                if (random.Next(50008888) > 25048888 && fieldName != FieldName.Field8)
                {
                    this.SetState(FieldName.Field8, State.Calculated);
                }
                if (random.Next(500099999) > 250499999 && fieldName != FieldName.Field9)
                {
                    this.SetState(FieldName.Field9, State.Calculated);
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

        public RateInfo Field5
        {
            get { return this.field5; }
            set 
            { 
                this.field5 = value; 
                this.SetState(FieldName.Field5, State.SetByUser); 
                UpdateRandomField(FieldName.Field5); 
            }
        }
        public RateInfo Field6
        {
            get { return this.field6; }
            set 
            { 
                this.field6 = value; 
                this.SetState(FieldName.Field6, State.SetByUser); 
                UpdateRandomField(FieldName.Field6); 
            }
        }
        public RateInfo Field7
        {
            get { return this.field7; }
            set 
            { 
                this.field7 = value; 
                this.SetState(FieldName.Field8, State.SetByUser); 
                UpdateRandomField(FieldName.Field8); 
            }
        }
        public RateInfo Field8
        {
            get { return this.field8; }
            set 
            { 
                this.field8 = value; 
                this.SetState(FieldName.Field8, State.SetByUser); 
                UpdateRandomField(FieldName.Field8); 
            }
        }
        public RateInfo Field9
        {
            get { return this.field9; }
            set 
            { 
                this.field9 = value; 
                this.SetState(FieldName.Field9, State.SetByUser); 
                UpdateRandomField(FieldName.Field9); 
            }
        }
    }
}
