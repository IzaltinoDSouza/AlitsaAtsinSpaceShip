namespace AASS
{
    interface IShield
    {
        public bool ShieldEnable{get;set;}
        public bool ShieldActivate{get;set;}
        public float ShieldDuration{get;set;}
        public int ShieldMaxCount{get;set;}
        public int ShieldCount{get;set;}
    }
}
