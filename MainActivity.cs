using System;
using System.IO;

using Mono.Data.Sqlite;
using System.Data;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
	[Activity (Label = "DB", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
				(System.Environment.SpecialFolder.Personal),"test_db.sqlite"
			);

			if (!File.Exists (dbPath)) 
			{
				var dbAssetStream = this.Assets.Open("test_db");

				var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
				var buffer = new byte[1024];

				int b = buffer.Length;
				int length;

				while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
				{
					dbFileStream.Write(buffer, 0, length);
				}

				dbFileStream.Flush();
				dbFileStream.Close();
				dbAssetStream.Close();
			}

			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.myButton);
			ListView peopleList = FindViewById<ListView>(Resource.Id.listView1);




			button.Click += delegate {
				List<string> tempList = new List<string> ();
				SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
				SqliteCommand command;
				SqliteDataReader reader;

				connection.Open();

				command = connection.CreateCommand();

				/*
				command.CommandText = "UPDATE students SET name='qweqwqwe'";
				command.ExecuteNonQuery();
				//*/

				command.CommandText = "SELECT name,surname FROM students";

				reader = command.ExecuteReader();
				while(reader.Read())
				{
					tempList.Add(reader[0].ToString() + " " + reader[1].ToString());
				}
				reader.Close();

				connection.Close();

				peopleList.Adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,tempList);
			};
		}
	}
}


