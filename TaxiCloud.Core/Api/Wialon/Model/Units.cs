// ReSharper disable UnusedMember.Global
namespace TaxiCloud.Core.Api.Wialon.Model
{
    public class Unit
    {
        public int I { get; set; }
        public D D { get; set; }
        public int F { get; set; }

        public override string ToString()
        {
            return D == null ? "unknown" : D.Nm;
        }
    }

    public class D
    {
        public string Nm { get; set; }
        public int Cls { get; set; }
        public int Id { get; set; }
        public int Mu { get; set; }
        public Pos Pos { get; set; }
        public Lmsg Lmsg { get; set; }
        public long Uacl { get; set; }
    }

    public class Pos
    {
        public int T { get; set; }
        public int F { get; set; }
        public int Lc { get; set; }
        public float Y { get; set; }
        public float X { get; set; }
        public float Z { get; set; }
        public int S { get; set; }
        public int C { get; set; }
        public int Sc { get; set; }
    }

    public class Lmsg
    {
        public int T { get; set; }
        public int F { get; set; }
        public string Tp { get; set; }
        public Pos1 Pos { get; set; }
        public int Lc { get; set; }
        public P P { get; set; }
        public int I { get; set; }
    }

    public class Pos1
    {
        public float Y { get; set; }
        public float X { get; set; }
        public float Z { get; set; }
        public int S { get; set; }
        public int C { get; set; }
        public int Sc { get; set; }
    }

    public class P
    {
        public float Odometer { get; set; }
        public int Param240 { get; set; }
        public float Adc1 { get; set; }
        public float PwrExt { get; set; }
        public int Param24 { get; set; }
        public int BatteryCharge { get; set; }
        public int Hdop { get; set; }
        public int IoCaused { get; set; }
        public int Adc2 { get; set; }
        public int Power { get; set; }
        public int GsmOperator { get; set; }
        public int TcoDistance { get; set; }
    }
}