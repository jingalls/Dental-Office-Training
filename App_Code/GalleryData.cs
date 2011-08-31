using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Class to represent a single row on the Gallery Page
/// </summary>
public class GalleryRow
{
    /// <summary>
    /// Creates an Empty instance of the Gallery Image Class
    /// </summary>
    public GalleryRow() { this.Columns = new List<GalleryColumn>(); }

    public List<GalleryColumn> Columns
    {
        get;
        set;
    }
}

/// <summary>
/// Class to hold the image information for a column on the Gallery Page
/// </summary>
public class GalleryColumn
{
    public GalleryColumn() { }

    public GalleryColumn(FileInfo image)
    {
        if (!new string[] { ".jpg", ".gif", ".png" }.Contains(image.Extension))
        {
            throw new ApplicationException("Invalid image extension. Failed to create instance of GalleryColumn.");
        }

        this.Image = image;
        this.ImageName = image.Name.Insert(image.Name.IndexOf('.'), "_800");
        this.ThumbNailImageName = image.Name.Insert(image.Name.IndexOf('.'), "_100");
        this.ImagePath = "~/" + image.Directory.FullName.Substring(image.Directory.FullName.IndexOf("images\\")).Replace("\\", "/");
    }

    public string ThumbNailImageName
    {
        get;
        set;
    }

    public string ImageName
    {
        get;
        set;
    }

    public string ImagePath
    {
        get;
        set;
    }

    public FileInfo Image
    {
        get;
        set;
    }
}
