using System;
using System.IO;

using Mono.Data.Sqlite;

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
	public class DatabaseCopy
	{
		public DatabaseCopy (Context context)
		{

			string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
				(System.Environment.SpecialFolder.Personal),"test_db.sqlite"
			);

			if (!File.Exists (dbPath)) {
				
				var dbAssetStream = context.Assets.Open("test_db");

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


		}
	}
}

