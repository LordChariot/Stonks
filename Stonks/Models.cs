using System;
using System.Collections.Generic;
using System.Reflection;

namespace Stonks
{
    public class StockDefinition
    {
        public string Symbol { get; set; }
        public decimal Shares { get; set; }
        public decimal PricePaid { get; set; }
        public string Notes { get; set; }
        public bool Chart { get; set; }

    }

    public class StockViewModel
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Shares { get; set; }
        public decimal PricePaid { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PercentChange { get; set; }
        public decimal FiftyTwoWeekHigh { get; set; }
        public decimal FiftyTwoWeekLow { get; set; }
        public string Notes { get; set; }
        public bool Chart { get; set; }

        public decimal Gain => (CurrentPrice - PricePaid) * Shares;
        public decimal GainPercent => PricePaid == 0 ? 0 : (CurrentPrice - PricePaid) / PricePaid * 100;
        public decimal Paid => PricePaid * Shares;
        public decimal Value => CurrentPrice * Shares;
    }

    [Serializable]
    public enum ChartValues
    {
        [StringValue("Price")] Price = 0,
        [StringValue("Percent Change")] PercentChange = 1,
        [StringValue("Value")] Value = 2,
        [StringValue("Gain")] Gain = 3,
        [StringValue("Percent Gain")] PercentGain = 4

    }

    [Serializable]
    public enum LogTypes
    {
        [StringValue("System")] System = 0,
        [StringValue("Chart")] Chart = 1
    }


}
public static class StringEnum
{
    public static T StringToEnum<T>(string value)
    {
        T enumValue = default(T);
        foreach (Enum t in Enum.GetValues(typeof(T)))
        {
            if (StringEnum.GetStringValue(t) == value)
            {
                return (T)Enum.Parse(typeof(T), t.ToString());
            }
        }
        return (T)Enum.Parse(typeof(T), enumValue.ToString());
    }
    public static string GetStringValue(Enum value)
    {
        string output = null;
        try
        {
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
        }
        catch { }
        return output;
    }
    public static string[] GetStringValues(Type type)
    {
        List<String> a = new List<string>();
        foreach (Enum t in Enum.GetValues(type))
        {
            a.Add(GetStringValue(t));
        }
        return a.ToArray();
    }
    public static string[] GetStringKeyValues(Type type)
    {
        List<String> a = new List<string>();
        foreach (Enum t in Enum.GetValues(type))
        {
            a.Add($"{GetStringValue(t)}");
        }
        return a.ToArray();
    }
}
public class StringValue : System.Attribute
{
    private readonly string _value;

    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

}