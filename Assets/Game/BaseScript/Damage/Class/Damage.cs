using System;

namespace MiniLol.Unit
{
    public struct Damage : IEquatable<Damage>
    {
        public float AttackDamage { get; }

        public Damage(float attackDamage)
        {
            this.AttackDamage = attackDamage;
        }

        public bool Equals(Damage other)
        {
            return AttackDamage.Equals(other.AttackDamage);
        }

        public override bool Equals(object obj)
        {
            return obj is Damage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return AttackDamage.GetHashCode();
        }
    }

}