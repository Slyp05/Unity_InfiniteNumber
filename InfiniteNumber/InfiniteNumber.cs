using System.Linq;
using System.Numerics;
using UnityEngine;

/*

    InfiniteNumber created by Lucas Sarkadi.

    Creative Commons Zero v1.0 Universal licence, 
    meaning it's free to use in any project with no need to ask permission or credits the author.

    Check out the github page for more informations:
    https://github.com/Slyp05/Unity_InfiniteNumber

*/
[System.Serializable] public struct InfiniteNumber : ISerializationCallbackReceiver
{
    // consts
    const char separationCharacter = ' ';

    // public
    public BigInteger Value;

    // static
    public static string FormatString(string input)
    {
        for (int i = input.Length - 3; i > 0; i -= 3)
            if (input[i - 1] != '-')
                input = input.Insert(i, separationCharacter.ToString());
        return input;
    }

    // constructeurs
    public InfiniteNumber(BigInteger val)
    {
        Value = val;
        serializedValue = Value.ToString();
    }

    public InfiniteNumber(string valStr)
    {
        Value = 0;
        serializedValue = string.Empty;

        SetValueFromString(valStr);
    }

    // implicit/explicit cast to Infinite Number
    public static implicit operator InfiniteNumber(BigInteger bi) => new InfiniteNumber(bi);

    public static implicit operator InfiniteNumber(string valStr) => new InfiniteNumber(valStr);

    public static implicit operator InfiniteNumber(sbyte b) => new InfiniteNumber(b);
    public static implicit operator InfiniteNumber(byte b) => new InfiniteNumber(b);
    public static implicit operator InfiniteNumber(short s) => new InfiniteNumber(s);
    public static implicit operator InfiniteNumber(ushort s) => new InfiniteNumber(s);
    public static implicit operator InfiniteNumber(int i) => new InfiniteNumber(i);
    public static implicit operator InfiniteNumber(uint i) => new InfiniteNumber(i);
    public static implicit operator InfiniteNumber(long l) => new InfiniteNumber(l);
    public static implicit operator InfiniteNumber(ulong l) => new InfiniteNumber(l);

    public static explicit operator InfiniteNumber(float f) => new InfiniteNumber((BigInteger)f);
    public static explicit operator InfiniteNumber(double d) => new InfiniteNumber((BigInteger)d);
    public static explicit operator InfiniteNumber(decimal d) => new InfiniteNumber((BigInteger)d);

    // implicit/explicit cast from Infinite Number
    public static implicit operator BigInteger(InfiniteNumber iNb) => iNb.Value;
    
    public static explicit operator sbyte(InfiniteNumber iNb) => (sbyte)iNb.Value;
    public static explicit operator byte(InfiniteNumber iNb) => (byte)iNb.Value;
    public static explicit operator short(InfiniteNumber iNb) => (short)iNb.Value;
    public static explicit operator ushort(InfiniteNumber iNb) => (ushort)iNb.Value;
    public static explicit operator int(InfiniteNumber iNb) => (int)iNb.Value;
    public static explicit operator uint(InfiniteNumber iNb) => (uint)iNb.Value;
    public static explicit operator long(InfiniteNumber iNb) => (long)iNb.Value;
    public static explicit operator ulong(InfiniteNumber iNb) => (ulong)iNb.Value;

    public static explicit operator float(InfiniteNumber iNb) => (float)iNb.Value;
    public static explicit operator double(InfiniteNumber iNb) => (double)iNb.Value;
    public static explicit operator decimal(InfiniteNumber iNb) => (decimal)iNb.Value;

    // operators overload
    public static InfiniteNumber operator +(InfiniteNumber iNb) => iNb;
    public static InfiniteNumber operator -(InfiniteNumber iNb) => new InfiniteNumber(-iNb.Value);
    public static InfiniteNumber operator ~(InfiniteNumber iNb) => new InfiniteNumber(~iNb.Value);
    public static InfiniteNumber operator ++(InfiniteNumber iNb) => new InfiniteNumber(iNb.Value + 1);
    public static InfiniteNumber operator --(InfiniteNumber iNb) => new InfiniteNumber(iNb.Value - 1);

    public static InfiniteNumber operator +(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value + b.Value);
    public static InfiniteNumber operator -(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value - b.Value);
    public static InfiniteNumber operator *(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value * b.Value);
    public static InfiniteNumber operator /(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value / b.Value);
    public static InfiniteNumber operator %(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value % b.Value);

    public static InfiniteNumber operator &(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value & b.Value);
    public static InfiniteNumber operator |(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value | b.Value);
    public static InfiniteNumber operator ^(InfiniteNumber a, InfiniteNumber b) => new InfiniteNumber(a.Value ^ b.Value);
    public static InfiniteNumber operator <<(InfiniteNumber iNb, int decal) => new InfiniteNumber(iNb.Value << decal);
    public static InfiniteNumber operator >>(InfiniteNumber iNb, int decal) => new InfiniteNumber(iNb.Value >> decal);

    public static bool operator ==(InfiniteNumber a, InfiniteNumber b) => (a.Value == b.Value);
    public static bool operator !=(InfiniteNumber a, InfiniteNumber b) => (a.Value != b.Value);
    public static bool operator <(InfiniteNumber a, InfiniteNumber b) => (a.Value < b.Value);
    public static bool operator >(InfiniteNumber a, InfiniteNumber b) => (a.Value > b.Value);
    public static bool operator <=(InfiniteNumber a, InfiniteNumber b) => (a.Value <= b.Value);
    public static bool operator >=(InfiniteNumber a, InfiniteNumber b) => (a.Value >= b.Value);

    // overrides
    public override bool Equals(object obj) => Value.Equals(obj);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => FormatString(Value.ToString());

    // private methods
    void SetValueFromString(string valStr)
    {
        if (valStr == null)
        {
            Value = 0;
            serializedValue = Value.ToString();
            return;
        }

        string filteredStr = new string(valStr.Where((char c) => char.IsDigit(c) || c == '-').ToArray());
        
        if (BigInteger.TryParse(filteredStr, out BigInteger res))
            Value = res;
        else
            Value = 0;
        
        serializedValue = Value.ToString();
    }

    // serialize
    [SerializeField] string serializedValue;

    public void OnBeforeSerialize()
    {
        serializedValue = Value.ToString();
    }

    public void OnAfterDeserialize()
    {
        SetValueFromString(serializedValue);
    }
}
