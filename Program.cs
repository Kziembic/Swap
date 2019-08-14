using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
#region constants

        private const string FIELD1 = "Field1";
        private const string FIELD2 = "Field2";
        private const string FIELD3 = "Field3";
        private const string FIELD4 = "Field4";
        private const string FIELD5 = "Field5";
        private const string FIELD6 = "Field6";
        private const string FIELD7 = "Field7";
        private const string FIELD8 = "Field8";
        private const string FIELD9 = "Field9";

#endregion
        private delegate void SetPropertyValue(string propertyName, Swap swap, RateInfo target);
        private delegate int GetPropertyValue<T>(string propertyName, T type);
        public static void SetFieldValue(string propertyName, Swap swap, RateInfo rateInfo)
        {
            PropertyInfo swapProperty = typeof(Swap).GetProperty(propertyName);
            
            if (swapProperty != null)
            {
                swapProperty.SetValue(swap, rateInfo, null);      
            }                
        }
        public static int GetFieldValue(string propertyName, Swap swap)
        {
            PropertyInfo property = swap.GetType().GetProperty(propertyName);
            var costam = property.GetValue(swap, null);
            var value = costam as RateInfo;        

            return value.Value;
        }

        private static FieldName GetEnumFromFieldName(string fieldName)
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

        private static void GetCombination(Swap swap, List<RateInfo> newList , ref List<Dictionary<FieldName, RateInfo>> productsCombination)
        {
            double count = Math.Pow(2, newList.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(newList.Count, '0');
                Dictionary<FieldName, RateInfo> fieldCombinations = new Dictionary<FieldName, RateInfo>();
                for (int j = 0; j < str.Length; j++)
                {                           
                    if (str[j] == '1')
                    {                              
                        fieldCombinations.Add((FieldName)j, newList[j]);
                    }           
                }
                productsCombination.Add(fieldCombinations);
            }
        }

        private static void SetToDefault(Swap us)
        {
            us.Field1 = new RateInfo() { Value = 11 };
            us.Field2 = new RateInfo() { Value = 22 };
            us.Field3 = new RateInfo() { Value = 33 };
            us.Field4 = new RateInfo() { Value = 44 };
            us.Field5 = new RateInfo() { Value = 55 };
            us.Field6 = new RateInfo() { Value = 66 };
            us.Field7 = new RateInfo() { Value = 77 };
            us.Field8 = new RateInfo() { Value = 88 };
            us.Field9 = new RateInfo() { Value = 99 };
        }
        static void Main(string[] args)
        {

            string[] FieldsConst = new string[] {FIELD1, FIELD2, FIELD3, FIELD4, FIELD5, FIELD6, FIELD7, FIELD8, FIELD9};
            RateInfo[] newRates = new RateInfo[]
            {
                new RateInfo {Value = 1},
                new RateInfo {Value = 2},
                new RateInfo {Value = 3},
                new RateInfo {Value = 4},
                new RateInfo {Value = 5},
                new RateInfo {Value = 6},
                new RateInfo {Value = 7},
                new RateInfo {Value = 8},
                new RateInfo {Value = 9}
            };

            Swap us = new Swap();
            SetPropertyValue setSwapValue = new SetPropertyValue(SetFieldValue);
            GetPropertyValue<Swap> getSwapValue = new GetPropertyValue<Swap>(GetFieldValue);

            using (us.GetContext(State.Default))
            {
                SetToDefault(us);
            }

            List<RateInfo> rateInfos = GetOriginalRateInfos(us);
            List<Dictionary<FieldName, RateInfo>> productsCombination = new List<Dictionary<FieldName, RateInfo>>();                   
            GetCombination(us, rateInfos, ref productsCombination);
            
            foreach (var combination in productsCombination)
            {
                foreach (var item in combination)
                {
                    Console.Write($"{item.Key} " + $"{item.Value.Value}, ");
                }
                Console.WriteLine();
            }

            // Myślę że trzeba tu tylko jeszcze wrzucić wartości do jakichś słowników i będzie to co trzeba.
            // Tylko
            GetAllCombinations(us, FieldsConst, newRates);
        }
        private static List<RateInfo> GetOriginalRateInfos(Swap us)
        {
            List<RateInfo> rateInfos = new List<RateInfo>();
            rateInfos.Add(us.Field1);
            rateInfos.Add(us.Field2);
            rateInfos.Add(us.Field3);
            rateInfos.Add(us.Field4);
            rateInfos.Add(us.Field5);
            rateInfos.Add(us.Field6);
            rateInfos.Add(us.Field7);
            rateInfos.Add(us.Field8);
            rateInfos.Add(us.Field9);

            return rateInfos;
        }
        static void GetAllCombinations(Swap us, string[] fields, RateInfo[] rateInfos)
        {  
            var listToBeChanged = GetOriginalRateInfos(us);     
            double count = Math.Pow(2, listToBeChanged.Count);

            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(listToBeChanged.Count, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        SetFieldValue(fields[j], us, rateInfos[j]);
                        listToBeChanged[j] = rateInfos[j];
                    }        
                }
                 
                using (us.GetContext(State.Default))
                {
                    SetToDefault(us);
                }

                listToBeChanged = GetOriginalRateInfos(us);
                Console.WriteLine();
            }
        }
    }
}
