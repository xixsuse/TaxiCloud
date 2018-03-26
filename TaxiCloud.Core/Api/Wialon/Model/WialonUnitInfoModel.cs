// ReSharper disable UnusedMember.Global
namespace TaxiCloud.Core.Api.Wialon.Model
{
    public class WialonUnitInfoModel
    {
        public int Count { get; set; }
        public WialonMessageModel[] Messages { get; set; }
    }

    public class WialonMessageModel
    {
        public int T { get; set; }
        public int F { get; set; }
        public string Tp { get; set; }
        public MessagePos Pos { get; set; }
        public int I { get; set; }
        public int O { get; set; }
        public int Lc { get; set; }
        public MessageP P { get; set; }
    }

    public class MessagePos
    {
        public float Y { get; set; }
        public float X { get; set; }
        public int Z { get; set; }
        public int S { get; set; }
        public int C { get; set; }
        public int Sc { get; set; }
    }

    public class MessageP
    {
        public int GsmSt { get; set; }
        public int NavSt { get; set; }
        public int Mw { get; set; }
        public int SimT { get; set; }
        public int SimIn { get; set; }
        public int St0 { get; set; }
        public int St1 { get; set; }
        public int St2 { get; set; }
        public int PwrIn { get; set; }
        public float PwrExt { get; set; }
        public int Freq0 { get; set; }
        public float WlnCrnMax { get; set; }
        public int InfoMessages { get; set; }
        public float WlnBrkMax { get; set; }
        public float WlnAccelMax { get; set; }
    }
}