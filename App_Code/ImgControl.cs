using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Summary description for ImgControl
/// </summary>
[Flags]
public enum Zone : byte
{
    topLeft = 9,
    topMiddle = 10,
    topRigth = 12,
    centerLeft = 17,
    centerMiddle = 18,
    centerRight = 20,
    bottomLeft = 33,
    bottomMiddle = 34,
    bottomRight = 36
}

public class ImgControl
{
    [Flags]
    private enum Location : byte
    {
        left = 1,
        middle = 2,
        right = 4,
        top = 8,
        center = 16,
        bottom = 32
    }

	public ImgControl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void ScalePic(string base_pic, int width, string nw_file)
    {
        int MaxHeight = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxImageHeight"]);

        System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(base_pic);

        // Prevent using images internal thumbnail
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

        int NewHeight = FullsizeImage.Height * width / FullsizeImage.Width;
        if (NewHeight > MaxHeight)
        {
            // Resize with height instead
            width = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
            NewHeight = MaxHeight;
        }


        System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(width, NewHeight, null, IntPtr.Zero);

        // Clear handle to original file so that we can overwrite it if necessary
        FullsizeImage.Dispose();

        // Save resized picture
        NewImage.Save(nw_file);
    }

    public static void OverlayPics(string base_pic, string appended_pic, string new_file, Zone zone)
    {
        OverlayPics(base_pic, appended_pic, new_file, zone, 0);
    }    
    public static void OverlayPics(string base_pic, string appended_pic, string new_file, Zone zone, int border)
    {
        OverlayPics(base_pic, appended_pic, new_file, zone, border, Color.Empty);
    }
    public static void OverlayPics(string base_pic, string appended_pic, string new_file, Zone zone, int border, Color color)
    {
        System.Drawing.Image org_image = System.Drawing.Image.FromFile(base_pic);
        int org_image_width = org_image.Width;
        int org_image_height = org_image.Height;

        Graphics grNw_image = Graphics.FromImage(org_image);

        System.Drawing.Image imgOverlay = new Bitmap(appended_pic);
        int overlay_width = imgOverlay.Width;
        int overlay_height = imgOverlay.Height;

        ImageAttributes imgAttrib = new ImageAttributes();
        ColorMap colorMap = new ColorMap();
        colorMap.OldColor = color;
        colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] remapTable = { colorMap };
        imgAttrib.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

        int xPosofWm = calculatePicXAxis(org_image_width, overlay_width, border, zone);
        int yPosofWm = calculatePicYAxis(org_image_height, overlay_height, border, zone);

        grNw_image.DrawImage(imgOverlay, new Rectangle(xPosofWm,yPosofWm, overlay_width, overlay_height), 0, 0, overlay_width, overlay_height, GraphicsUnit.Pixel, imgAttrib);
        org_image.Save(new_file);
        //grNw_image.Dispose();
        //org_image.Dispose();
       // imgOverlay.Dispose();
    }

    public static void OverlayText(string base_pic, string text, string new_file, Zone zone, bool fitToWidth)
    {
        Font font = new Font("Arial", 12, FontStyle.Regular);
        OverlayText(base_pic, text, new_file, zone, fitToWidth, font);
    }        
    public static void OverlayText(string base_pic, string text, string new_file, Zone zone, bool fitToWidth, Font font)
    {
        System.Drawing.Image org_image = System.Drawing.Image.FromFile(base_pic);
        int org_image_width = org_image.Width;
        int org_image_height = org_image.Height;

        Bitmap new_image = new Bitmap(org_image_width, org_image_height, PixelFormat.Format24bppRgb);
        new_image.SetResolution(org_image.HorizontalResolution, org_image.VerticalResolution);
  
        Graphics grNw_image = Graphics.FromImage(new_image);

        grNw_image.SmoothingMode = SmoothingMode.AntiAlias;
        grNw_image.DrawImage(org_image, new Rectangle(0, 0, org_image_width, org_image_height), 0, 0, org_image_width, org_image_height, GraphicsUnit.Pixel);
        int[] fontSizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

        SizeF textSize = new SizeF();

        if(fitToWidth.Equals(true))
        {
            string fontName = font.Name;
            FontStyle fontStyle = font.Style;

            for (int i = 0; i < 7; i++)
            {
                font = new Font(fontName, fontSizes[i], fontStyle);
                textSize = grNw_image.MeasureString(text, font);

                if ((ushort)textSize.Width < (ushort)org_image_width)
                    break;
            }
        }

        StringFormat StrFormat = new StringFormat();
        if (zone.Equals(Zone.topLeft) || zone.Equals(Zone.centerLeft) || zone.Equals(Zone.bottomLeft))
        {
            StrFormat.Alignment = StringAlignment.Near;
        }
        else if (zone.Equals(Zone.topMiddle) || zone.Equals(Zone.centerMiddle) || zone.Equals(Zone.bottomMiddle))
        {
            StrFormat.Alignment = StringAlignment.Center;
        }
        else
        {
            StrFormat.Alignment = StringAlignment.Far;
        }

        int yPosofText = calculateTxtYAxis(org_image_height, (int)textSize.Height, zone);
        int xPosofText = calculateTxtXAxis(org_image_width, (int)textSize.Width, zone);

        
        SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

        grNw_image.DrawString(text,                 //string of text
                font,                                   //font
                semiTransBrush2,                           //Brush
                new PointF(xPosofText, yPosofText),  //Position
                StrFormat);

        SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
        grNw_image.DrawString(text, font, semiTransBrush, new PointF(xPosofText, yPosofText), StrFormat);
        new_image.Save(new_file);
    }

    #region CoordinateCalculations
    private static int calculatePicYAxis(int org_height, int overlay_height, int border, Zone zone)
    {
        int origin;

        if((((byte)zone) & ((byte)Location.top)) == ((byte)Location.top))
        {
            return border;
        }
        else if ((((byte)zone) & ((byte)Location.center)) == ((byte)Location.center))
        {
            origin = ((org_height - overlay_height) / 2);
            return origin;
        }
        else
        {
            origin = ((org_height - overlay_height) - border);
            return origin;
        }
    }
    private static int calculatePicXAxis(int org_width, int overlay_width, int border, Zone zone)
    {
        int origin;

        if ((((byte)zone) & ((byte)Location.left)) == ((byte)Location.left))
        {
            return border;
        }
        else if ((((byte)zone) & ((byte)Location.middle)) == (((byte)Location.middle)))
        {
            origin = ((org_width - overlay_width) / 2);
            return origin;
        }
        else
        {
            origin = ((org_width - overlay_width) - border);
            return origin;
        }
    }

    private static int calculateTxtXAxis(int org_width, int text_width, Zone zone)
    {
        int origin;
        if ((((byte)zone) & ((byte)Location.left)) == ((byte)Location.left))
        {
            origin = org_width / 12;
            return origin;
        }
        else if ((((byte)zone) & ((byte)Location.middle)) == ((byte)Location.middle))
        {
            origin = org_width / 2;
            return origin;
        }
        else
        {
            origin = (org_width * 11) / 12;
            return origin;
        }
    }
    private static int calculateTxtYAxis(int org_Height, int text_height, Zone zone)
    {
        int origin;
        if ((((byte)zone) & ((byte)Location.top)) == ((byte)Location.top))
        {
            origin = org_Height / 12;
            return origin;
        }
        else if ((((byte)zone) & ((byte)Location.center)) == ((byte)Location.center))
        {
            origin = org_Height / 2;
            return origin;
        }
        else
        {
            origin = (org_Height * 11) / 12;
            return origin;
        }
    }
    #endregion  
}
