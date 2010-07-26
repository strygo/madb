﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;

namespace Managed.Adb.IO {
	public static class Rgb565 {
		/*public static Image ToImage ( String file ) {
			using ( FileStream fs = new FileStream ( file, FileMode.Open, FileAccess.Read ) ) {
				return ToImage ( fs );
			}
		}*/

		public static Image ToImage ( byte[] data, int width, int height ) {
			int pixels = data.Length / 2;
			Bitmap bitmap = new Bitmap ( width, height, PixelFormat.Format16bppRgb565 );
			BitmapData bitmapdata = bitmap.LockBits ( new Rectangle ( 0, 0, width, height ), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb565 );
			Bitmap image = new Bitmap ( width, height, PixelFormat.Format16bppRgb565 );
			for ( int i = 0; i < data.Length; i++ ) {
				Marshal.WriteByte ( bitmapdata.Scan0, i, data[i] );
			}
			bitmap.UnlockBits ( bitmapdata );
			using ( Graphics g = Graphics.FromImage ( image ) ) {
				g.DrawImage ( bitmap, new Point ( 0, 0 ) );
				return image;
			}
		}

		public static bool ToRgb565 ( this Image image, string file ) {
			try {
				Bitmap bmp = image as Bitmap;
				BitmapData bmpData = bmp.LockBits ( new Rectangle ( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadOnly, PixelFormat.Format16bppRgb565 );
				using ( FileStream fs = new FileStream ( file, FileMode.Create, FileAccess.Write, FileShare.Read ) ) {
					byte[] buffer = new byte[307200];
					Marshal.Copy ( bmpData.Scan0, buffer, 0, buffer.Length );
					fs.Write ( buffer, 0, buffer.Length );
				}
				bmp.UnlockBits ( bmpData );
				return true;
			} catch {
				return false;
			}
		}
	}

}
