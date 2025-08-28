using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FactoryBase
{
    void Init();
    FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex);
}
