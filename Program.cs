using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
        private const string FIELD1 = "Field1";
        private const string FIELD2 = "Field2";
        private const string FIELD3 = "Field3";
        private const string FIELD4 = "Field4";
        private delegate void SetPropertyValue(int value, string propertyName, Swap swap, RateInfo target);
        private delegate int GetPropertyValue<T>(string propertyName, T type);
        public static void SetFieldValue(int value, string propertyName, Swap swap, RateInfo rateInfo)
        {
            PropertyInfo swapProperty = typeof(Swap).GetProperty(propertyName);
            PropertyInfo rateInfoProperty = typeof(RateInfo).GetProperty("Value");

            if (swapProperty != null && rateInfoProperty != null)
            {
                rateInfoProperty.SetValue(rateInfo, value, null);
                var fieldName = GetEnum(propertyName);
                swap.SetState(fieldName, State.Default);       
            }                
        }

        public static int GetFieldValue(string propertyName, Swap swap)
        {
            PropertyInfo property = swap.GetType().GetProperty(propertyName);
            var costam = property.GetValue(swap, null);
            var value = costam as RateInfo;        

            return value.Value;
        }

        private static FieldName GetEnum(string fieldName)
        {
            var enums = Enum.GetValues(typeof(FieldName));
            foreach (var item in enums)
            {
                if(string.Equals(fieldName, ((FieldName)item).ToString()))
                {
                    return (FieldName)item;
                }              
            }

            throw new ArgumentNullException();
        }
        private static void GetCombination(List<RateInfo> newList , ref List<List<RateInfo>> productsCombination)
        {
            double count = Math.Pow(2, newList.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(newList.Count, '0');
                List<RateInfo> fieldCombinations = new List<RateInfo>();
                for (int j = 0; j < str.Length; j++)
                {                           
                    if (str[j] == '1')
                    {                              
                        fieldCombinations.Add(newList[j]);
                    }           
                }
                productsCombination.Add(fieldCombinations);
            }
        }

        static void Main(string[] args)
        {        
            Swap us = new Swap();
            SetPropertyValue setSwapValue = new SetPropertyValue(SetFieldValue);
            GetPropertyValue<Swap> getSwapValue = new GetPropertyValue<Swap>(GetFieldValue);
            
            //inicjalizacja pól wartościami domyślnymi
            using (us.GetContext(State.Default))
            {
                us.Field1 = new RateInfo() { Value = 10 };
                us.Field2 = new RateInfo() { Value = 22 };
                us.Field3 = new RateInfo() { Value = 133 };
                us.Field4 = new RateInfo() { Value = 1024 };           
            }

            //sprawdzenie stanu: domyślny
            Console.WriteLine(us.GetState(FieldName.Field1));
            Console.WriteLine(us.GetState(FieldName.Field2));
            Console.WriteLine(us.GetState(FieldName.Field3));
            Console.WriteLine(us.GetState(FieldName.Field4));

            List<RateInfo> rateInfos = new List<RateInfo>();
                rateInfos.Add(us.Field1);
                rateInfos.Add(us.Field2);
                rateInfos.Add(us.Field3);
                rateInfos.Add(us.Field4);

            SetFieldValue(10, FIELD2, us,  us.Field2);
            List<List<RateInfo>> productsCombination = new List<List<RateInfo>>();

            GetCombination(rateInfos, ref productsCombination);

            foreach (var combination in productsCombination)
            {
                    foreach (var item in combination)
                    {  
                         Console.Write($"{item.Value} ");                                  
                    }
                    Console.WriteLine();
            }
            //modyfikacja przez użytkownika
            SetFieldValue(243, FIELD2, us, us.Field2);

            // sprawdzenie stanu: setByUser
            Console.WriteLine(us.GetState(FieldName.Field2));
            Console.WriteLine(GetFieldValue(FIELD4, us));
        }
    }
}
