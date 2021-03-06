﻿using System;

namespace EzGoldSharp.UnitManager
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    internal class MyHero
    {
        #region Fields

        private static float _attackRange;

        #endregion Fields

        #region Methods

        public static float AttackRange()
        {
            if (!Utils.SleepCheck("MyHeroInfo.AttackRange"))
            {
                return _attackRange;
            }
            Utils.Sleep(1000, "MyHeroInfo.AttackRange");

            if (Variables.Me.ClassId == ClassId.CDOTA_Unit_Hero_TrollWarlord)
                _attackRange = Variables.Q.IsToggled ? 128 : Variables.Me.GetAttackRange();
            else
                _attackRange = Variables.Me.GetAttackRange();

            return _attackRange;
        }

        public static List<Ability> GetAbilities()
        {
            return AllyHeroes.AbilityDictionary[Variables.Me.Handle].Where(x => x.CanBeCasted()).ToList();
        }

        public static List<Item> GetItems()
        {
            return AllyHeroes.ItemDictionary[Variables.Me.Handle];
        }

        public static Item GetItem(string name)
        {
            return GetItems().FirstOrDefault(x => x.Name == name);
        }

        public static float GetProjectileSpeed(Entity unit)
        {
            return Variables.Me.ClassId == ClassId.CDOTA_Unit_Hero_ArcWarden
                ? 800
                : UnitDatabase.GetByName(unit.Name).ProjectileSpeed;
        }

        public static float GetMyAttackSpeed(Hero hero)
        {
            var attackSpeed = Math.Min(hero.AttacksPerSecond * 1 / 0.01, 600);

            return (float)attackSpeed;
        }

        public static float GetMyAttackPoint(Hero hero)
        {
            var animationPoint = 0f;

            var attackSpeed = GetMyAttackSpeed(hero);

            animationPoint = (float) hero.AttackPoint();

            return animationPoint / (1 + (attackSpeed - 100) / 100);
        }

        #endregion Methods
    }
}