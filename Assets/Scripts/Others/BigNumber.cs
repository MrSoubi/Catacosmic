using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;

namespace BigFloatNumerics
{
    [Serializable]
    public class BigNumber : IComparable, IComparable<BigNumber>, IEquatable<BigNumber>
    ///number is m × 10^n
    {
        public float m { get; private set; } // you could set this to `double` and there should be minimal problem. Decimal is better.
        public int n { get; private set; }

        public static readonly BigNumber IntMax = (BigNumber)int.MaxValue;

        const float CompTolerance = 1e-6f;
        const int CompTolerancei = 6;

        private List<string> indexToMagnitude = new()
    {
        "",
        "e1",
        "e2",
        "e3",
        "e4",
        "e5",
        "e6",
        "e7",
        "e8",
        "e9",
        "e10",
        "e11",
        "e12",
        "e13",
        "e14",
        "e15",
        "e16",
        "e17",
        "e18",
        "e19",
        "e20",
        "e21",
        "e22",
        "e23",
        "e24",
        "e25",
        "e26",
        "e27",
        "e28",
        "e29",
        "e30",
        "e31",
        "e32",
        "e33",
        "e34",
        "e35",
        "e36",
        "e37",
        "e38",
        "e39",
        "e40",
        "e41",
        "e42",
        "e43",
        "e44",
        "e45",
        "e46",
        "e47",
        "e48",
        "e49",
        "e50",
        "e51",
        "e52",
        "e53",
        "e54",
        "e55",
        "e56",
        "e57",
        "e58",
        "e59",
        "e60",
        "e61",
        "e62",
        "e63",
        "e64",
        "e65",
        "e66",
        "e67",
        "e68",
        "e69",
        "e70",
        "e71",
        "e72",
        "e73",
        "e74",
        "e75",
        "e76",
        "e77",
        "e78",
        "e79",
        "e80",
        "e81",
        "e82",
        "e83",
        "e84",
        "e85",
        "e86",
        "e87",
        "e88",
        "e89",
        "e90",
        "e91",
        "e92",
        "e93",
        "e94",
        "e95",
        "e96",
        "e97",
        "e98",
        "e99",
        "e100",
        "e101",
        "e102",
        "e103",
        "e104",
        "e105",
        "e106",
        "e107",
        "e108",
        "e109",
        "e110",
        "e111",
        "e112",
        "e113",
        "e114",
        "e115",
        "e116",
        "e117",
        "e118",
        "e119",
        "e120",
        "e121",
        "e122",
        "e123",
        "e124",
        "e125",
        "e126",
        "e127",
        "e128",
        "e129",
        "e130",
        "e131",
        "e132",
        "e133",
        "e134",
        "e135",
        "e136",
        "e137",
        "e138",
        "e139",
        "e140",
        "e141",
        "e142",
        "e143",
        "e144",
        "e145",
        "e146",
        "e147",
        "e148",
        "e149",
        "e150",
        "e151",
        "e152",
        "e153",
        "e154",
        "e155",
        "e156",
        "e157",
        "e158",
        "e159",
        "e160",
        "e161",
        "e162",
        "e163",
        "e164",
        "e165",
        "e166",
        "e167",
        "e168",
        "e169",
        "e170",
        "e171",
        "e172",
        "e173",
        "e174",
        "e175",
        "e176",
        "e177",
        "e178",
        "e179",
        "e180",
        "e181",
        "e182",
        "e183",
        "e184",
        "e185",
        "e186",
        "e187",
        "e188",
        "e189",
        "e190",
        "e191",
        "e192",
        "e193",
        "e194",
        "e195",
        "e196",
        "e197",
        "e198",
        "e199",
    };

        public BigNumber Arrange() // Sets Numerator to be at range of `[1,10)`
        {
            float absm = Mathf.Abs(m);
            if (absm < float.Epsilon)
            {
                n = 0;
                return this;
            }
            int log = (int)Mathf.Floor(Mathf.Log10(absm));
            n += log;
            m /= Mathf.Pow(10, log);
            return this;
        }

        #region constructors
        public BigNumber(string value)
        {
            BigNumber bf = Parse(value);
            this.m = bf.m;
            this.n = bf.n;
        }
        public BigNumber(float m, int n)
        {
            this.m = m;
            this.n = n;
            Arrange();
        }
        public BigNumber(BigInteger value)
        {
            int log = (int)Math.Floor(BigInteger.Log10(BigInteger.Abs(value)));
            if (log < 8)
            {
                m = (float)value;
                Arrange();
                return;
            }
            n = log;
            int valueDividedByLog = (int)(value / BigInteger.Pow(10, log - 8));
            m = valueDividedByLog / 1e8f; // Int.Max =~ 2e9. so divide to int range then divide again to double range.
        }
        public BigNumber(BigNumber value)
        {
            this.m = value.m;
            this.n = value.n;
        }
        public BigNumber(ulong value)
        {
            m = (float)value;
            n = 0;
            Arrange();
        }
        public BigNumber(long value)
        {
            m = (float)value;
            n = 0;
            Arrange();
        }
        public BigNumber(uint value)
        {
            m = (float)value;
            n = 0;
            Arrange();
        }
        public BigNumber(int value)
        {
            m = (float)value;
            n = 0;
            Arrange();
        }
        public BigNumber(float value)
        {
            m = (float)value;
            n = 0;
            Arrange();
        }
        public BigNumber(double value) : this(value.ToString("e9"))// converts to "123e+5678", hence "+5678" can be parsed correctly, is very lazy approach
        {
        }
        public BigNumber(decimal value)
        {
            m = (float)value;
            Arrange();
        }
        #endregion

        #region basic arithmetic
        public BigNumber Add(BigNumber other)
        {
            if (this.n > other.n)
            {
                if (this.n - other.n > 20) return this;
                m += other.m / Mathf.Pow(10, (float)(this.n - other.n));
                this.Arrange();
            }
            else
            {
                if (other.n - this.n > 20) return other;
                m *= Mathf.Pow(10, (float)(this.n - other.n));
                m += other.m;
                this.n = other.n;
                this.Arrange();
            }

            return this;
        }
        public BigNumber Subtract(BigNumber other)
        {
            return Add(other.Negate());
        }


        public BigNumber Multiply(BigNumber other)
        {
            this.m *= other.m;
            this.n += other.n;
            this.Arrange();
            return this;
        }
        public BigNumber Divide(BigNumber other)
        {
            if (other.m == 0)
                throw new System.DivideByZeroException("other");

            this.m /= other.m;
            this.n -= other.n;
            this.Arrange();
            return this;
        }
        public BigNumber Remainder(BigNumber other)
        {

            throw new NotImplementedException("Does not mattered to me lol");
        }
        public BigNumber Pow(int exponent) // there is no smart way to do float lol
        {
            if (m == 0)
            {
                // Nothing to do
            }
            if (exponent > 30)
                return Pow(Pow(this, exponent / 30), 30) + Pow(exponent % 30);

            this.n += exponent;
            this.m = Mathf.Pow(m, exponent);
            Arrange();

            return this;
        }
        public BigNumber Abs()
        {
            m = Mathf.Abs(m);
            return this;
        }
        public BigNumber Negate()
        {
            m = -m;
            return this;
        }
        public BigInteger Log10i()
        {
            return n;
        }
        public BigNumber Log10()
        {
            return Mathf.Log10(m) + (BigNumber)n;
        }
        public BigNumber Log(double baseValue)
        {
            return Log10() * Math.Log(baseValue, 10);
        }

        public int CompareTo(BigNumber other)
        {
            var diff = this - other;

            if (diff.n == 0 && diff.m == 0)
                return 0;
            if (this.n - diff.n > CompTolerancei)
                return 0;

            else return diff.m.CompareTo(0);
        }
        public int CompareTo(object other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (!(other is BigNumber))
                throw new System.ArgumentException("other is not a BiggerFloat");

            return CompareTo((BigNumber)other);
        }
        public bool Equals(BigNumber other)
        {
            int comResult = CompareTo(other);
            return comResult == 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Derived static Arithmetic methods
        public static int Compare(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).CompareTo(right);
        }

        public static BigNumber Negate(BigNumber value)
        {
            return (new BigNumber(value)).Negate();
        }
        public static BigNumber Abs(BigNumber value)
        {
            return (new BigNumber(value)).Abs();
        }
        public static BigNumber Add(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Add(right);
        }
        public static BigNumber Subtract(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Subtract(right);
        }
        public static BigNumber Multiply(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Multiply(right);
        }
        public static BigNumber Divide(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Divide(right);
        }
        public static BigNumber Pow(BigNumber value, int exponent)
        {
            return (new BigNumber(value)).Pow(exponent);
        }
        public static BigNumber Remainder(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Remainder(right);
        }
        public static BigNumber Log10(BigNumber value)
        {
            return (new BigNumber(value)).Log10();
        }
        public static BigNumber Log(BigNumber value, double baseValue)
        {
            return (new BigNumber(value)).Log(baseValue);
        }
        #endregion

        #region ToString and Parse
        public override string ToString()
        {
            string digit = "";
            float value = m;

            if (value * Mathf.Pow(10f, n) < 1000) return $"{(value * Mathf.Pow(10f, n)):F0}";

            if (n % 3 == 0) digit = indexToMagnitude[n / 3];
            else if (n % 3 == 1)
            {
                value *= 10;
                digit = n / 3 - 1 >= 0 ? indexToMagnitude[n / 3] : indexToMagnitude[0];
            }
            else if (n % 3 == 2)
            {
                value *= 100;
                digit = n / 3 - 1 >= 0 ? indexToMagnitude[n / 3] : indexToMagnitude[0];
            }
            return $"{value:F2}{digit}";
        }
        public string ToHumanFriendlyString(int? maxLength = null)
        {
            if (maxLength == null)
                return $"{m:F10}e{n}";
            if (BigInteger.Abs(n) < maxLength)
            {
                return $"{(double)m:F99}".Substring(0, (int)maxLength);
            }
            else
            {
                int len = (int)(float)this.Log(10);
                if (len > maxLength)
                {
                    return $"{m:F0}e{n}";
                }
                else
                {
                    return $"{m.ToString("F" + (maxLength - len - 3))}e{n}";
                }
            }
        }

        public static BigNumber Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            value.Trim();
            value = value.Replace(",", "");
            int pos = value.IndexOf('e');
            value = value.Replace("e", "");

            if (pos < 0)
            {
                if (value.IndexOf('.') >= 0)
                {
                    //just floating point 
                    double Signifacand = double.Parse(value);
                    return (new BigNumber(Signifacand)).Arrange();
                }
                else
                {
                    //just big integer
                    BigInteger nu = BigInteger.Parse(value);
                    return (new BigNumber(nu)).Arrange();
                }
            }
            else
            {
                //decimal point (length - pos - 1)
                float Signifacand = float.Parse(value.Substring(0, pos));

                int denominator = int.Parse(value.Substring(pos));

                return (new BigNumber(Signifacand, denominator)).Arrange();
            }
        }
        public static bool TryParse(string value, out BigNumber result)
        {
            try
            {
                result = BigNumber.Parse(value);
                return true;
            }
            catch (ArgumentNullException)
            {
                result = 0;
                return false;
            }
            catch (FormatException)
            {
                result = 0;
                return false;
            }
        }

        #endregion

        #region Derived Functions and Operators
        public static BigNumber operator -(BigNumber value)
        {
            return (new BigNumber(value)).Negate();
        }
        public static BigNumber operator -(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Subtract(right);
        }
        public static BigNumber operator --(BigNumber value)
        {
            return value.Subtract(1);
        }
        public static BigNumber operator +(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Add(right);
        }
        public static BigNumber operator +(BigNumber value)
        {
            return (new BigNumber(value)).Abs();
        }
        public static BigNumber operator ++(BigNumber value)
        {
            return value.Add(1);
        }
        public static BigNumber operator %(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Remainder(right);
        }
        public static BigNumber operator *(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Multiply(right);
        }
        public static BigNumber operator /(BigNumber left, BigNumber right)
        {
            return (new BigNumber(left)).Divide(right);
        }
        public static BigNumber operator ^(BigNumber left, int right)
        {
            return (new BigNumber(left)).Pow(right);
        }

        public static bool operator !=(BigNumber left, BigNumber right)
        {
            return Compare(left, right) != 0;
        }
        public static bool operator ==(BigNumber left, BigNumber right)
        {
            return Compare(left, right) == 0;
        }
        public static bool operator <(BigNumber left, BigNumber right)
        {
            return Compare(left, right) < 0;
        }
        public static bool operator <=(BigNumber left, BigNumber right)
        {
            return Compare(left, right) <= 0;
        }
        public static bool operator >(BigNumber left, BigNumber right)
        {
            return Compare(left, right) > 0;
        }
        public static bool operator >=(BigNumber left, BigNumber right)
        {
            return Compare(left, right) >= 0;
        }
        #endregion

        #region Casts
        public static explicit operator decimal(BigNumber value)
        {
            if (decimal.MinValue > value) throw new System.OverflowException("value is less than System.decimal.MinValue.");
            if (decimal.MaxValue < value) throw new System.OverflowException("value is greater than System.decimal.MaxValue.");

            return (decimal)(value.m * Math.Pow(10, (double)value.n));
        }

        public static explicit operator BigInteger(BigNumber v)
        {
            if ((v.n - 5) > int.MaxValue) throw new OverflowException("value is too large");
            if ((v > int.MaxValue || v < int.MinValue))
            {
                return BigInteger.Multiply((BigInteger)(v.m * 1e5f), BigInteger.Pow(10, (int)(v.n - 5)));
            }
            else return (int)v;
        }

        public static explicit operator double(BigNumber value)
        {
            if (double.MinValue > value) throw new System.OverflowException("value is less than System.double.MinValue.");
            if (double.MaxValue < value) throw new System.OverflowException("value is greater than System.double.MaxValue.");

            return (double)value.m * Math.Pow(10, (double)value.n);
        }
        public static explicit operator float(BigNumber value)
        {
            if (float.MinValue > value) throw new System.OverflowException("value is less than System.float.MinValue.");
            if (float.MaxValue < value) throw new System.OverflowException("value is greater than System.float.MaxValue.");

            return (float)(value.m * Mathf.Pow(10, (float)value.n));
        }

        public static explicit operator int(BigNumber value)
        {
            if (int.MinValue > value) throw new System.OverflowException("value is less than System.float.MinValue.");
            if (int.MaxValue < value) throw new System.OverflowException("value is greater than System.float.MaxValue.");

            return (int)(value.m * Mathf.Pow(10, (float)value.n));
        }


        public static implicit operator BigNumber(byte value)
        {
            return new BigNumber((uint)value);
        }
        public static implicit operator BigNumber(sbyte value)
        {
            return new BigNumber((int)value);
        }
        public static implicit operator BigNumber(short value)
        {
            return new BigNumber((int)value);
        }
        public static implicit operator BigNumber(ushort value)
        {
            return new BigNumber((uint)value);
        }
        public static implicit operator BigNumber(int value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(long value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(uint value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(ulong value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(decimal value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(double value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(float value)
        {
            return new BigNumber(value);
        }
        public static implicit operator BigNumber(BigInteger value)
        {
            return new BigNumber(value);
        }
        public static explicit operator BigNumber(string value)
        {
            return new BigNumber(value);
        }
        #endregion
    }
}

