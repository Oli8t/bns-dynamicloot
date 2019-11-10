using BS;

namespace DynamicLoot
{
  public class DynamicLoot : LevelModule
  {
    public override void OnLevelLoaded(LevelDefinition levelDefinition)
    {
      PatchLootTables(Catalog.current);
      base.OnLevelLoaded(levelDefinition);
    }

    private void PatchLootTables(Catalog catalog)
    {
      LootTable weapon1H = catalog.GetData<LootTable>("Weapon1H");
      LootTable shields = catalog.GetData<LootTable>("Shields");

      foreach (ItemData item in catalog.GetDataList(Catalog.Category.Item))
      {
        LootTable.Drop drop = CreateDrop(item);
        if (IsWeapon1H(item))
        {
          weapon1H.drops.Add(drop);
        }
        if (IsShield(item))
        {
          shields.drops.Add(drop);
        }
      }
    }

    LootTable.Drop CreateDrop(ItemData item)
    {
      LootTable.Drop newDrop = new LootTable.Drop();
      newDrop.itemID = item.id;
      newDrop.reference = 0;
      newDrop.lootTableID = string.Empty;
      newDrop.probabilityWeight = 1f;
      return newDrop;
    }

    private bool IsWeapon1H(ItemData item)
    {
      return item.category == ItemData.Category.Weapon &&
         item.moduleAI.weaponClass == ItemModuleAI.WeaponClass.Melee &&
         item.moduleAI.weaponHandling == ItemModuleAI.WeaponHandling.OneHanded;
    }

    private bool IsShield(ItemData item)
    {
      return item.category == ItemData.Category.Shield;
    }
  }
}
