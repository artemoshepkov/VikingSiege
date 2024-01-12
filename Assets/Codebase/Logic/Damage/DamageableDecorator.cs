namespace Codebase.Logic.Damage
{
    public abstract class DamageableDecorator : IDamageable
    {
        protected IDamageable _damageable;

        protected DamageableDecorator(IDamageable damageable)
        {
            _damageable = damageable;
        }

        public abstract void TakeDamage(float damage);
    }
}