using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonDataSaver : IDataSaver 
{
	private string GetSaveFileName(string filename)
	{
		return string.Format("{0}/{1}", Application.persistentDataPath, filename);
	}

	public void Save(PlayerData dat)
	{
		string fn = GetSaveFileName ("player.txt");

		string json = JsonUtility.ToJson (dat);

		using (StreamWriter writer = new StreamWriter (new FileStream (fn, FileMode.Create)))
		{
			writer.Write (json);
		}
	}

	public bool Load(PlayerData dat)
	{
		string fn = GetSaveFileName ("player.txt");

		if (File.Exists (fn)) 
		{
			using (StreamReader reader = new StreamReader (new FileStream (fn, FileMode.Open)))
			{
				JsonUtility.FromJsonOverwrite (reader.ReadToEnd (), dat);	
			}

			return true;
		}

		return false;
	}
}
