using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData
{
    protected T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

    // TODO 안쓰면 나중에 삭제할 것
    //protected ItemGrade CheckItemGrade(ItemGrade? data)
    //{
    //    return data ?? ItemGrade.NONE;
    //}

    //protected DataType CheckDataType(DataType? data)
    //{
    //    return data ?? DataType.NONE;
    //}

    //protected InventoryType CheckInventoryType(InventoryType? data)
    //{
    //    return data ?? InventoryType.NONE;
    //}

    //protected ItemType CheckItemType(ItemType? data)
    //{
    //    return data ?? ItemType.NONE;
    //}

    //protected EquipSlot CheckEquipSlot(EquipSlot? data)
    //{
    //    return data ?? EquipSlot.NONE;
    //}

    //protected ElementType CheckElementType(ElementType? data)
    //{
    //    return data ?? ElementType.NONE;
    //}
}
