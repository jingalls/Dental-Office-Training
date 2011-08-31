using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Gallery : System.Web.UI.Page
{
    List<GalleryRow> galleryImages = new List<GalleryRow>();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the images for the datagrid display
        galleryImages = App_Code.Helper.GetGalleryImages();
        rptGalleryImages.DataSource = galleryImages;
        rptGalleryImages.DataBind();
    }

    protected void rptGalleryImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            GalleryRow galleryRow = (GalleryRow)e.Item.DataItem;

            for (int i = 0; i < galleryRow.Columns.Count; i++)
            {
                HtmlAnchor imgLink = (HtmlAnchor)e.Item.FindControl("imgLinkCol" + (i + 1).ToString());
                imgLink.HRef = galleryRow.Columns[i].ImagePath + "/" + galleryRow.Columns[i].ImageName;
                imgLink.Title = galleryRow.Columns[i].ImageName;

                HtmlImage imageGalleryItem = (HtmlImage)e.Item.FindControl("imgGalleryItemCol" + (i + 1).ToString());
                imageGalleryItem.Src = galleryRow.Columns[i].ImagePath + "/" + galleryRow.Columns[i].ThumbNailImageName;
                imageGalleryItem.Alt = galleryRow.Columns[i].ImageName;

                if (e.Item.ItemIndex == (galleryImages.Count - 1) && i == (galleryRow.Columns.Count - 1))
                {
                    while (i % 3 != 0)
                    {
                        i++;

                        imgLink = (HtmlAnchor)e.Item.FindControl("imgLinkCol" + (i + 1).ToString());
                        imgLink.Visible = false;

                        imageGalleryItem = (HtmlImage)e.Item.FindControl("imgGalleryItemCol" + (i + 1).ToString());
                        imageGalleryItem.Visible = false;
                    }
                }
            }
        }
    }
}
