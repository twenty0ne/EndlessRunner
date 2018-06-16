using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSaver 
{
	void Save(PlayerData dat);
	bool Load(PlayerData dat);
}
