using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IManager
{
  string State { get; set;}
  void Initialize();
}