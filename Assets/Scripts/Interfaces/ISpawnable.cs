using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public void DeleteEntityBelowPlayer();
    public void DeleteEntity();
}
