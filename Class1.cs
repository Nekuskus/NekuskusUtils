using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NekuskusUtils
{
    public struct QInt : IComparable, IConvertible, IComparable<QInt>, IEquatable<QInt>, IQueryable<QInt>
    {
        public QInt(long value)
        {
            m_value = value;
        }
        public const long MaxValue = Int64.MaxValue;
        public const long MinValue = Int64.MinValue;
        internal long m_value;
        //
        // Operators
        //
        public static QInt operator +(QInt i, QInt i2) => new QInt(i.m_value + i2.m_value);
        public static QInt operator -(QInt i, QInt i2) => new QInt(i.m_value - i2.m_value);
        public static QInt operator *(QInt i, QInt i2) => new QInt(i.m_value * i2.m_value);
        public static QInt operator /(QInt i, QInt i2) => new QInt(i.m_value / i2.m_value);
        public static QInt operator %(QInt i, QInt i2) => new QInt(i.m_value % i2.m_value);
        public static QInt operator +(QInt i, long i2) => new QInt(i.m_value + i2);
        public static QInt operator -(QInt i, long i2) => new QInt(i.m_value - i2);
        public static QInt operator *(QInt i, long i2) => new QInt(i.m_value * i2);
        public static QInt operator /(QInt i, long i2) => new QInt(i.m_value / i2);
        public static QInt operator %(QInt i, long i2) => new QInt(i.m_value % i2);
        public static QInt operator +(QInt i, Object i2)
        {
            if(!(i2 is long)) throw new InvalidCastException($"Invalid cast from {i2.GetType()} to long");
            return new QInt(i.m_value + (long)i2);
        }
        public static QInt operator -(QInt i, Object i2)
        {
            if(!(i2 is long)) throw new InvalidCastException($"Invalid cast from {i2.GetType()} to long");
            return new QInt(i.m_value - (long)i2);
        }
        public static QInt operator *(QInt i, Object i2)
        {
            if(!(i2 is long)) throw new InvalidCastException($"Invalid cast from {i2.GetType()} to long");
            return new QInt(i.m_value * (long)i2);
        }
        public static QInt operator /(QInt i, Object i2)
        {
            if(!(i2 is long)) throw new InvalidCastException($"Invalid cast from {i2.GetType()} to long");
            return new QInt(i.m_value / (long)i2);
        }
        public static QInt operator %(QInt i, Object i2)
        {
            if(!(i2 is long)) throw new InvalidCastException($"Invalid cast from {i2.GetType()} to long");
            return new QInt(i.m_value % (long)i2);
        }
        public static bool operator == (QInt i, QInt i2)
        {
            return i.Equals(i2);
        }
        public static bool operator != (QInt i, QInt i2)
        {
            return !i.Equals(i2);
        }
        // TODO: When IComparable and IConvertible are done, add {this >= (QInt)100 ? (short)((m_value%100)/10) : throw costam} in these properties
        public short Units
        {
            get => (short)(m_value%10);
            set => m_value = long.Parse(m_value.ToString().Substring(0,m_value.ToString().Length-1) + value.ToString());
        }
        public short Tens
        {
            get => (short)((m_value%100)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-2) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-1));
        }
        public short Hundreds
        {
            get => (short)((m_value%1000)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-3) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-2));
        }
        public short Thousands
        {
            get => (short)((m_value%10000)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-4) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-3));
        }
        public short TenThousands
        {
            get => (short)((m_value%100000)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-5) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-4));
        }
        public short HundredThousands
        {
            get => (short)((m_value%1000000)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-6) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-5));
        }
        public short Millions
        {
            get => (short)((m_value%10000000)/10);
            set => m_value = long.Parse(m_value.ToString().Substring(0, m_value.ToString().Length-7) + value.ToString() + m_value.ToString().Substring(m_value.ToString().Length-6));
        }
        public int CompareTo(Object value)
        {
            if(value == null)
            {
                return 1;
            }
            if(value is long)
            {
                long i = (long)value;
                if(m_value < i) return -1;
                if(m_value > i) return 1;
                return 0;
            }
            throw new ArgumentException("Argument must be Int64 (long)");
        }
        int IComparable<QInt>.CompareTo(QInt other)
        {
            return CompareTo(other);
        }
        public int CompareTo(long value)
        {
            if(m_value < value) return -1;
            if(m_value > value) return 1;
            return 0;
        }
        public override bool Equals(Object obj)
        {
            if(!(obj is long)) return false;
            return m_value == ((Int64)obj);
        }
        public bool Equals(Int64 obj)
        {
            return m_value == obj;
        }
        bool IEquatable<QInt>.Equals(QInt other)
        {
            return Equals(other);
        }
        public override int GetHashCode()
        {
            return (unchecked((int)((long)m_value)) ^ (int)(m_value >> 32));
        }
        public override String ToString()
        {
            return $"{m_value}";
        }
        public static long Parse(String s)
        {
            return Int64.Parse(s);
        }
        public static long Parse(String s, NumberStyles style)
        {
            return Int64.Parse(s, style);
        }
        public static long Parse(String s, IFormatProvider provider)
        {
            return Int64.Parse(s, provider);
        }
        public static long Parse(String s, NumberStyles style, IFormatProvider provider)
        {
            return Int64.Parse(s, style, provider);
        }
        /// <summary>
        /// Returns true/false without assigning anything
        /// </summary>
        public static bool TryParse(String s)
        {
            try
            {
                Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool TryParse(String s, out long result)
        {
            return Int64.TryParse(s, out result);
        }
        //
        // IConvertible
        //
        public TypeCode GetTypeCode()
        {
            return TypeCode.Int64;
        }
        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(m_value);
        }
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(m_value);
        }
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(m_value);
        }
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(m_value);
        }
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(m_value);
        }
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(m_value);
        }
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(m_value);
        }
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(m_value);
        }
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return m_value;
        }
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(m_value);
        }
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(m_value);
        }
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(m_value);
        }
        Decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(m_value);
        }
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Int64 to DateTime");
        }
        /// <summary>
        /// Conversion is not actually supported, it just returns the type of Int64
        /// </summary>
        Object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return GetType();
        }
    }
}