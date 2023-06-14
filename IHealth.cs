namespace AASS
{
    interface IHealth
    {
    	public int MaxHealth{get;set;}
    	public int CurrentHealth{get;set;}
    	
        public void TakeDamage(int amount);
        public void Heal(int amount);
    }
}
